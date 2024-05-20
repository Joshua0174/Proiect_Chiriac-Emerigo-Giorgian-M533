using BusinessLayer.Dto.ProductDto;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto ToProductDto(this Product productModel)
        {
            return new ProductDto
            {
                Id = productModel.Id,
                Name = productModel.Name,
                Symbol = productModel.Symbol,
                Description = productModel.Description,
                Price = productModel.Price,
                Category = productModel.Category,
                Reviews = productModel.Reviews.Select(c => c.ToReviewDto()).ToList()
            };
        }
        public static Product ToProductFromCreateDto(this CreateProductDto createProductDto)
        {
            return new Product
            {
                Name = createProductDto.Name,
                Symbol = createProductDto.Symbol,
                Description = createProductDto.Description,
                Price = createProductDto.Price,
                Category = createProductDto.Category,
            };
        }
        public static Product ToProductFromUpdateDto(this UpdateProductDto updateProductDto)
        {
            return new Product
            {
                Name = updateProductDto.Name,
                Description = updateProductDto.Description,
                Price = updateProductDto.Price,
                Category = updateProductDto.Category,
            };
        }
    }
}
