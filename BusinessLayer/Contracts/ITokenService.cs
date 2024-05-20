using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
    public interface ITokenService
    {
        string CreateTokenForUser(AppUser user);
        string CreateTokenForAdmin(AppUser admin);
    }
}
