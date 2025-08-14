using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OtoServisSatis.Entities;
using OtoServisSatis.Service.Abstract;

namespace OtoServisSatis.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class CustomersController : Controller
    {
        private readonly IService<Customer> _service;
        private readonly IService<Vehicle> _serviceVehicle;
        public CustomersController(IService<Customer> service, IService<Vehicle> serviceVehicle)
        {
            _service = service;
            _serviceVehicle = serviceVehicle;
        }

        // GET: CustomersController
        public async Task<ActionResult> IndexAsync()
        {
            var model = await _service.GetAllAsync();
            return View(model);
        }

        // GET: CustomersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomersController/Create
        public async Task<ActionResult> CreateAsync()
        {
            ViewBag.VehicleId = new SelectList(await  _serviceVehicle.GetAllAsync(), "Id" ,"Model");
            return View();
        }

        // POST: CustomersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.AddAsync(customer);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Bir Hata Oluştu");
                }
            }
            ViewBag.VehicleId = new SelectList(await _serviceVehicle.GetAllAsync(), "Id", "Model");
            return View(customer);
        }

        // GET: CustomersController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var model = await _service.FindAsync(id);
            ViewBag.VehicleId = new SelectList(await _serviceVehicle.GetAllAsync(), "Id", "Model");
            return View(model);
        }

        // POST: CustomersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _service.Update(customer);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Bir Hata Oluştu");
                }
            }

            ViewBag.VehicleId = new SelectList(await _serviceVehicle.GetAllAsync(), "Id", "Model");
            return View();
        }

        // GET: CustomersController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: CustomersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Customer customer)
        {
            try
            {
                _service.Delete(customer);
                _service.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
