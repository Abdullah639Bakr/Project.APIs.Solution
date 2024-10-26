using Microsoft.AspNetCore.Identity;
using Project.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Repositories.Contract
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(AppUser user , UserManager<AppUser> userManager);
    }
}
