using Hastane.Model;
using Hastane.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hastane.Controller
{
    public class HastaController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly HospitalDbContext _context;

        public HastaController(HospitalDbContext context)
        {
            var model = new HastaViewModel();
            _context = context;
        }
        public IActionResult Home()
        {
            // Session'dan HastaId'yi al
            var hastaid = HttpContext.Session.GetInt32("HastaId");
            if (hastaid == null)
            {
                // Eğer Session'da hasta id yoksa, kullanıcı giriş yapmamış olabilir
                return RedirectToAction("Login", "Account"); // Giriş sayfasına yönlendirin
            }

            var hastaView = new HastaViewModel();
            hastaView.Hasta = _context.Hastalar.Where(h => h.HastaId == hastaid).ToList();

            // Geri kalan bilgileri doldur
            hastaView.Brans = _context.Branslar.ToList();
            hastaView.Doktors = _context.Doktorlar.ToList();

            var randevu = _context.Randevular.Where(r => r.HastaId == hastaid).ToList();
            hastaView.BekleyenRandevular = randevu
                .Where(r => r._randevuTarihi > DateTime.Now)
                .ToList();
            hastaView.GecmisRandevular = randevu
                .Where(r => r._randevuTarihi <= DateTime.Now)
                .ToList();

            return View(hastaView);
        }

        [HttpPost]
        public IActionResult SifreDegistir(HastaViewModel hastaView, int hastaid, string currentPassword, string newPassword)
        {
            var hasta = _context.Hastalar.FirstOrDefault(h => h.HastaId == hastaid);
            if (hasta == null)
            {
                Console.WriteLine("Hasta nesnesi null.");
                return RedirectToAction("Home");  // "Home" sayfasına geri dön
            }
            else if (hasta != null && hasta.Sifre != currentPassword)
            {
                ViewBag.ErrorMessage = "Mevcut şifreniz uyuşmamaktadır.";
                return RedirectToAction("Home");  // "Home" sayfasına geri dön
            }
            if (hasta != null)
            {
                hasta.Sifre = newPassword;
                _context.SaveChanges();
                // Session'a hastaid'yi kaydedelim
                HttpContext.Session.SetInt32("HastaId", hastaid);
                TempData["SuccessMessage"] = "Şifreniz başarıyla değiştirildi.";
                return RedirectToAction("Home");  // "Home" sayfasına geri dön
            }
            else
            {
                return RedirectToAction("Home");  // "Home" sayfasına geri dön

            }
        }
        [HttpGet]
        public IActionResult DoktorGetir(int bransId)
        {
            // Debug: Gelen bransId'yi kontrol edin
            Console.WriteLine($"BransId: {bransId}");

            var doktorlar = _context.Doktorlar
                .Where(d => d.BransId == bransId)
                .Select(d => new { d.DoktorId, d.DoktorAd, d.DoktorSoyad })
                .ToList();

            // Debug: Dönen doktor sayısını kontrol edin
            Console.WriteLine($"Dönen Doktor Sayısı: {doktorlar.Count}");

            return Json(doktorlar);
        }


        public IActionResult MusaitTarihVeSaatler(int doktorId, DateTime tarih)
        {
            var tarihler = _context.RandevuMusait
                .Where(r => r.DoktorId == doktorId && r.Durum == true)
                .GroupBy(r => r.MusaitTarih)
                .Select(g => g.Key.ToString("yyyy-MM-dd"))
                .ToList();

            var saatler = _context.RandevuMusait
                .Where(r => r.DoktorId == doktorId && r.Durum == true)
                .GroupBy(r => r.MusaitTarih)
                .ToDictionary(
                    g => g.Key.ToString("yyyy-MM-dd"),
                    g => g.Select(r => r.MusaitSaat.ToString())
                          .ToList()
                );

            return Json(new { tarihler, saatler });
        }

        [HttpPost]
        public IActionResult RandevuAl(HastaViewModel hastaView, Randevu randevu, int HastaId, int bransId)
        {
            var hastaid = HttpContext.Session.GetInt32("HastaId");
            var rnd = new Randevu();
            rnd.HastaId = hastaid.Value;
            rnd.DoktorId = randevu.DoktorId;
            rnd.RandevuTarihi = randevu.RandevuTarihi;
            rnd.RandevuSaati = randevu.RandevuSaati;
            rnd.OnayDurumu = true;
            var rndmusait = _context.RandevuMusait.FirstOrDefault(r => r.MusaitSaat == rnd.RandevuSaati && r.MusaitTarih == rnd._randevuTarihi && r.DoktorId == rnd.DoktorId);
            rndmusait.Durum = false;
            _context.Randevular.Add(rnd);
            _context.SaveChanges();
            // Kullanıcıya başarı mesajı
            TempData["Message"] = "Randevunuz başarıyla alınmıştır.";
            return RedirectToAction("Home", "Hasta");
        }
        [HttpPost]
        public IActionResult RandevuSil(int randevuid)
        {
            var randevu = _context.Randevular.FirstOrDefault(r => r.RandevuId == randevuid);
            if (randevu != null)
            {
                _context.Randevular.Remove(randevu);
                _context.SaveChanges();
                TempData["RMessage"] = "Randevu başarıyla iptal edildi.";
            }
            else
            {
                TempData["RErrorMessage"] = "Randevu bulunamadı.";
            }

            return RedirectToAction("Home");
        }

    }
}
