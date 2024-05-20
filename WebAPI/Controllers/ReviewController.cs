using BusinessLayer.Contracts;
using BusinessLayer.Dto.ReviewDto;
using BusinessLayer.Extensions;
using BusinessLayer.Mappers;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IProductService _productService;
        public ReviewController(IReviewService reviewService, UserManager<AppUser> userManager, IProductService productService)
        {
            _reviewService = reviewService;
            _userManager = userManager;
            _productService = productService;
        }

        [Authorize(Policy = "UserOnly")]
        [HttpPost("{productId:Guid}")]
        public async Task<IActionResult> Create([FromRoute] Guid productId, CreateReviewDto reviewDto) {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (_productService.GetById(productId) != null)
            {
                return BadRequest("Product doesn't exist");
            }
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var reviewModel = reviewDto.ToReviewFromCreateDto(productId);
            reviewModel.AppUserId = appUser.Id;
            _reviewService.Create(productId, reviewDto);
            return Ok(reviewDto);
        }

        [Authorize(Policy = "UserOnly")]
        [HttpDelete("{id:Guid}")]
        public IActionResult Delete(Guid id) { 
            _reviewService.Delete(id);
            return Ok();
        }
    }
}
