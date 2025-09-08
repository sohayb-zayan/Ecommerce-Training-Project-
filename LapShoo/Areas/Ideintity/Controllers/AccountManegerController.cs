using LapShoo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyShop.Entities.Model;
using MyShop.Entities.VMs;

namespace LapShoo.Areas.Ideintity.Controllers
{
    [Area("Ideintity")]
    public class AccountManegerController : Controller
    {
        private readonly SignInManager<Useres> signInManager;
        private readonly UserManager<Useres> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly RedirectService _redirectService;

        public AccountManegerController(
            SignInManager<Useres> signInManager,
            UserManager<Useres> userManager,
            RoleManager<IdentityRole> roleManager,
            RedirectService red)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this._redirectService = red;
        }

        // GET: /Ideintity/AccountManeger/Rig
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Rig()
        {
            return View();
        }

        // POST: /Ideintity/AccountManeger/Rig
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Rig(RigVM Newuser)
        {
            if (ModelState.IsValid)
            {
                var users = new Useres
                {
                    FirstName = Newuser.FirstName,
                    LastName = Newuser.LasttName,
                    UserName = Newuser.UserName,
                    Country = Newuser.Country,
                    PhoneNumber = Newuser.PhoneNumber,
                    Email = Newuser.Email,
                };

                var result = await userManager.CreateAsync(users, Newuser.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(users, "User");

                    // تسجيل الدخول مباشرة (اختياري)
                    await signInManager.SignInAsync(users, isPersistent: false);

                    // يروح للـ Login بعد التسجيل
                    return RedirectToAction("LogIn", "AccountManeger", new { area = "Ideintity" });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(Newuser);
        }

        // GET: /Ideintity/AccountManeger/LogIn
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "Category", new { area = "Admin" });
                }
                else if (User.IsInRole("User"))
                {
                    return RedirectToAction("Index", "Home", new { area = "Customer" });
                }
                else
                {
                    var user = await userManager.GetUserAsync(User);
                    await userManager.AddToRoleAsync(user, "User");
                    return RedirectToAction("Index", "AccountManeger", new { area = "Customer" });
                }
            }
            return View();
        }

        // POST: /Ideintity/AccountManeger/LogIn
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn(LogVM log)
        {
            if (!ModelState.IsValid)
            {
                return View(log);
            }

            var user = await userManager.FindByEmailAsync(log.Email);
            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(
                    user.UserName, log.Password, log.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var (action, controller, area) = await _redirectService.GetRedirectAsync(user);
                    return RedirectToAction(action, controller, new { area });
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid Login Attempt.");
            return View(log);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("LogIn", "AccountManeger", new { area = "Ideintity" });
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
