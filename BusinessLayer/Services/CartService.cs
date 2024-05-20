using BusinessLayer.Contracts;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CartService : ICartService
    {  
        private readonly ICartRepository _cartService;
        public CartService(ICartRepository cartService)
        {
            _cartService = cartService;
        }

        public async Task<Cart> CreateCart(Cart cart)
        {
            var cartModel= await _cartService.CreateAsync(cart);
            return cartModel;
        }

        public async Task<Cart> DeleteCart(AppUser appUser, string symbol)
        {
            var cartDeleted = await _cartService.DeleteAsync(appUser, symbol);
            return cartDeleted;
        }

        public async Task<List<Product>> GetUserCart(AppUser user)
        {
            var userCart=await _cartService.GetUserCart(user);
            return userCart; 
        }
    }
}
