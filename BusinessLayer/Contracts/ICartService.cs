    using DataAccessLayer.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace BusinessLayer.Contracts
    {
        public interface ICartService
        {
            Task<List<Product>> GetUserCart(AppUser user);
            Task<Cart> CreateCart(Cart cart);
            Task<Cart> DeleteCart(AppUser appUser, String symbol);
    }
    }
