using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ThinkBridgeUpdated.Models;
using ThinkBridgeUpdated.Repository;

namespace ThinkBridgeUpdated.Controllers
{
    public class HomeController : Controller
    {
        //private object ProductId;
        public ActionResult GetMembers()
        {
            try
            {
                IEnumerable<MemberViewModel> members = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44331/api/");

                    //Called Member default GET All records  
                    //GetAsync to send a GET request   
                    // PutAsync to send a PUT request  
                    var responseTask = client.GetAsync("member");
                    responseTask.Wait();

                    //To store result of web api response.   
                    var result = responseTask.Result;

                    //If success received   
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<MemberViewModel>>();
                        readTask.Wait();

                        members = readTask.Result;
                    }
                    else
                    {
                        //Error response received   
                        members = Enumerable.Empty<MemberViewModel>();
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }
                }
                return View(members);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult EditProduct(int id)
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.GetResponse("api/Home/EditProduct?id=" + id.ToString());
                response.EnsureSuccessStatusCode();
                Models.MemberViewModel productediting = response.Content.ReadAsAsync<Models.MemberViewModel>().Result;
                ViewBag.Title = "Edit Product";
                return View(productediting);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //[HttpPost]  
        public ActionResult Update(Models.MemberViewModel product)
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.PutResponse("api/Home/UpdateProduct", product);
                response.EnsureSuccessStatusCode();
                return RedirectToAction("GetAllProducts");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult Details(int id)
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.GetResponse("api/Home/GetProduct?id=" + id.ToString());
                response.EnsureSuccessStatusCode();
                Models.MemberViewModel products = response.Content.ReadAsAsync<Models.MemberViewModel>().Result;
                ViewBag.Title = "All Products";
                return View(products);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.MemberViewModel product)
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.PostResponse("api/Home/InsertProduct", product);
                response.EnsureSuccessStatusCode();
                return RedirectToAction("GetAllProducts");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.DeleteResponse("api/Home/DeleteProduct?id=" + id.ToString());
                response.EnsureSuccessStatusCode();
                return RedirectToAction("GetAllProducts");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}



//localhost:44331/api/member

//localhost:44331/Home/GetMembers