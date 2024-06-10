using Tatweer.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tatweer.Application.Behaviour;
using Tatweer.Insrastructure.Data;
using Tatweer.Core.Entities;
using Tatweer.Application.Commands.Products;

namespace Tatweer.Application.Handlers.Products
{
    public class SaveProductCommandHandler : IRequestHandler<SaveProductCommand, Result>
    {
        private readonly ILogger<SaveProductCommandHandler> _logger;
        private readonly TatweerContext _context;

        // Inject necessary services like DbContext or repositories
        public SaveProductCommandHandler(ILogger<SaveProductCommandHandler> logger, TatweerContext context)
        {
            // Initialize dependencies
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context;
        }

        public async Task<Result> Handle(SaveProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation(" Adding new product");
                if (request.Id == 0)
                {
                    var product = new Product(request.Name, request.Qty, request.Price, request.IsVisible);
                    await _context.Products.AddAsync(product);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation(" Product saved successfully");
                }
                else
                {
                    var current = await _context.Products
                                                .FirstOrDefaultAsync(a => a.Id == request.Id);

                    if (current != null)
                    {
                        current.Update(request.Name, request.Qty, request.Price, request.IsVisible);
                        _context.Products.Update(current);
                        _logger.LogInformation($" Product with Id {request.Id} updated");
                        await _context.SaveChangesAsync();
                    }
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                await _context.RollbackTransactionAsync();
                _logger.LogError($" Failed to save product: {ex.Message} ");
                return Result.Failure(" product couldn't be save");
            }
        }

    }
}
