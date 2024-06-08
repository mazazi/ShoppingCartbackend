using Tatweer.Core.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace Tatweer.Core.Entities
{
    public class Product : EntityBase
    {
        public string Name { get; private set; }
        public int Qty { get; private set; }
        public decimal Price { get; private set; }
        public bool IsVisible { get; private set; }

        protected Product()
        {

        }

        public Product(string name, int quantity, decimal price, bool isvisible)
        {
            SetValues(name, quantity, price, isvisible);
        }

        public void Update(string name, int quantity, decimal price, bool isvisible)
        {
            SetValues(name, quantity, price, isvisible);
        }

        private void SetValues(string name, int quantity, decimal price, bool isvisible)
        {
            Name = name;
            IsVisible = isvisible;
            Price = price;
            Qty = quantity;
        }

        public void MarkProductVisible()
        {
            if(CheckProductVisibility())
                throw new InvalidOperationException("Product already visible.");

            this.IsVisible = true;
        }

        public void MarkProductNotVisible()
        {
            if (!CheckProductVisibility())
                throw new InvalidOperationException("Product already hidden.");

            this.IsVisible = false;
        }

        private bool CheckProductVisibility()
        {
            return this.IsVisible;
        }
    }
}
