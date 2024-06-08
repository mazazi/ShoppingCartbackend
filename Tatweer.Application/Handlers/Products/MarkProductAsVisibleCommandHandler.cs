using Tatweer.Application.Commands.Products;
using Tatweer.Application.Models;
using Tatweer.Insrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tatweer.Application.Commands.Products;

namespace Tatweer.Application.Handlers.Products
{

    public class MarkProductAsVisibleCommandHandler : IRequestHandler<MarkProductAsVisibleCommand, Result>
    {
        private readonly ILogger<MarkProductAsVisibleCommandHandler> _logger;
        private readonly TatweerContext _context; 
        public MarkProductAsVisibleCommandHandler(ILogger<MarkProductAsVisibleCommandHandler> logger, TatweerContext context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context; 
        }

        public async Task<Result> Handle(MarkProductAsVisibleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation(" Change product to be visible");
                var current = await _context.Products
                                            .FirstOrDefaultAsync(a => a.Id == request.Id);

                if (current != null)
                {
                    current.MarkProductVisible();
                    _logger.LogInformation($" product with Id {request.Id} has been mark as visible");
                    await _context.SaveChangesAsync();
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError($" Failed to mark product as visible : {ex.Message} ");
                return Result.Failure(" Product couldn't be changed to visible");
            }
        }
    }
}
