using BusinessLayer.Contracts;
using BusinessLayer.Extensions;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("controller")]
    public class CartController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        public CartController(UserManager<AppUser> userManager, IProductService productService, ICartService cartService)
        {
            _userManager = userManager;
            _productService = productService;
            _cartService = cartService;
        }

        [HttpGet]
        [Authorize(Policy = "UserOnly")]
        public async Task<IActionResult> GetUserCart()
        {
            var userName = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(userName);
            if (appUser == null)
            {
                return BadRequest("Something went wrong!");
            }
            var userCart = await _cartService.GetUserCart(appUser);
            return Ok(userCart);
        }

        [HttpPost]
        [Authorize(Policy = "UserOnly")]
        public async Task<IActionResult> AddCart(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var product = _productService.GetBySymbol(symbol);

            if (product == null)
            {
                return BadRequest("Product not found");
            }

            var userCart = await _cartService.GetUserCart(appUser);

            if (userCart == null)
            {
                return BadRequest();
            }

            if (userCart.Any(e => e.Symbol.ToLower() == symbol.ToLower()))
            {
                return BadRequest("Cannot add the same product to cart");
            }

            var cartModel = new Cart
            {
                ProductId = product.Id,
                AppUserId = appUser.Id,
            };

            await _cartService.CreateCart(cartModel);

            if (cartModel == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Created();
            }
        }

        [HttpDelete]
        [Authorize(Policy = "UserOnly")]
        public async Task<IActionResult> DeleteCart(string symbol)
        {
            var userName= User.GetUsername();
            var appUser= await _userManager.FindByNameAsync(userName);

            var userCart = await _cartService.GetUserCart(appUser);

            var filteredStock=userCart.Where(s=>s.Symbol.ToLower()==symbol.ToLower()).ToList();
            
            if (filteredStock.Count()==1) {
                await _cartService.DeleteCart(appUser, symbol);
            }
            else
            {
                return BadRequest("Product not in your cart");
            }
            return Ok();
        }
    }
}
