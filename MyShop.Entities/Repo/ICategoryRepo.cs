using MyShop.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Entities.Repo
{
    public interface ICategoryRepo:IGenericRepo<Category>
    {
        void Update(Category category);
    }
}
