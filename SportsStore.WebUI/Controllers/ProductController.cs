using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        #region Init and constructor
        private IProductRepository repository;
        public int pageSize = 4;
        public ProductController(IProductRepository repo)
        {
            this.repository = repo;
        } 
        #endregion

        public ViewResult List(int? id, string category, int page = 1)
        {
            //IEnumerable<Product> iProduct = (id == null ? repository.Products : repository.Products.Where(p => p.ProductID == id.Value));
            IEnumerable<Product> iProduct = repository.Products
                                                        .Where(p => id == null|| p.ProductID == id)
                                                        .Where(p => category == null || p.Category == category);

            PagingInfo pInfo = new PagingInfo {
                CurrentPage = page,
                ItemsPerPage = pageSize,
                TotalItems = iProduct.Count()
            };


            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = iProduct
                                .OrderBy(p => p.ProductID)
                                .Skip((pInfo.CurrentPage - 1) * pInfo.ItemsPerPage)
                                .Take(pInfo.ItemsPerPage),
                PagingInfo = pInfo,
                CurrentCategory = category
            };
            return View(model);
        }

        #region Dummy Test
        public ViewResult ListHighEnd(int? id, int page = 1)
        {
            IEnumerable<ProductHighEnd> iProductHighEnd = (id == null ? repository.ProductHighEnds : repository.ProductHighEnds.Where(p => p.ProductHighEndID == id.Value));
            return View(iProductHighEnd.OrderBy(p => p.ProductHighEndID).Skip((page - 1) * pageSize).Take(pageSize));
        } 
        #endregion

    }
}