using Microsoft.AspNetCore.Mvc;
using OtoServisSatis.Entities;
using OtoServisSatis.Service.Abstract;
using OtoServisSatis.WebUI.ExtensionMethods;

namespace OtoServisSatis.WebUI.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly IVehicleService _serviceVehicle;
        public FavoritesController(IVehicleService service)
        {
            _serviceVehicle = service;
        }
        public IActionResult Index()
        {
            var favorites = GetFavorites();
            return View(favorites);
        }

        private List<Vehicle> GetFavorites()
        {
            List<Vehicle>? vehicles = new();
            vehicles = HttpContext.Session.GetJson<List<Vehicle>>("GetFavorites");
            return vehicles ?? new List<Vehicle>();
        }

        public IActionResult Add(int VehicleId)
        {
            try
            {
                var vehicle = _serviceVehicle.Find(VehicleId);
                if (vehicle != null)
                {
                    var favorites = GetFavorites();

                    if (!favorites.Any(f => f.Id == VehicleId))
                    {
                        favorites.Add(vehicle);
                        HttpContext.Session.SetJson("GetFavorites", favorites);
                    }
                }
            }
            catch (Exception )
            {
                TempData["Message"] = "<div class = 'alert alert-danger'>Hata Oluştu!</div>";
            }

            return RedirectToAction("Index");
        }

        public IActionResult Remove(int VehicleId)
        {
            try
            {
                var vehicle = _serviceVehicle.Find(VehicleId);
                if (vehicle != null)
                {
                    var favorites = GetFavorites();

                    if (favorites.Any(f => f.Id == VehicleId))
                    {
                        favorites.RemoveAll(f => f.Id == VehicleId);
                        HttpContext.Session.SetJson("GetFavorites", favorites);
                    }
                }
            }
            catch (Exception)
            {
                TempData["Message"] = "<div class = 'alert alert-danger'>Hata Oluştu!</div>";
            }

            return RedirectToAction("Index");
        }
    }
}
