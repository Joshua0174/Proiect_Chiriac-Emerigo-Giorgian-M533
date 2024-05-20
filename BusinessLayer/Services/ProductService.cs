using BusinessLayer.Contracts;
using BusinessLayer.Dto.ProductDto;
using BusinessLayer.Dto.ReviewDto;
using BusinessLayer.Helpers;
using BusinessLayer.Mappers;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BusinessLayer.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;

        public ProductService(IRepository<Product> repository)
        {
            _repository = repository;
        }
        public ProductDto Create(Product productModel)
        {
            _repository.Add(productModel);
            _repository.SaveChanges();
            return productModel.ToProductDto();
        }

        public ProductDto Delete(Guid id)
        {
             
              var product= _repository.GetById(id);
              if(product == null)
              {
                return null;
              }
              //var reviews= product.Reviews;
              
              _repository.Remove(product);
              _repository.SaveChanges();
             return product.ToProductDto();
        }

        public ProductDto DeleteDelegate(Guid id, Func<Guid, ProductDto> callback)
        {
            return callback(id);
        }

        public List<ProductDto> GetAll(QueryObject query)
        {
            var products=_repository.GetAllAsQuery().Include(c=>c.Reviews).ToList();
            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                products = (List<Product>)products.Where(s => s.Name.Contains(query.Name));
            }
            if (!string.IsNullOrWhiteSpace(query.Category))
            {
                products = (List<Product>)products.Where(s => s.Category.Contains(query.Category));
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("ProductName", StringComparison.OrdinalIgnoreCase))
                {
                    products = query.IsDescending ? products.OrderByDescending(s => s.Name).ToList() : products.OrderBy(s => s.Name).ToList();
                }
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
           
            return products.Skip(skipNumber).Take(query.PageSize).Select(c=>c.ToProductDto()).ToList(); 
        }

        public ProductDto GetById(Guid id)
        {
            var product = _repository.FindAsQuery(c=>c.Id==id).Include(c=>c.Reviews).FirstOrDefault();
            if (product == null)
            {
                return null;
            }
            return product.ToProductDto();
        }

        public ProductDto GetBySymbol(string symbol)
        {
            return _repository.Find(c => c.Symbol == symbol).FirstOrDefault().ToProductDto();
        }

        public ProductDto Update(Guid id, Product productModel)
        {   
            var product=_repository.Find(c=>c.Id==id).FirstOrDefault();
            if (product == null) { return null; }
            product.Name=productModel.Name;
            product.Description=productModel.Description;
            product.Price=productModel.Price;
            product.Category=productModel.Category;

            _repository.Update(product);
            _repository.SaveChanges();
            return product.ToProductDto(); 
        }

        
    }
}
