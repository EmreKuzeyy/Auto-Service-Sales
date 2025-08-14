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
    public class SalesController : Controller
    {
        private readonly IService<Sale> _service;
        private readonly IService<Vehicle> _serviceVehicle;
        private readonly IService<Customer> _serviceCustomer;
        public SalesController(IService<Sale> service, IService<Vehicle> serviceVehicle, IService<Customer> serviceCustomer)
        {
            _service = service;
            _serviceVehicle = serviceVehicle;
            _serviceCustomer = serviceCustomer;
        }

        // GET: SalesController
        public async Task<ActionResult> IndexAsync()
        {
            var model = await _service.GetAllAsync();
            return View(model);
        }

        // GET: SalesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SalesController/Create
        public async Task<ActionResult> CreateAsync()
        {
            ViewBag.VehicleId = new SelectList((await _serviceVehicle.GetAllAsync()).Select(v => new{v.Id,DisplayName = $"{v.Model} - {v.Colour} - {v.BodyType} - {v.ModelYear}"}),"Id","DisplayName");
            ViewBag.CustomerId = new SelectList((await _serviceCustomer.GetAllAsync()).Select(c => new { c.Id, FullName = c.Name + " " + c.Surname }), "Id", "FullName");
            return View();
        }

        // POST: SalesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Sale sale)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.AddAsync(sale);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Bir Hata Oluştu");
                }
            }
            ViewBag.VehicleId = new SelectList((await _serviceVehicle.GetAllAsync()).Select(v => new{ v.Id,DisplayName = $"{v.Model} - {v.Colour} - {v.BodyType} - {v.ModelYear}"}),"Id","DisplayName");
            ViewBag.CustomerId = new SelectList((await _serviceCustomer.GetAllAsync()).Select(c => new { c.Id, FullName = c.Name + " " + c.Surname }),"Id","FullName",sale.CustomerId);
            return View(sale);
        }

        // GET: SalesController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var model = await _service.FindAsync(id);
            ViewBag.VehicleId = new SelectList((await _serviceVehicle.GetAllAsync()).Select(v => new{v.Id,DisplayName = $"{v.Model} - {v.Colour} - {v.BodyType} - {v.ModelYear}"}),"Id","DisplayName");
            ViewBag.CustomerId = new SelectList((await _serviceCustomer.GetAllAsync()).Select(c => new { c.Id, FullName = c.Name + " " + c.Surname }),"Id","FullName",model.CustomerId);
            return View(model);
        }

        // POST: SalesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id,Sale sale)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _service.Update(sale);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Bir Hata Oluştu");
                }
            }

            ViewBag.VehicleId = new SelectList((await _serviceVehicle.GetAllAsync()).Select(v => new { v.Id, DisplayName = $"{v.Model} - {v.Colour} - {v.BodyType} - {v.ModelYear}" }), "Id", "DisplayName");
            ViewBag.CustomerId = new SelectList((await _serviceCustomer.GetAllAsync()).Select(c => new { c.Id, FullName = c.Name + " " + c.Surname }), "Id", "FullName", sale.CustomerId);
            return View();
        }

        // GET: SalesController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: SalesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id,Sale sale)
        {
            try
            {
                _service.Delete(sale);
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
