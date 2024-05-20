using BusinessLayer.Dto.ProductDto;
using BusinessLayer.Helpers;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
    public interface IProductService
    {
        public List<ProductDto> GetAll(QueryObject query);
        public ProductDto GetById(Guid id);
        public ProductDto GetBySymbol(string symbol);
        public ProductDto Create(Product productModel);
        public ProductDto Update(Guid id, Product productModel);
        public ProductDto Delete(Guid id);

        public ProductDto DeleteDelegate(Guid id, Func<Guid, ProductDto> callback);
        
    }
}
