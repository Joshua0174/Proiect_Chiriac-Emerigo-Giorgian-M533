using BusinessLayer.Contracts;
using BusinessLayer.Dto.ReviewDto;
using BusinessLayer.Mappers;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IRepository<Review> _repository;
        public ReviewService(IRepository<Review> repository)
        {
            _repository = repository;
        }
        public ReviewDto Create(Guid Id, CreateReviewDto reviewModel)
        {   
            var review=reviewModel.ToReviewFromCreateDto(Id);
            _repository.Add(review);
            _repository.SaveChanges();
            return review.ToReviewDto();
        }

        public ReviewDto Delete(Guid id)
        {
            var review=_repository.Find(c=>c.Id==id).FirstOrDefault();
            if(review==null) {
                return null;
            }
            _repository.Remove(review);
            _repository.SaveChanges();
            return review.ToReviewDto() ;
        }

        
    }
}
