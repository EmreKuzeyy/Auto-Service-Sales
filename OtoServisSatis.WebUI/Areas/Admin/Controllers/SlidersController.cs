using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OtoServisSatis.Entities;
using OtoServisSatis.Service.Abstract;
using OtoServisSatis.WebUI.Utils;

namespace OtoServisSatis.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class SlidersController : Controller
    {
        private readonly IService<Slider> _service;

        public SlidersController(IService<Slider> service)
        {
            _service = service;
        }

        // GET: SlidersController
        public async Task<ActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        // GET: SlidersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SlidersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SlidersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Slider slider , IFormFile Photo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    slider.Photo = await FileHelper.FileLoaderAsync(Photo, filePath: "/Img/Slider/");
                    await _service.AddAsync(slider);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Bir Hata Oluştu");
                }
            }
            return View(slider);
        }

        // GET: SlidersController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: SlidersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id,Slider slider, IFormFile? Photo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingSlider = await _service.FindAsync(id);
                    if (existingSlider == null) return NotFound();

                    if (Photo != null && Photo.Length > 0)
                    {
                        existingSlider.Photo = await FileHelper.FileLoaderAsync(Photo, "/Img/Slider/");
                    }

                    existingSlider.Description = slider.Description;
                    existingSlider.Title = slider.Title;
                    existingSlider.Link = slider.Link;

                    _service.Update(existingSlider);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Bir Hata Oluştu");
                }
            }
            return View(slider);
        }
    

        // GET: SlidersController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: SlidersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var slider = await _service.FindAsync(id);
                if (slider != null)
                {
                    FileHelper.DeleteFile(slider.Photo!, "/Img/Slider/");

                    _service.Delete(slider);
                    await _service.SaveAsync();

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
