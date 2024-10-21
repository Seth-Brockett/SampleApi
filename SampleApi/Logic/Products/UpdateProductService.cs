using Microsoft.EntityFrameworkCore;
using SampleApi.Common.Requests;
using SampleApi.Common.Responses;
using SampleApi.Core.DataAccess.Context;
using SampleApi.Core.DataAccess.Models;
using SampleApi.Core.Services;

namespace SampleApi.Logic.Products
{
    public interface IUpdateProductService : IAsyncService<UpdateProductRequest, UpdateProductResponse> { }

    public class UpdateProductService : IUpdateProductService
    {
        private readonly SampleDbContext _sampleDbContext;

        public UpdateProductService(SampleDbContext sampleDbContext)
        {
            _sampleDbContext = sampleDbContext;
        }

        public async Task<UpdateProductResponse> ExecuteAsync(UpdateProductRequest request)
        {

            if (!await _sampleDbContext.Products.AnyAsync(product => product.Id.Equals(request.ProductId)))
            {
                return new UpdateProductResponse
                {
                    ProductId = Guid.Empty
                };
            }

            var updatedProduct = new DbProduct
            {
                Id = request.ProductId,
                Name = request.ProductName,
                Quantity = request.ProductQuantity
            };

            _sampleDbContext.Products.Update(updatedProduct);

            await _sampleDbContext.SaveChangesAsync();

            return new UpdateProductResponse
            {
                ProductId = request.ProductId
            };
        }
    }
}
