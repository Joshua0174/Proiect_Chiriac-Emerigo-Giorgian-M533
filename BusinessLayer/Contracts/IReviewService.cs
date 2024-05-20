using BusinessLayer.Dto.ProductDto;
using BusinessLayer.Dto.ReviewDto;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
    public interface IReviewService
    {
        //public List<ReviewDto> GetAll();
        //public ReviewDto GetById(Guid id);
        public ReviewDto Create(Guid id, CreateReviewDto productModel);
        //public ReviewDto Update(Guid id, Product productModel);
        public ReviewDto Delete(Guid id);
    }
}
