namespace Hastane.Model
{
    public class Randevu
    {
        public int RandevuId { get; set; }  // Primary key
        public int HastaId { get; set; }    // Foreign key
        public int DoktorId { get; set; }    // Foreign key
        public DateTime _randevuTarihi;
        public DateTime RandevuTarihi
    {
        get { return _randevuTarihi; }
        set
        {
            _randevuTarihi = value;
            // Randevu saatini güncelle
            RandevuSaati = value.TimeOfDay; // Randevu tarihinin saatini al
        }
    }
        public TimeSpan RandevuSaati { get; set; }
        public bool OnayDurumu { get; set; }  // 0: Bekliyor, 1: Onaylandı

        // Navigation properties
        public Hasta Hasta { get; set; }  // Related Hasta
        public Doktor Doktor { get; set; }  // Related Doktor
    }

}
