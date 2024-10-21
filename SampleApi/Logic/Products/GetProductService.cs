using Microsoft.EntityFrameworkCore;
using SampleApi.Common.Models;
using SampleApi.Common.Requests;
using SampleApi.Common.Responses;
using SampleApi.Core.DataAccess.Context;
using SampleApi.Core.Services;

namespace SampleApi.Logic.Products
{
    public interface IGetProductService : IAsyncService<GetProductRequest, GetProductResponse> { }

    public class GetProductService : IGetProductService
    {
        private readonly SampleDbContext _sampleDbContext;

        public GetProductService(SampleDbContext sampleDbContext)
        {
            _sampleDbContext = sampleDbContext;
        }

        public async Task<GetProductResponse> ExecuteAsync(GetProductRequest request)
        {
            var product = await _sampleDbContext.Products.FirstOrDefaultAsync(product => product.Id.Equals(request.ProductId));

            if (product == null)
            {
                return new GetProductResponse
                {
                    Product = null
                };
            }

            var responseProduct = new Product
            {
                ProductId = product.Id,
                ProductName = product.Name,
                ProductQuantity = product.Quantity
            };

            return new GetProductResponse
            {
                Product = responseProduct,
            };
        }
    }
}
