using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SampleApi.Common.Requests
{
    public class UpdateProductRequest : AddProductRequest
    {
        [Required]
        public Guid ProductId { get; set; }
    }
}
