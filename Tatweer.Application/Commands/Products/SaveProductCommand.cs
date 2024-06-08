using Tatweer.Application.Models;
using MediatR;

namespace Tatweer.Application.Commands.Products
{
    /// <summary>
    /// Represents a product with a name, quantity, price, and visibility.
    /// </summary>
    public class SaveProductCommand : IRequest<Result>
    {
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the name of product.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the quantity of product.
        /// </summary>
        public int Qty { get; set; }
        /// <summary>
        /// Gets or sets the price of product.
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Gets or sets the visibility of product.
        /// </summary>
        public bool IsVisible { get; set; }
    }
}
