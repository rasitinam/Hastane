namespace Hastane.Model
{
    public class RandevuMusait
    {
        public int RandevuMusaitId { get; set; }  // Primary key
        public int DoktorId { get; set; }          // Foreign key
        public DateTime MusaitTarih { get; set; }
        public TimeSpan MusaitSaat { get; set; }
        public bool Durum { get; set; }  // 1: Uygun, 0: Dolu

        // Navigation property
        public required Doktor Doktor { get; set; }  // Related Doktor
    }

}
