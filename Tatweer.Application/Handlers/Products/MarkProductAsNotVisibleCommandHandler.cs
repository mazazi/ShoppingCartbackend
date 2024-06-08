using Tatweer.Application.Commands.Products;
using Tatweer.Application.Models;
using Tatweer.Insrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tatweer.Application.Commands.Products;

namespace Tatweer.Application.Handlers.Products
{

    public class MarkProductAsNotVisibleCommandHandler : IRequestHandler<MarkProductAsNotVisibleCommand, Result>
    {
        private readonly ILogger<MarkProductAsNotVisibleCommandHandler> _logger;
        private readonly TatweerContext _context; 
        public MarkProductAsNotVisibleCommandHandler(ILogger<MarkProductAsNotVisibleCommandHandler> logger, TatweerContext context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context; 
        }

        public async Task<Result> Handle(MarkProductAsNotVisibleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation(" Change product to be not visible");
                var current = await _context.Products
                                            .FirstOrDefaultAsync(a => a.Id == request.Id);

                if (current != null)
                {
                    current.MarkProductNotVisible();
                    _logger.LogInformation($" product with Id {request.Id} has been mark as not visible");
                    await _context.SaveChangesAsync();
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError($" Failed to mark product as not visible : {ex.Message} ");
                return Result.Failure(" Product couldn't be changed to not visible");
            }
        }
    }
}
