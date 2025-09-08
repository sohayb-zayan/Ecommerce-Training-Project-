using MyShop.Entities.Model;
using MyShop.Entities.VMs;

namespace MyShop.Entities.Repo
{
    public interface IProductRepo:IGenericRepo<Product>
    {
        void Update(Product product);
        Product Create(ProductVM pvm);
    }
}
