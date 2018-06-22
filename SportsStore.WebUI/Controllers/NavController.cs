using SportsStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        // GET: Nav
        //public ActionResult Index()
        //{
        //    return View();
        //}

        #region Constructor with DI for Repository
        private IProductRepository repository;
        public NavController(IProductRepository repo)
        {
            repository = repo;
        } 
        #endregion



        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = repository.Products
                                                        .Select(c => c.Category)
                                                        .Distinct()
                                                        .OrderBy(c => c);
            return PartialView(categories);
        }
    }
}