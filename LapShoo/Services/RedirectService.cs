using Microsoft.AspNetCore.Identity;
using MyShop.Entities.Model;

namespace LapShoo.Services
{
    public class RedirectService
    {
        private readonly UserManager<Useres> _userManager;

        public RedirectService(UserManager<Useres> userManager)
        {
            _userManager = userManager;
        }

        public async Task<(string action, string controller, string area)> GetRedirectAsync(Useres user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("Admin"))
            {
                return ("Index", "Category", "Admin");
            }
            else if (roles.Contains("Emp"))
            {
                return ("Index", "Category", "Admin");
            }
            else
            {
                return ("Index", "Home", "Customer");
            }
        }
    }
}
