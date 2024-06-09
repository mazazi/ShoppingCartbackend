using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tatweer.Application.Commands.Products;
using Tatweer.Application.Handlers.Products;
using Tatweer.Application.Models;
using Tatweer.Core.Entities;
using Tatweer.Insrastructure.Data;

namespace Tatweer.Application.Handlers.Cart
{
    public class SaveShoppingCartCommandHandler : IRequestHandler<SaveShoppingCartCommand, Result>
    {
        private readonly ILogger<SaveShoppingCartCommandHandler> _logger;
        private readonly TatweerContext _context;

        // Inject necessary services like DbContext or repositories
        public SaveShoppingCartCommandHandler(ILogger<SaveShoppingCartCommandHandler> logger, TatweerContext context)
        {
            // Initialize dependencies
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context;
        }

        public async Task<Result> Handle(SaveShoppingCartCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation(" Create Shopping Cart");
                if (!request.Items.Any())
                    return Result.Failure($" Cart Items is empty");

                _context.BeginTransaction();
                foreach (var item in request.Items)
                {
                    var product = await MapToProduct(item.ProductId);
                    if (product == null) continue;

                    //Check existing product qty with item product qty
                    if (product.Qty < item.Qty)
                        return Result.Failure($"Product [{item.ProductName}] has quantity is less than your order quantity");

                    var cartItem = new CartItem(item.ProductId, item.Price, item.Qty);
                    await _context.CartItems.AddAsync(cartItem);

                    //reduce Quantity stock of product
                    product.UpdateStock(item.Qty);

                }

                await _context.CommitTransactionAsync();
                await _context.SaveChangesAsync();
                return Result.Success();
            }
            catch (Exception ex)
            {
                await _context.RollbackTransactionAsync();
                _logger.LogError($" Failed to save Shopping Cart: {ex.Message} ");
                return Result.Failure(" Shooping Cart couldn't be save");
            }
        }

        private async Task<Product?> MapToProduct(int id)
        {
            if (id <= 0)
                return null;

            var product = await _context.Products
                                        .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return null;

            return product;
        }
    }
}
