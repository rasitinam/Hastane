namespace Hastane.Model
{
    public class Sekreter
    {
        public int Sekreterid { get; set; }  // Primary key
        public required string Sekreterad { get; set; }
        public required string Sekretersoyad { get; set; }
        public required string SekreterTC { get; set; }  // Unique
        public required string Telefon { get; set; }
        public required string Email { get; set; }      // Unique
        public required string Sifre { get; set; }
    }

}
