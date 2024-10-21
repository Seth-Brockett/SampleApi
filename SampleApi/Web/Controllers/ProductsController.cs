using Microsoft.AspNetCore.Mvc;
using SampleApi.Common.Requests;
using SampleApi.Common.Responses;
using SampleApi.Logic.Products;

namespace SampleApi.Web.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private readonly IGetProductListService _getProductListService;
        private readonly IGetProductService _getProductService;
        private readonly IAddProductService _addProductService;
        private readonly IUpdateProductService _updateProductService;
        private readonly IDeleteProductService _deleteProductService;

        public ProductsController(
            IGetProductListService productListService,
            IGetProductService getProductService,
            IAddProductService addProductService,
            IUpdateProductService updateProductService,
            IDeleteProductService deleteProductService
            )
        {
            _getProductListService = productListService;
            _getProductService = getProductService;
            _addProductService = addProductService;
            _updateProductService = updateProductService;
            _deleteProductService = deleteProductService;
        }

        [HttpGet]
        public async Task<ActionResult<GetProductListResponse>> GetProductListAsync()
        {
            var response = await _getProductListService.ExecuteAsync(new VoidRequest());
            return response;
        }

        [HttpGet("{ProductId}")]
        public async Task<ActionResult<GetProductResponse>> GetProductAsync(GetProductRequest request)
        {
            var response = await _getProductService.ExecuteAsync(request);
            return response.Product is null ? NotFound(new ErrorResponse { Message = "The product was not found." }) : response;
        }

        [HttpPost]
        public async Task<ActionResult<AddProductResponse>> AddProductAsync([FromBody] AddProductRequest request)
        {
            var response = await _addProductService.ExecuteAsync(request);
            return response;
        }

        // Would prefer this to have Id in route. Can maintain correct bindings by making a custom model binder and attribute for [FromRouteAndBody].
        [HttpPut]
        public async Task<ActionResult<UpdateProductResponse>> UpdateProductAsync([FromBody] UpdateProductRequest request)
        {
            var response = await _updateProductService.ExecuteAsync(request);
            return response.ProductId.Equals(Guid.Empty) ? NotFound(new ErrorResponse { Message = "The product to update was not found." }) : response;
        }

        // For an added layer of safety when interacting with entity. I should have added a column for tracking deleted entities.
        // I did not have time to do this, but wanted to note the optimization.
        [HttpDelete("{ProductId}")]
        public async Task<ActionResult<DeleteProductResponse>> DeleteProductAsync(DeleteProductRequest request)
        {
            var response = await _deleteProductService.ExecuteAsync(request);
            return response.ProductId.Equals(Guid.Empty) ? NotFound(new ErrorResponse { Message = "The product to delete was not found." }) : response;
        }
    }
}
