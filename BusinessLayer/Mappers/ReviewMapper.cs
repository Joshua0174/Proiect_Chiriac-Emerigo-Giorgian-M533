using BusinessLayer.Dto.ReviewDto;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Mappers
{
    public static class ReviewMapper
    {
        public static ReviewDto ToReviewDto(this Review reviewModel)
        {
            return new ReviewDto
            {
                Id = reviewModel.Id,
                Title = reviewModel.Title,
                Content = reviewModel.Content,
                CreatedOn = reviewModel.CreatedOn,
                Rating = reviewModel.Rating,
                ProductId = reviewModel.ProductId,
            };
        }
        public static Review ToReviewFromCreateDto( this CreateReviewDto reviewModel, Guid productId)
        {
            return new Review
            {
                Title = reviewModel.Title,
                Content = reviewModel.Content,
                //CreatedOn = reviewModel.CreatedOn,
                Rating = reviewModel.Rating,
                ProductId = productId,
            };
        }
    }
}
