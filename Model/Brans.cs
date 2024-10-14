namespace Hastane.Model
{
    public class Brans
    {
        public int BransId { get; set; }  // Primary key
        public required string BransAd { get; set; }

        // Navigation property for related Doktorlar
        public required ICollection<Doktor> Doktorlar { get; set; }
    }

}
