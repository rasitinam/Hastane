using Hastane.Model;
namespace Hastane.ViewModel
{
    public class HastaViewModel
    {
        public List<Randevu> BekleyenRandevular { get; set; } = new List<Randevu> { };
        public List<Randevu> GecmisRandevular { get; set; } = new List<Randevu> { };
        public List<Hasta> Hasta { get; set; } = new List<Hasta> { };
        public List<Doktor> Doktors { get; set; } = new List<Doktor> { };

        public List<Brans> Brans {  get; set; } = new List<Brans>();
        public string? ErrorMessage {get ; set;} 
    }
}
