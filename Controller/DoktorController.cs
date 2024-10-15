using Hastane.Model;
using Hastane.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Hastane.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoktorController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly HospitalDbContext _context;

        public DoktorController(HospitalDbContext context)
        {
            _context = context;
        }

        [HttpGet("home")]
        public IActionResult DoktorHome()
        {
            var doktorid = HttpContext.Session.GetInt32("DoktorId");
            if (doktorid == null)
            {
                // Eğer Session'da doktor id yoksa, giriş yapmamış olabilir
                return RedirectToAction("Login", "Account"); // Giriş sayfasına yönlendir
            }
            // Doktoru getiriyoruz
            var doktorView = new DoktorViewModel();
            doktorView.Doktor = _context.Doktorlar
                .Where(d => d.DoktorId == doktorid.Value) // Value ile nullable'ı çözüyoruz
                .ToList();
            // Randevuları alıyoruz
            var brans = _context.Branslar.FirstOrDefault(b =>b.BransId==doktorView.Doktor.First().BransId);
            doktorView.Brans.Add(brans); 
            var randevu = _context.Randevular.Where(r => r.DoktorId == doktorid.Value).ToList();
            // Bekleyen randevuları ve geçmiş randevuları alıyoruz
            doktorView.BekleyenRandevular = randevu
                .Where(r => r.RandevuTarihi > DateTime.Now)
                .ToList();
            var hastalar = (from r in _context.Randevular
                            join h in _context.Hastalar on r.HastaId equals h.HastaId
                            select h).ToList();

            doktorView.Hasta = hastalar;

            doktorView.GecmisRandevular = randevu
                .Where(r => r.RandevuTarihi <= DateTime.Now)
                .ToList();
            
            return View(doktorView); // View'e döndürüyoruz
        }

        [HttpGet("branch")]
        public IActionResult DoktorGetir(int branchId)
        {
            var doctors = _context.Doktorlar
                .Where(d => d.BransId == branchId)
                .Select(d => new
                {
                    Doktorid = d.DoktorId,
                    DoktorAd = d.DoktorAd,
                    DoktorSoyad = d.DoktorSoyad
                })
                .ToList();

            if (doctors == null || !doctors.Any())
            {
                return NotFound();
            }

            return Ok(doctors);
        }
    }
}
