using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pro_EntityLayer;
namespace Pro_DAL.Repo
{
    public interface IProduct
    {
        IEnumerable<ProductModel> GetProducts();
        ProductModel GetProduct(int id);
        void AddProduct(ProductModel NewRecord);
    }
}
