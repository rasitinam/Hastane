using Microsoft.AspNetCore.Mvc;

namespace Hastane.Controller
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using global::Hastane.Model;

    namespace Hastane.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class DoctorsController : ControllerBase
        {
            private readonly HospitalDbContext _context;

            public DoctorsController(HospitalDbContext context)
            {
                _context = context;
            }

            // GET api/doctors?branchId={branchId}
            [HttpGet]
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

}
