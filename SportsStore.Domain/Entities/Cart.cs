using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        /// <summary>
        /// Adding item to the Cart
        /// </summary>
        /// <param name="product">A member of the product Entity class</param>
        /// <param name="quantity">Ammount of the product being added to the Cart</param>
        public void AddItem(Product product, int quantity)
        {
            CartLine cartLine = lineCollection.Where(p => p.Product.ProductID == product.ProductID).FirstOrDefault();
            if (cartLine == null)
                lineCollection.Add(new CartLine() { Product = product, Quantity = quantity });
            else
            {
                cartLine.Quantity += quantity;
            }

            lineCollection.RemoveAll(line => line.Quantity <= 0);
                
        }

        public void RemoveLine(Product product)
        {
            lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(line => line.Product.Price * line.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }
}
