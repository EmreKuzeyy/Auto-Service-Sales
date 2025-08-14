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
    public class MakesController : Controller
    {
        private readonly IService<Make> _service;
        public MakesController(IService<Make> service)
        {
            _service = service;
        }
        // GET: MakesController
        public async Task<ActionResult> IndexAsync()
        {
            var model = await _service.GetAllAsync();
            return View(model);
        }

        // GET: MakesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MakesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MakesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Make make)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.AddAsync(make);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Bir Hata Oluştu");
                }
            }
            return View();

        }

        // GET: MakesController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: MakesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Make make)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    _service.Update(make);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Bir Hata Oluştu");
                }
            }

            return View();
        }

        // GET: MakesController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: MakesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Make make)
        {
            try
            {
                _service.Delete(make);
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
