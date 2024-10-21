using Microsoft.EntityFrameworkCore;
using SampleApi.Common.Requests;
using SampleApi.Common.Responses;
using SampleApi.Core.DataAccess.Context;
using SampleApi.Core.DataAccess.Models;
using SampleApi.Core.Services;


namespace SampleApi.Logic.Products
{
    public interface IAddProductService : IAsyncService<AddProductRequest, AddProductResponse> { }

    public class AddProductService : IAddProductService
    {
        private readonly SampleDbContext _sampleDbContext;

        public AddProductService(SampleDbContext sampleDbContext)
        {
            _sampleDbContext = sampleDbContext;
        }

        public async Task<AddProductResponse> ExecuteAsync(AddProductRequest request)
        {
            var productToAdd = new DbProduct
            {
                Id = Guid.NewGuid(),
                Name = request.ProductName,
                Quantity = request.ProductQuantity
            };

            // Operation is potentially excpensive and chance of overlap is incredibly slim. However, decided to do this given the small size of data set.
            while (await _sampleDbContext.Products.AnyAsync(product => product.Id.Equals(productToAdd.Id)))
            {
                productToAdd.Id = Guid.NewGuid();
            }

            _sampleDbContext.Products.Add(productToAdd);

            await _sampleDbContext.SaveChangesAsync();

            return new AddProductResponse
            {
                ProductId = productToAdd.Id,
            };
        }
    }
}
