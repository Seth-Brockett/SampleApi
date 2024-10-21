using Microsoft.EntityFrameworkCore;
using SampleApi.Common.Requests;
using SampleApi.Common.Responses;
using SampleApi.Core.DataAccess.Context;
using SampleApi.Core.Services;

namespace SampleApi.Logic.Products
{
    public interface IDeleteProductService : IAsyncService<DeleteProductRequest, DeleteProductResponse> { }

    public class DeleteProductService : IDeleteProductService
    {
        private readonly SampleDbContext _sampleDbContext;

        public DeleteProductService(SampleDbContext sampleDbContext)
        {
            _sampleDbContext = sampleDbContext;
        }

        public async Task<DeleteProductResponse> ExecuteAsync(DeleteProductRequest request)
        {
            var productToDelete = await _sampleDbContext.Products.FirstOrDefaultAsync(product => product.Id == request.ProductId);

            if (productToDelete is null)
            {
                return new DeleteProductResponse
                {
                    ProductId = Guid.Empty
                };
            }

            _sampleDbContext.Products.Remove(productToDelete);

            await _sampleDbContext.SaveChangesAsync();

            return new DeleteProductResponse
            {
                ProductId = request.ProductId
            };
        }
    }
}
