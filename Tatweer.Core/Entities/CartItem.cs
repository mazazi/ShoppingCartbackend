using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tatweer.Core.Common;

namespace Tatweer.Core.Entities
{
    public class CartItem : EntityBase
    {
        /// <summary>
        /// Gets or sets of product Id.
        /// </summary>
        public int ProductId { get; set; }
        public Product Product { get; set; }
        /// <summary>
        /// Gets or sets of price of the product.
        /// because if the price is changed in product it should not affects on old cart items
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Gets or sets the quantity of product.
        /// </summary>
        public int Qty { get; set; }

        protected CartItem()
        {

        }

        public CartItem(int productId, decimal price, int quantity)
        {
            SetValues(productId, price, quantity);
        }

        private void SetValues(int productId, decimal price, int quantity)
        {
            ProductId = productId;
            Price = price;
            Qty = quantity;
        }


    }
}
