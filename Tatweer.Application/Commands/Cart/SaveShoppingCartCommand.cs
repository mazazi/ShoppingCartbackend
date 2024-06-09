using Tatweer.Application.Models;
using MediatR;
using Tatweer.Application.Responses.Cart;

namespace Tatweer.Application.Commands.Products
{
    /// <summary>
    /// Represents a Cart with a Product, quantity, and price.
    /// </summary>
    public class SaveShoppingCartCommand : IRequest<Result>
    {
        public List<ShoppingCartDto> Items { get; set; }
    }
}
