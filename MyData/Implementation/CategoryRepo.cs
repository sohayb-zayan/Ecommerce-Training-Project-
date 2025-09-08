using MyData;
using MyShop.Entities.Model;
using MyShop.Entities.Repo;

using System;

namespace MyDataAcc.Implementation
{
    
    public class CategoryRepo: GenericRepo<Category>, ICategoryRepo
    {
        private readonly AppDbContext _context;

        public CategoryRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Category category)
        {
            var categoryInDb = _context.Categories.Find(category.Id);
            if (categoryInDb != null)
            {
                categoryInDb.Name = category.Name;
                categoryInDb.Description = category.Description;
                categoryInDb.CreatedTime = DateTime.Now;
            }
        }
    }
}
