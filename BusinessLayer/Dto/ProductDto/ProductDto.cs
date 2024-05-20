using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dto.ProductDto
{
    public class ProductDto
    {   
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Symbol {  get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public List<ReviewDto.ReviewDto> Reviews { get; set; }
    }
}
