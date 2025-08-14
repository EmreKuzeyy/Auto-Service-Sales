using Microsoft.AspNetCore.Mvc;
using OtoServisSatis.Entities;
using OtoServisSatis.Service.Abstract;
using OtoServisSatis.WebUI.Models;
using System.Diagnostics;

namespace OtoServisSatis.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService<Slider> _service;
        private readonly IService<Vehicle> _serviceVehicle;
        private readonly IService<Make> _serviceMake;

        public HomeController(IService<Slider> service, IService<Vehicle> serviceVehicle, IService<Make> serviceMake)
        {
            _service = service;
            _serviceVehicle = serviceVehicle;
            _serviceMake = serviceMake;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var model = new HomePageViewModel()
            {
                Sliders = await _service.GetAllAsync(),
                Vehicles = await _serviceVehicle.GetAllAsync(v=> v.HomePage),
                Makes = await _serviceMake.GetAllAsync()
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
