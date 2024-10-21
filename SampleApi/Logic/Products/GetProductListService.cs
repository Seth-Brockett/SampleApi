using Microsoft.EntityFrameworkCore;
using SampleApi.Common.Models;
using SampleApi.Common.Requests;
using SampleApi.Common.Responses;
using SampleApi.Core.DataAccess.Context;
using SampleApi.Core.Services;

namespace SampleApi.Logic.Products
{
    public interface IGetProductListService : IAsyncService<VoidRequest, GetProductListResponse> { }

    public class GetProductListService : IGetProductListService
    {
        private readonly SampleDbContext _sampleDbContext;

        public GetProductListService(SampleDbContext sampleDbContext)
        {
            _sampleDbContext = sampleDbContext;
        }

        public async Task<GetProductListResponse> ExecuteAsync(VoidRequest request)
        {
            if (!_sampleDbContext.Products.Any())
            {
                return new GetProductListResponse
                {
                    Products = []
                };
            }

            var productList = await _sampleDbContext.Products.Select(product => new Product
            {
                ProductId = product.Id,
                ProductName = product.Name,
                ProductQuantity = product.Quantity
            }).ToListAsync();

            return new GetProductListResponse
            {
                Products = productList
            };
        }
    }
}
