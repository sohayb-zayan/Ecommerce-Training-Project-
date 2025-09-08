using Microsoft.AspNetCore.Mvc;
using LapShoo.Models; 
using MyShop.Entities.Repo;
using MyShop.Entities.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyShop.Entities.VMs;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace LapShoo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetData(int? categoryId)
        {
            var list = new List<Product>();
            if(categoryId == null)
            list = _unitOfWork.Products.GetAll(x=>x.CategoryId != null, x=>x.Category).ToList();
            else if (categoryId.HasValue)
            {
                if (categoryId.Value == 0)
                    list = _unitOfWork.Products.GetAll(x => x.CategoryId == null, x => x.Category).ToList();
                else if(categoryId.Value == -1)
                {
                    list = _unitOfWork.Products.GetAll(null, x => x.Category).ToList();
                }
                else
                {
                    list = _unitOfWork.Products.GetAll(x=>x.CategoryId == categoryId, x => x.Category).ToList();
                }
            }
            return Json(new { data = list });
        }



        [HttpGet]
        public IActionResult Creat()
        {
            ViewBag.Categories = new SelectList(_unitOfWork.Categories.GetAll(), "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Creat(ProductVM product)
        {
            if (ModelState.IsValid)
            {
                string RootPath = _env.WebRootPath;
                if (product.ImgFile != null)
                {
                    if (product.ImgFile != null)
                    {
                        string filename = Guid.NewGuid().ToString();
                        var Upload = Path.Combine(RootPath, @"Product-img");
                        var ext = Path.GetExtension(product.ImgFile.FileName);

                        using (var filestream = new FileStream(Path.Combine(Upload, filename + ext), FileMode.Create))
                        {
                            product.ImgFile.CopyTo(filestream);
                        }
                        product.ImgUrl = "/Product-img/" + filename + ext;
                    }

                    var pro = _unitOfWork.Products.Create(product);
                    _unitOfWork.Products.Add(pro);
                    _unitOfWork.Complete();
                    TempData["Create"] = "Item has Created Successfully";
                    return RedirectToAction("Index");
                }
            }
            return View(product);
        }
        [HttpDelete]
        public IActionResult Delete(int id) 
        {
            var pro = _unitOfWork.Products.GetById(x => x.Id == id);
            if (pro == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }

            var oldimg = Path.Combine(_env.WebRootPath, pro.ImgUrl.TrimStart('/'));
            if (System.IO.File.Exists(oldimg))
            {
                System.IO.File.Delete(oldimg);
            }


            _unitOfWork.Products.Remove(pro);
            _unitOfWork.Complete();
            return Json(new { success = true, message = "Item Deleted Succssfuly" });

        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var product = _unitOfWork.Products.GetById(x=>x.Id == id,x=>x.Category);
            if (id == null | id == 0)
            {
                NotFound();
            }

            var vmpro = new ProductEditVM
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImgUrl = product.ImgUrl,   
                CategoryId = product.CategoryId ?? 0 
            };
            return View(vmpro);
        }

        [HttpPost]
        public IActionResult Edit(ProductEditVM model)
        {
            if (ModelState.IsValid)
            {
                var rootpath = _env.ContentRootPath;

                if(model.ImgFile!= null)
                {
                    string name = Guid.NewGuid().ToString();
                    var upload = Path.Combine(rootpath, @"/Product-img");
                    var ext = Path.GetExtension(model.ImgFile.FileName);

                    if (model.ImgUrl != null)
                    {
                        var oldimg = Path.Combine(rootpath, model.ImgUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldimg))
                        {
                            System.IO.File.Delete(oldimg);
                        }
                    }
                    using (var filestream = new FileStream(Path.Combine(upload, name + ext), FileMode.Create))
                    {
                        model.ImgFile.CopyTo(filestream);
                    }
                    model.ImgUrl = Path.Combine(upload, name + ext);
                }

                var pro = new Product
                {
                    Name = model.Name,
                    ImgUrl = model.ImgUrl,
                    Description = model.Description,
                    Price = model.Price,
                    CategoryId = model.CategoryId,
                };
                _unitOfWork.Products.Add(pro);
                _unitOfWork.Complete();
                TempData["Edit"] = "Item has Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        
        [HttpGet]
        public IActionResult GetPro(int id)
        {
            var pro = _unitOfWork.Products.GetById(x => x.Id == id,x=>x.Category);
            if (pro == null)
            {
                return Json(new { success = false, message = "product not found" });
            }

            return Json(new { success = true, data = pro });
        }
        
    }
}
