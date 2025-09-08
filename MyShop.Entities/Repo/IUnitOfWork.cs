using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Entities.Repo
{
    public interface IUnitOfWork
    {
        public ICategoryRepo Categories { get; }
        public IProductRepo Products { get; }
        int Complete();
    }
}
