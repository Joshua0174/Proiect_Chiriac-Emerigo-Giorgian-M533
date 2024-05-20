using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public interface ICartRepository
    { 
        Task<List<Product>> GetUserCart(AppUser user);
        Task<Cart> CreateAsync(Cart cart);
        Task<Cart> DeleteAsync(AppUser appuser, string symbol);
    }
}
