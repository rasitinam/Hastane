using Hastane.Model;
using Hastane.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Hastane.Controller
{
    public class LoginController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly HospitalDbContext _context;

        public LoginController(HospitalDbContext context)
        {
            var model = new LoginViewModel();
            _context = context;
        }
        public IActionResult Index(LoginViewModel model)
        {
            return View(model);
        }
        [HttpPost]
        public IActionResult Submit(LoginViewModel model, string role, string tc, string password)
        {
            if (role == "doktor")
            {
                var d = _context.Doktorlar.FirstOrDefault(d => d.DoktorTC == tc && d.Sifre == password);
                if (d != null)
                {
                    HttpContext.Session.SetInt32("DoktorId", d.DoktorId);
                    return RedirectToAction("DoktorHome", "Doktor", new { doktorid = d.DoktorId });
                }
                else
                {
                    model.ErrorMessage = "TC kimlik numarası veya şifre yanlış.";
                    return View("Index", model);
                }
            }
            else if (role == "hasta")
            {
                var h = _context.Hastalar.FirstOrDefault(h => h.TcKimlikNo == tc && h.Sifre == password);
                if (h != null)
                {
                    HttpContext.Session.SetInt32("HastaId", h.HastaId);
                    return RedirectToAction("Home", "Hasta", new { hastaid = h.HastaId });
                }
                else
                {
                    model.ErrorMessage = "TC kimlik numarası veya şifre yanlış.";
                    return View("Index", model);
                }
            }
            else if (role == "sekreter")
            {
                // Sekreter için işlem yapabilirsiniz
                return View("SekreterView"); // Örnek bir görünüm
            }
            else
            {
                model.ErrorMessage = "Geçersiz rol seçimi.";
                return View("Index", model);
            }
        }

    }
}
