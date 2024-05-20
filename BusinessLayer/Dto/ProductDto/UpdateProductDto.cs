using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dto.ProductDto
{
    public class UpdateProductDto
    {
       
        [Required]
        [MinLength(4, ErrorMessage = "Product name must be 4 characters")]
        [MaxLength(20, ErrorMessage = "Product name cannot be over 20 characters")]
        public string Name { get; set; } = string.Empty;


        [Required]
        public string Symbol { get; set; }

        [Required]
        [MinLength(15, ErrorMessage = "Product description  must be 15 characters")]
        [MaxLength(250, ErrorMessage = "Product description cannot be over 250 characters")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0.001, 100000, ErrorMessage = "Price must be between 0.001 and 100000 euro")]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(15, ErrorMessage = " Category cannot be over 20 characters")]
        public string Category { get; set; } = string.Empty;

    }
}
