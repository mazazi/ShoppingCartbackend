using AutoMapper;
using Tatweer.Application.Behaviour;
using Tatweer.Application.Responses.Products;
using Tatweer.Application.Responses.Wrappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Tatweer.Core.Entities;
using Tatweer.Insrastructure.Data;
using Tatweer.Application.Responses.Cart;

namespace Tatweer.Application.Services
{
    public interface IShoppingCartQuery
    {
        Task<List<CartItem>> GetAllAsync();
        Task<ShoppingCartDto> GetByIdAsync(int id);
        Response<DataSourceResult> GetAllWithPager(int page, int pageSize, out int total);

    }

    public class ShoppingCartQuery : IShoppingCartQuery
    {
        private readonly TatweerContext _tatweerContext;
        private readonly IMapper _mapper;

        public ShoppingCartQuery(TatweerContext TatweerContext, IMapper mapper)
        {
            _tatweerContext = TatweerContext ?? throw new ArgumentNullException(nameof(TatweerContext));
            _mapper = mapper;
        }

        public async Task<List<CartItem>> GetAllAsync()
        {
            return await _tatweerContext.CartItems
                                    .AsNoTracking()
                                    .ToListAsync();
        }

        public async Task<ShoppingCartDto> GetByIdAsync(int id)
        {
            var pro = await _tatweerContext.CartItems
                                    .Where(a => a.Id == id)
                                    .FirstOrDefaultAsync();

            return _mapper.Map<ShoppingCartDto>(pro);
        }

        public Response<DataSourceResult> GetAllWithPager(int page, int pageSize, out int total)
        {
            var cartItemsDtos = new List<ShoppingCartDto>();
            try
            {
                var products = GetAllPaged(page, pageSize, out total);

                if (products.Count() > 0)
                    cartItemsDtos = _mapper.Map<List<ShoppingCartDto>>(products);

                var result = new DataSourceResult
                {
                    Data = cartItemsDtos,
                    TotalItems = total,
                    PageIndex = page
                };

                return result;
            }
            catch (Exception ex)
            {
                total = 0;
                return new Response<DataSourceResult>(ex.Message);
            }
        }

        private IQueryable<CartItem> GetAllPaged(int page, int pageSize, out int total)
        {
            var items = _tatweerContext.CartItems 
                                          .AsNoTracking();
             
            total = items.Count();
            var pagedProducts = items.OrderByDescending(x => x.Id).Skip(page * pageSize).Take(pageSize);

            return pagedProducts;
        }

    }
}
