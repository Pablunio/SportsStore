using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Domain.Entities;
using System.Linq;

namespace SportsStore.UnitTests
{
    /// <summary>
    /// Testing Cart functons
    /// </summary>
    [TestClass]
    public class CartTests
    {
        public CartTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Can_Add_New_Lines()
        {
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Cart cart = new Cart();

            cart.AddItem(p1, 3);
            cart.AddItem(p2, 2);
            CartLine[] cl = cart.Lines.ToArray();

            Assert.AreEqual(2, cl.Length);
            Assert.AreEqual(p1, cl[0].Product);
            Assert.AreEqual(p2, cl[1].Product);
        }

        [TestMethod]
        public void Can_Add_Remove_Quantity()
        {
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };

            Cart cart = new Cart();

            cart.AddItem(p1, 2);
            cart.AddItem(p2, 1);

            cart.AddItem(p1, 3);
            cart.AddItem(p1, -1);
            cart.AddItem(p2, -1);

            CartLine[] cl = cart.Lines.ToArray();

            Assert.AreEqual(1, cl.Length);
            Assert.AreEqual(4, cl[0].Quantity);
        }


        [TestMethod]
        public void Can_Remove_Line()
        {
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Product p3 = new Product { ProductID = 3, Name = "P3" };

            Cart cart = new Cart();

            cart.AddItem(p1, 2);
            cart.AddItem(p2, 1);
            cart.AddItem(p3, 2);
            
            Assert.AreEqual(3, cart.Lines.Count());

            cart.RemoveLine(p2);
            
            Assert.AreEqual(2, cart.Lines.Count());
        }

        [TestMethod]
        public void Can_Sum_the_cart()
        {
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 10M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 5M };
            Product p3 = new Product { ProductID = 3, Name = "P3", Price = 1M };

            Cart cart = new Cart();

            cart.AddItem(p1, 2);
            cart.AddItem(p2, 1);
            cart.AddItem(p3, 2);

            Assert.AreEqual(10M * 2 + 1 * 5M + 2 * 1M, cart.ComputeTotalValue());

            cart.AddItem(p1, 3);
            cart.AddItem(p2, 1);
            cart.AddItem(p3, -1);

            Assert.AreEqual(10M * 5 + 5M * 2 + 1 * 1M, cart.ComputeTotalValue());
        }

        [TestMethod]
        public void Can_Clear_cart()
        {
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 10M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 5M };
            Product p3 = new Product { ProductID = 3, Name = "P3", Price = 1M };

            Cart cart = new Cart();

            cart.AddItem(p1, 2);
            cart.AddItem(p2, 1);
            cart.AddItem(p3, 2);

            Assert.AreEqual(3, cart.Lines.Count());

            cart.Clear();

            Assert.AreEqual(0, cart.Lines.Count());

        }





    }
}
