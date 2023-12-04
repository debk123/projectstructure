using Pro_EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pro_DAL.Repo
{
    public class ProductDataService : IProduct
    {
        DecDb23NewEntities ProDb;
        ProductModel PModel;
        List<ProductModel> ProductModelList;

        public ProductDataService()
        {
            ProDb = new DecDb23NewEntities();
            ProductModelList = new List<ProductModel>();
            PModel = new ProductModel();
        }
        public ProductModel GetProduct(int id)
        {
            try
            {
                var ProductData = ProDb.Products.Find(id);

                if (ProductData != null)
                {
                    PModel.Pid = ProductData.Pid;
                    PModel.Pname = ProductData.Pname;

                    return PModel;
                }
                else
                {
                    throw new Exception("product not found");
                }

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddProduct(ProductModel NewProduct)
        {
            try
            {
                Product p = new Product();
              
                p.Pid = NewProduct.Pid;
                p.Pname = NewProduct.Pname;


                ProDb.Products.Add(p);
                ProDb.SaveChanges();
             
               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<ProductModel> GetProducts()
        {
            try
            {
                var ProductList = ProDb.Products.ToList();

                // to copy list<product> into list<productMOdel>
                foreach (var item in ProductList)
                {
                    PModel.Pid = item.Pid;
                    PModel.Pname = item.Pname;

                    ProductModelList.Add(PModel);

                }
                return ProductModelList;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
