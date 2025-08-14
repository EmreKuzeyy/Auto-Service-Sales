using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OtoServisSatis.Entities;
using OtoServisSatis.Service.Abstract;
using OtoServisSatis.Service.Concrete;
using OtoServisSatis.WebUI.Utils;

namespace OtoServisSatis.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class VehiclesController : Controller
    {
        private readonly IVehicleService _service;
        private readonly IService<Make> _serviceMake;

        public VehiclesController(IVehicleService service, IService<Make> serviceMake)
        {
            _service = service;
            _serviceMake = serviceMake;
        }
        // GET: VehiclesController
        public async Task<ActionResult> IndexAsync()
        {
            var model = await _service.GetCustomVehicleList();
            return View(model);
        }

        // GET: VehiclesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VehiclesController/Create
        public async Task<ActionResult> CreateAsync()
        {
            ViewBag.MakeId = new SelectList(await _serviceMake.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: VehiclesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Vehicle vehicle, IFormFile Photo1, IFormFile? Photo2, IFormFile? Photo3)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    vehicle.Photo1 = await FileHelper.FileLoaderAsync(Photo1, filePath:"/Img/Vehicles/");
                    vehicle.Photo2 = await FileHelper.FileLoaderAsync(Photo2, filePath:"/Img/Vehicles/");
                    vehicle.Photo3 = await FileHelper.FileLoaderAsync(Photo3, filePath:"/Img/Vehicles/");
                    await _service.AddAsync(vehicle);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Bir Hata Oluştu");
                }
            }
            ViewBag.MakeId = new SelectList (await _serviceMake.GetAllAsync(), "Id", "Name");
            return View(vehicle);
        }

        // GET: VehiclesController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var model = await _service.FindAsync(id);
            ViewBag.MakeId = new SelectList(await _serviceMake.GetAllAsync(), "Id", "Name");
            return View(model);
        }

        // POST: VehiclesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Vehicle vehicle, IFormFile? Photo1, IFormFile? Photo2, IFormFile? Photo3)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Veritabanından mevcut entity'yi al
                    var existingVehicle = await _service.FindAsync(id);
                    if (existingVehicle == null) return NotFound();

                    // Eğer yeni fotoğraf yüklenmediyse, eski fotoğraf yolunu koru
                    if (Photo1 != null && Photo1.Length > 0)
                    {
                        existingVehicle.Photo1 = await FileHelper.FileLoaderAsync(Photo1, "/Img/Vehicles/");
                    }
                    // Yeni fotoğraf yoksa, eski fotoğrafı koru
                    // (else yok, zaten existingVehicle.Photo1 mevcut)

                    if (Photo2 != null && Photo2.Length > 0)
                    {
                        existingVehicle.Photo2 = await FileHelper.FileLoaderAsync(Photo2, "/Img/Vehicles/");
                    }

                    if (Photo3 != null && Photo3.Length > 0)
                    {
                        existingVehicle.Photo3 = await FileHelper.FileLoaderAsync(Photo3, "/Img/Vehicles/");
                    }

                    // Diğer alanları da güncelle
                    existingVehicle.MakeId = vehicle.MakeId;
                    existingVehicle.Colour = vehicle.Colour;
                    existingVehicle.Price = vehicle.Price;
                    existingVehicle.Model = vehicle.Model;
                    existingVehicle.BodyType = vehicle.BodyType;
                    existingVehicle.ModelYear = vehicle.ModelYear;
                    existingVehicle.IsSale = vehicle.IsSale;
                    existingVehicle.Notes = vehicle.Notes;
                    existingVehicle.HomePage = vehicle.HomePage;

                    _service.Update(existingVehicle);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Bir Hata Oluştu");
                }
            }
            ViewBag.MakeId = new SelectList(await _serviceMake.GetAllAsync(), "Id", "Name");
            return View(vehicle);
        }

        // GET: VehiclesController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: VehiclesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var vehicle = await _service.FindAsync(id);
                if (vehicle != null)
                {
                    // Fotoğraf dosyalarını sil
                    FileHelper.DeleteFile(vehicle.Photo1!);
                    FileHelper.DeleteFile(vehicle.Photo2!);
                    FileHelper.DeleteFile(vehicle.Photo3!);

                    // Veritabanından aracı sil
                    _service.Delete(vehicle);
                    _service.Save();

                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }
            catch
            {
                return View();
            }
        }
    }
}
