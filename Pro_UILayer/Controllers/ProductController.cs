using Pro_EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace Pro_UILayer.Controllers
{
    public class ProductController : Controller
    {
        HttpClient AppClient;

        public ProductController()
        {
            AppClient = new HttpClient();
            AppClient.BaseAddress = new Uri("http://localhost:50628/");
        }
        // GET: Product
        public ActionResult ShowProduct()
        {
            List<ProductModel> ProductList;
            if (Session["UserSession"] != null)
            {

                try
                {
                    //Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt.Value);
                  
                    AppClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["UserSession"].ToString());
                    
                    var res = AppClient.GetAsync("ShowProducts").Result;

                    if (res.IsSuccessStatusCode)
                    {
                        var dat = res.Content.ReadAsAsync<List<ProductModel>>().Result;
                        ProductList = dat;
                        ViewBag.r = ProductList;

                        return View("Showproduct",ProductList);

                    }
                    else if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        TempData["Err"] = "UnAthorized to access resource";
                        return RedirectToAction("login", "account");
                    }
                }
                catch (Exception ex)
                {
                    TempData["Err"] = ex.Message;
                }
            }
            else
            {
                return RedirectToAction("Login","Account");
            }
            return View();
        }

        public ActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(ProductModel NewProduct)
        {           
            if (Session["UserSession"] != null)
            {

                try
                {
                    //Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt.Value);

                    AppClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["UserSession"].ToString());

                    var res = AppClient.PostAsJsonAsync<ProductModel>("Addproduct", NewProduct).Result;

                    if (res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("showproducts");

                    }
                    else if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        TempData["Err"] = "UnAthorized to access resource";
                        return RedirectToAction("login", "account");
                    }
                }
                catch (Exception ex)
                {
                    TempData["Err"] = ex.Message;
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
    }
}