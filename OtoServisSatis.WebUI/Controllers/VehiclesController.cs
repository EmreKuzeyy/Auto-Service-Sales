using Microsoft.AspNetCore.Mvc;
using OtoServisSatis.Entities;
using OtoServisSatis.Service.Abstract;
using OtoServisSatis.WebUI.Models;
using OtoServisSatis.WebUI.Utils;
using System.Security.Claims;

namespace OtoServisSatis.WebUI.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly IVehicleService _service;
        private readonly IService<Customer> _serviceCustomer;
        private readonly IUserService _serviceUser;
        public VehiclesController(IVehicleService service, IService<Customer> serviceCustomer, IUserService serviceUser)
        {
            _service = service;
            _serviceCustomer = serviceCustomer;
            _serviceUser = serviceUser;
        }
        public async Task<IActionResult> IndexAsync(int? id)
        {
            if (id == null) 
            return BadRequest();
               
            var vehicle = await _service.GetVehicleById(id.Value);
            if (vehicle == null)
                return NotFound();

            var model = new VehicleDetailViewModel();
            model.Vehicle = vehicle;
            if (User.Identity.IsAuthenticated)
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                var uguid = User.FindFirst(ClaimTypes.UserData)?.Value;
                if (!string.IsNullOrEmpty(email) || !string.IsNullOrEmpty(uguid))
                {
                    var user = _serviceUser.Get(u => u.Email == email && u.UserGuid.ToString() == uguid);
                    if (user is not null)
                    {
                        model.Customer = new Customer
                        {
                            Name = user.Name,
                            Surname = user.Surname,
                            Email = user.Email,
                            Phone = user.Phone
                        };
                    }
                }
            }

            

            return View(model);
        }

        [Route("tum-araclarimiz")]
        public async Task<IActionResult> List()
        {
            var model = await _service.GetCustomVehicleList(v=> v.IsSale);
            return View(model);
        }
        public async Task<IActionResult> Search(string q)
        {
            var model = await _service.GetCustomVehicleList
                (v => v.IsSale && v.Make.Name.Contains(q) || v.BodyType.Contains(q) || v.Model.Contains(q));
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> CustomerRegister(Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _serviceCustomer.AddAsync(customer);
                    await _serviceCustomer.SaveAsync();

                    var vehicle = await _service.FindAsync(customer.VehicleId);

                    await MailHelper.SendMailAsync(customer, vehicle, Request);

                    TempData["Message"] =
                        $"<div class='alert alert-success alert-dismissible fade show' role='alert'>Sayın {customer.Name} {customer.Surname}, talebiniz işleme alınmıştır.<button type='button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";

                    return Redirect("/Vehicles/Index/" + customer.VehicleId);
                }
                catch
                {
                    TempData["Message"] =
                        "<div class='alert alert-danger alert-dismissible fade show' role='alert'>Sayın kullanıcımız, bir hata oluştu. Lütfen tekrar deneyiniz.<button type='button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                    ModelState.AddModelError("", "Bir Hata Oluştu");
                }
            }

            return View();
        }

    }
}
