using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SampleApi.Common.Requests
{
    public class AddProductRequest
    {
        [Required]
        [StringLength(20, ErrorMessage = "Product names must not excede 20 characters.")]
        public string ProductName { get; set; } = null!;

        public int ProductQuantity { get; set; } = 0;
    }
}
