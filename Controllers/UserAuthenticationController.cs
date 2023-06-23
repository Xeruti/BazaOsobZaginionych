using BazaOsobZaginionych.Models.DTO;
using BazaOsobZaginionych.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BazaOsobZaginionych.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private readonly IUserAuthenticationService _service;
        public UserAuthenticationController(IUserAuthenticationService service)
        {
            this._service = service;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Login(Login model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _service.LoginAsync(model);
            if (result.StatusCode == 1)
            {
                return RedirectToAction("Display", "Dashboard");
            }
            else
            {
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Login));
            }
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Registration(RegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.Role = "user";
            var result = await this._service.RegisterAsync(model);
            TempData["msg"] = result.Message;
            return RedirectToAction(nameof(Registration));
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
          await _service.LogoutAsync();
          return RedirectToAction(nameof(Login));
        }

        
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAdmin()
        {
            var model = new RegistrationModel
            {
                Username = "admin",
                FirstName = "Przemysław",
                LastName = "Kalinowski",
                Email = "email@gmail.com",
                Password = "Admin1234!"
            };
            Console.Write(model.Username);
            model.Role = "admin";
            var result = await _service.RegisterAsync(model);
            return Ok(result);

        }


    }
}
