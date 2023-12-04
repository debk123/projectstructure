using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Pro_EntityLayer;
namespace Pro_UILayer.Controllers
{
    public class AccountController : Controller
    {
        HttpClient AppClient;
        HttpCookie cook;
        public AccountController()
        {
            AppClient = new HttpClient();
            AppClient.BaseAddress = new Uri("http://localhost:50628/");
            TempData["login"]= "";
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            Request.Cookies.Clear();
            cook = null;
            //Request.Cookies["jwtCookie"].Expires = DateTime.Now.AddSeconds(0);
            TempData["login"] = "enabled";
            return RedirectToAction("Login", "Account");
        }



        // GET: Account

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserModel User)
        {
            string tk;
            try
            {
                var res = AppClient.PostAsJsonAsync<UserModel>("login", User).Result;

                if (res.IsSuccessStatusCode)
                {
                    var dat = res.Content.ReadAsAsync<string>().Result;
                    tk = dat;
                    StoreToken(tk);
                    TempData["login"] = "disabled";
                    return RedirectToAction("simp", "Account");
                }
                else
                {
                    TempData["Err"] = "invalid User";
                }
            }
            catch (Exception ex)
            {
                TempData["Err"] = ex.Message;
            }
            return View();

           }

        private void StoreToken(string token)
        {
            cook = new HttpCookie("jwtCookie");
            cook.Value = token;
            cook.Expires = DateTime.Now.AddDays(2);
            Session["UserSession"] = cook.Value;
            //d = new HttpApplication();
            // d.Context.Application["dt"] = cook.Value;
            Response.Cookies.Add(cook);
        }


        [HttpGet]
        public ActionResult Simp()
        {
            // var jwt = Request.Cookies["jwtCookie"];

            // string s = "";
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

                        return View(ProductList);

                    }
                    else if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        TempData["Err"] = "UnAthorized to access resource";
                        return RedirectToAction("login");
                    }
                }
                catch (Exception ex)
                {
                    TempData["Err"] = ex.Message;
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }

    }
}