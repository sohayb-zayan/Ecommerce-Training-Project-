using MyData;
using MyShop.Entities.Model;
using MyShop.Entities.Repo;
using System;

namespace MyDataAcc.Implementation
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;

        
        public ICategoryRepo Categories { get; }

        public IProductRepo Products { get; }

      

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Categories = new CategoryRepo(context);
            Products= new ProductRepo(context);
        }


            public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
