using MyData;
using MyShop.Entities.Model;
using MyShop.Entities.Repo;
using MyShop.Entities.VMs;

namespace MyDataAcc.Implementation
{
    public class ProductRepo : GenericRepo<Product>, IProductRepo
    {
        private readonly AppDbContext _context;
        public ProductRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }


        public Product Create(ProductVM vm)
        {
            var newProduct = new Product
            {
                Name = vm.Name,
                Description = vm.Description,
                Price = vm.Price,
                CategoryId = vm.CategoryId,
                ImgUrl = vm.ImgUrl
            };
            return newProduct;
        }

        public void Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
