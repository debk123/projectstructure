using Pro_DAL.Repo;
using Pro_EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Pro_WebApiLayer.Controllers
{
    [Authorize(Roles ="customer,admin")]
    public class ProductWebApiController : ApiController
    {
        IProduct ProdService;

        public ProductWebApiController()
        {
            ProdService = new ProductDataService();
        }

        [HttpGet]
        [Route("ShowProducts")]
        public IHttpActionResult ShowProducts()
        {
            try
            {
                var ProductList = ProdService.GetProducts();

                if (ProductList != null)
                {
                    return Ok(ProductList);
                }
                else
                {
                    return BadRequest("empty record/Add new Record");
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("AddProduct")]
        public IHttpActionResult AddProduct(ProductModel NewProduct)
        {
            try
            {
                ProdService.AddProduct(NewProduct);
              
                 return Ok("record added");
              
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
