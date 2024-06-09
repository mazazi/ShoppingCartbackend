using AutoMapper;
using Tatweer.Application.Behaviour;
using Tatweer.Application.Responses.Products;
using Tatweer.Application.Responses.Wrappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Tatweer.Core.Entities;
using Tatweer.Insrastructure.Data;

namespace Tatweer.Application.Services
{
    public interface IProductQuery
    {
        Task<List<Product>> GetAllAsync();
        Task<ProductsDto> GetByIdAsync(int id);
        Response<DataSourceResult> GetAllWithPager(int page, int pageSize, out int total, string? name);
        Response<DataSourceResult> GetAllWithPagerForAdmin(int page, int pageSize, out int total, string? name);

    }

    public class ProductQuery : IProductQuery
    {
        private readonly TatweerContext _tatweerContext;
        private readonly IMapper _mapper;

        public ProductQuery(TatweerContext TatweerContext, IMapper mapper)
        {
            _tatweerContext = TatweerContext ?? throw new ArgumentNullException(nameof(TatweerContext));
            _mapper = mapper;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _tatweerContext.Products
                                    .AsNoTracking()
                                    .ToListAsync();
        }

        public async Task<ProductsDto> GetByIdAsync(int id)
        {
            var pro = await _tatweerContext.Products
                                    .Where(a => a.Id == id)
                                    .FirstOrDefaultAsync();

            return _mapper.Map<ProductsDto>(pro);
        }

        public Response<DataSourceResult> GetAllWithPager(int page, int pageSize, out int total, string? name)
        {
            var productsDtos = new List<ProductsDto>();
            try
            {
                var products = GetAllPaged(page, pageSize, out total, name);

                if (products.Count() > 0)
                    productsDtos = _mapper.Map<List<ProductsDto>>(products);

                var result = new DataSourceResult
                {
                    Data = productsDtos,
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

        private IQueryable<Product> GetAllPaged(int page, int pageSize, out int total, string? name)
        {
            var products = _tatweerContext.Products
                                          .Where(a => a.IsVisible)
                                          .AsNoTracking();

            if (!string.IsNullOrEmpty(name))
                products = products.Where(x => x.Name.ToLower().Contains(name.Trim().ToLower()));

            total = products.Count();
            var pagedProducts = products.OrderByDescending(x => x.Id).Skip(page * pageSize).Take(pageSize);

            return pagedProducts;
        }

        private IQueryable<Product> GetAllForAdminPaged(int page, int pageSize, out int total, string? name)
        {
            var products = _tatweerContext.Products 
                                          .AsNoTracking();

            if (!string.IsNullOrEmpty(name))
                products = products.Where(x => x.Name.ToLower().Contains(name.Trim().ToLower()));

            total = products.Count();
            var pagedProducts = products.OrderByDescending(x => x.Id).Skip(page * pageSize).Take(pageSize);

            return pagedProducts;
        }
        public Response<DataSourceResult> GetAllWithPagerForAdmin(int page, int pageSize, out int total, string? name)
        {
            var productsDtos = new List<ProductsDto>();
            try
            {
                var products =  GetAllForAdminPaged(page, pageSize, out total, name);

                if (products.Count() > 0)
                    productsDtos = _mapper.Map<List<ProductsDto>>(products);

                var result = new DataSourceResult
                {
                    Data = productsDtos,
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
    }
}
