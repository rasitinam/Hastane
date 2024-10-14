using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Hastane.Model
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options)
        {
        }
        

        public DbSet<Hasta> Hastalar { get; set; }
        public DbSet<Brans> Branslar { get; set; }
        public DbSet<Doktor> Doktorlar { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<RandevuMusait> RandevuMusait { get; set; }
        public DbSet<Sekreter> Sekreterler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API ile model yapılandırmaları burada yapılabilir.

            base.OnModelCreating(modelBuilder);
        }
    }

}
