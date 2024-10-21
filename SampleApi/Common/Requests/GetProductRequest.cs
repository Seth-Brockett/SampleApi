using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SampleApi.Common.Requests
{
    public class GetProductRequest
    {
        [Required]
        [FromRoute]
        public Guid ProductId { get; set; }
    }
}
