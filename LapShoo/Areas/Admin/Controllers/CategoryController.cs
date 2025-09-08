using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyData;
using MyShop.Entities.Model;
using MyShop.Entities.Repo;

namespace LapShoo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
              
                return View();
        }

        [HttpGet]
        public IActionResult GetData()
        {
            var list = new List<Category>();
            list = _unitOfWork.Categories.GetAll().ToList();
            return Json(new { data = list }); 
        }

        [HttpGet]
        public IActionResult Creat()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveCreat(Category category)
        {
            if(ModelState.IsValid )
            {
                _unitOfWork.Categories.Add(category);
                _unitOfWork.Complete();
                TempData["Create"] = "Item has Created Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpDelete] 
        public IActionResult Delete(int id)
        {
            var categoryInDb = _unitOfWork.Categories.GetById(x => x.Id == id);
            if (categoryInDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }

            _unitOfWork.Categories.Remove(categoryInDb);
            _unitOfWork.Complete();

            return Json(new { success = true, message = "Category has been Deleted" });
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var category = _unitOfWork.Categories.GetById(x => x.Id == id);
            if (category == null)
                return NotFound();
            return Json(category);
        }

        [HttpPost]
        public IActionResult Edit(Category model)
        {
            if (model == null)
                return Json(new { success = false, message = "Invalid data" });

            var categoryInDb = _unitOfWork.Categories.GetById(x => x.Id == model.Id);
            if (categoryInDb == null)
                return Json(new { success = false, message = "all failds are requerd" });

            categoryInDb.Name = model.Name;
            categoryInDb.Description = model.Description;

            _unitOfWork.Categories.Update(categoryInDb);
            _unitOfWork.Complete();
            return Json(new { success = true, message = "Category updated successfully" });
        }
    }
}
