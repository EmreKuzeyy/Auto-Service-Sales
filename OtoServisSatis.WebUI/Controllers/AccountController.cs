using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtoServisSatis.Entities;
using OtoServisSatis.Service.Abstract;
using OtoServisSatis.Service.Concrete;
using OtoServisSatis.WebUI.Models;
using System.Security.Claims;

namespace OtoServisSatis.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _service;
        private readonly IService<Role> _serviceRole;

        public AccountController(IUserService userService, IService<Role> serviceRole)
        {
            _service = userService;
            _serviceRole = serviceRole;
        }

        [Authorize(Policy = "CustomerPolicy")]
        public IActionResult Index()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var uguid = User.FindFirst(ClaimTypes.UserData)?.Value;
            if (!string.IsNullOrEmpty(email) || !string.IsNullOrEmpty(uguid))
            {
                var user = _service.Get(u => u.Email == email && u.UserGuid.ToString() == uguid);
                if (user is not null)
                {
                    return View(user);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult UserUpdate(User user)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                var uguid = User.FindFirst(ClaimTypes.UserData)?.Value;
                if (!string.IsNullOrEmpty(email) || !string.IsNullOrEmpty(uguid))
                {
                    var userLogin = _service.Get(u => u.Email == email && u.UserGuid.ToString() == uguid);
                    if (userLogin is not null)
                    {
                        userLogin.Name = user.Name;
                        userLogin.Surname = user.Surname;
                        userLogin.Email = user.Email;
                        userLogin.Password = user.Password;
                        userLogin.DateAdded = user.DateAdded;
                        userLogin.IsActive = user.IsActive;
                        userLogin.Phone = user.Phone;
                        
                        _service.Update(userLogin);
                        _service.Save();
                    }
                }
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Hata Oluştu!");
            }
            return RedirectToAction("Index");
        }

        public IActionResult Register()
        {
           return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _serviceRole.GetAsync(r => r.Name == "Customer");
                    if (role == null)
                    {
                        ModelState.AddModelError("", "Kayıt Başarısız");
                        return View();
                    }
                    user.RoleId = role.Id;
                    user.IsActive = true;
                    await _service.AddAsync(user);
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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(CustomerLoginViewModel customerLogin)
        {
            try
            {
                var account = await _service
                    .GetAsync(u => u.Email == customerLogin.Email && u.Password == customerLogin.Password && u.IsActive == true);
                if (account == null)
                {
                    ModelState.AddModelError("", "Giriş Başarısız");
                }
                else
                {
                    var role = _serviceRole.Get(r => r.Id == account.RoleId);
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, account.Name),
                        new Claim(ClaimTypes.Email, account.Email),
                        new Claim(ClaimTypes.UserData, account.UserGuid.ToString())
                    };
                    if (role is not null)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    }
                    var userIdentity = new ClaimsIdentity(claims, "Login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);
                    if(role.Name == "Admin")
                    {
                        return Redirect("/Admin");
                    }
                    return Redirect("/Home/Index");

                }
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Giriş Başarısız");
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

    }
}
