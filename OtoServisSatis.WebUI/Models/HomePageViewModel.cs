using OtoServisSatis.Entities;

namespace OtoServisSatis.WebUI.Models
{
    public class HomePageViewModel
    {
        public List<Slider> Sliders { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public List<Make> Makes { get; set; }
    }
}
