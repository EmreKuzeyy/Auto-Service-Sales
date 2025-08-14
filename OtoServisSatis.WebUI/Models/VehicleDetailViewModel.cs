using OtoServisSatis.Entities;

namespace OtoServisSatis.WebUI.Models
{
    public class VehicleDetailViewModel
    {
        public Vehicle Vehicle { get; set; }
        public Customer? Customer { get; set; }
    }
}
