using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class CartRepository : ICartRepository
    {   
        private readonly MyDbContext _dbContext;

        public CartRepository(MyDbContext dbContext)
        {

            _dbContext = dbContext;

        }

        public async Task<Cart> CreateAsync(Cart cart)
        {
            await _dbContext.Carts.AddAsync(cart);
            await _dbContext.SaveChangesAsync();
            return cart;
        }

        public async Task<Cart> DeleteAsync(AppUser appuser, string symbol)
        {
            var cartModel = await _dbContext.Carts.FirstOrDefaultAsync(x => x.AppUserId == appuser.Id && x.Product.Symbol.ToLower()==symbol.ToLower());
            if (cartModel == null)
            {
                return null;
            }
            _dbContext.Carts.Remove(cartModel);
            await _dbContext.SaveChangesAsync();
            return cartModel;
        }

        public async Task<List<Product>> GetUserCart(AppUser user)
        {
            return await _dbContext.Carts.Where(u => u.AppUserId == user.Id)
                .Select(product => new Product
                {
                Id=product.ProductId,
                Name=product.Product.Name,
                Symbol=product.Product.Symbol,
                Category=product.Product.Category,
                Description=product.Product.Description,
                Price=product.Product.Price,

            }).ToListAsync();
        }
    }
}
