namespace Hastane.Model
{
    public class Hasta
    {
        public int HastaId { get; set; }  // Primary key
        public required string HastaAd { get; set; }
        public required string HastaSoyad { get; set; }
        public required string TcKimlikNo { get; set; }  // Unique
        public required string Telefon { get; set; }      // Unique
        public required string Email { get; set; }        // Unique
        public required string Adres { get; set; }
        public DateTime? DogumTarihi { get; set; }  // Nullable
        public  string? Sifre { get; set; }

        // Navigation property for related Randevular
        public required ICollection<Randevu> Randevular { get; set; }
    }

}
