using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.Entities.Repo;

namespace LapShoo.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = "Admin,User,Emp")]
    public class HomeController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            var product = _unitOfWork.Products.GetAll(x=>x.CategoryId!=null,x=>x.Category);
            return View(product);
        }
    }
}
