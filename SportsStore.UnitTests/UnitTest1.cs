using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using System.Web.Mvc;
using SportsStore.WebUI.Models;
using SportsStore.WebUI.HtmlHelpers;


namespace SportsStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            ///Setup
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[] {
                    new Product { ProductID = 1, Name = "Product ID 1" },
                    new Product { ProductID = 2, Name = "Product ID 2"},
                    new Product { ProductID = 3, Name = "Product ID 3"},
                    new Product { ProductID = 4, Name = "Product ID 4"},
                    new Product { ProductID = 5, Name = "Product ID 5"},
                    new Product { ProductID = 6, Name = "Product ID 6"},
                    new Product { ProductID = 7, Name = "Product ID 7"},
                    new Product { ProductID = 8, Name = "Product ID 8"}
                }
            );
            ProductController controler = new ProductController(mock.Object);
            controler.pageSize = 3;

            /// Run
            ProductsListViewModel result = (ProductsListViewModel)controler.List(null, null, 2).Model;

            /// Test
            Product[] prodArr = result.Products.ToArray();
            Assert.IsTrue(prodArr.Length == 3);
            Assert.AreEqual(prodArr[0].Name , "Product ID 4");
            Assert.AreEqual(prodArr[1].Name, "Product ID 5");
            Assert.AreEqual(prodArr[2].Name, "Product ID 6");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            HtmlHelper myHelper = null;
            PagingInfo pagingInfo = new PagingInfo { CurrentPage = 2, ItemsPerPage = 10, TotalItems = 28 };
            Func<int, string> pageURLDelegate = i => "Strona " + i;

            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageURLDelegate);

            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Strona 1"">1</a>"
                            + @"<a class=""btn btn-default btn-primary selected"" href=""Strona 2"">2</a>"
                            + @"<a class=""btn btn-default"" href=""Strona 3"">3</a>", result.ToString());
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            ///Setup
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[] {
                    new Product { ProductID = 1, Name = "Product ID 1" },
                    new Product { ProductID = 2, Name = "Product ID 2"},
                    new Product { ProductID = 3, Name = "Product ID 3"},
                    new Product { ProductID = 4, Name = "Product ID 4"},
                    new Product { ProductID = 5, Name = "Product ID 5"},
                    new Product { ProductID = 6, Name = "Product ID 6"},
                    new Product { ProductID = 7, Name = "Product ID 7"},
                    new Product { ProductID = 8, Name = "Product ID 8"}
                }
            );
            ProductController controler = new ProductController(mock.Object);
            controler.pageSize = 3;

            /// Run
            ProductsListViewModel result = (ProductsListViewModel)controler.List(null, null, 2).Model;

            /// Test
            PagingInfo pInfo = result.PagingInfo;
            Assert.AreEqual(pInfo.CurrentPage, 2);
            Assert.AreEqual(pInfo.ItemsPerPage, 3);
            Assert.AreEqual(pInfo.TotalItems, 8);
            Assert.AreEqual(pInfo.TotalPages, 3);


        }

        [TestMethod]
        public void Can_Filter_Products_And_Pagenate()
        {
            ///Setup
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[] {
                    new Product { ProductID = 1, Name = "Product ID 1", Category = "cat1" },
                    new Product { ProductID = 2, Name = "Product ID 2", Category = "cat2"},
                    new Product { ProductID = 3, Name = "Product ID 3", Category = "cat3"},
                    new Product { ProductID = 4, Name = "Product ID 4", Category = "cat1"},
                    new Product { ProductID = 5, Name = "Product ID 5", Category = "cat2"},
                    new Product { ProductID = 6, Name = "Product ID 6", Category = "cat1"},
                    new Product { ProductID = 7, Name = "Product ID 7", Category = "cat2"},
                    new Product { ProductID = 8, Name = "Product ID 8", Category = "cat1"}
                }
            );
            ProductController controler = new ProductController(mock.Object);
            controler.pageSize = 3;

            /// Run
            ProductsListViewModel result = (ProductsListViewModel)controler.List(null, "cat1", 2).Model;


            /// Test
            /// Can Filter
            Product[] prod = result.Products.ToArray();
            Assert.AreEqual(prod.Length, 1);
            Assert.IsTrue(prod[0].Name == "Product ID 8" && prod[0].Category == "cat1");


            /// Test
            /// Paging info
            PagingInfo pInfo = result.PagingInfo;
            Assert.AreEqual(pInfo.CurrentPage, 2);
            Assert.AreEqual(pInfo.ItemsPerPage, 3);
            Assert.AreEqual(pInfo.TotalItems, 4);
            Assert.AreEqual(pInfo.TotalPages, 2);


        }

        [TestMethod]
        public void Can_Create_Categories()
        {
            // przygotowanie 
            // — tworzenie imitacji repozytorium 
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products)
                .Returns(
                    new Product[] {
                        new Product {ProductID = 1, Name = "P1", Category = "Jabłka"},
                        new Product {ProductID = 2, Name = "P2", Category = "Jabłka"},
                        new Product {ProductID = 3, Name = "P3", Category = "Śliwki"},
                        new Product {ProductID = 4, Name = "P4", Category = "Pomarańcze"}
                    }
            );

            // przygotowanie — utworzenie kontrolera
            NavController target = new NavController(mock.Object);

            // działanie — pobranie zbioru kategorii
            string[] results = ((IEnumerable<string>)target.Menu().Model).ToArray();

            // asercje
            Assert.AreEqual(results.Length, 3);
            Assert.AreEqual(results[0], "Jabłka");
            Assert.AreEqual(results[1], "Pomarańcze");
            Assert.AreEqual(results[2], "Śliwki");
        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            // przygotowanie 
            // — tworzenie imitacji repozytorium 
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products)
                .Returns(
                    new Product[] {
                        new Product {ProductID = 1, Name = "P1", Category = "Jabłka"},
                        new Product {ProductID = 2, Name = "P2", Category = "Jabłka"},
                        new Product {ProductID = 3, Name = "P3", Category = "Śliwki"},
                        new Product {ProductID = 4, Name = "P4", Category = "Pomarańcze"}
                    }
            );

            // przygotowanie — utworzenie kontrolera
            NavController target = new NavController(mock.Object);

            string categoryToSelect = "Jabłka";

            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            Assert.AreEqual(result, categoryToSelect);

        }

    }
}
