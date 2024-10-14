namespace Hastane.Model
{
    public class Doktor
    {
        public int DoktorId { get; set; }  // Primary key
        public required string DoktorAd { get; set; }
        public required string DoktorSoyad { get; set; }
        public required string DoktorTC { get; set; }  // Unique
        public int BransId { get; set; }      // Foreign key
        public required string Telefon { get; set; }
        public required string Email { get; set; }      // Unique
        public required string Sifre { get; set; }

        // Navigation properties
        public required Brans Brans { get; set; }  // Navigation property for Brans
        public required ICollection<Randevu> Randevular { get; set; }  // Related Randevular
        public required ICollection<RandevuMusait> RandevuMusaits { get; set; }  // Related RandevuMusait
    }

}
