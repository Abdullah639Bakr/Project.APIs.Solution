using Microsoft.AspNetCore.Identity;
using Project.Core.Entities;
using Project.Core.Entities.Identity;
using Project.Repository.Data.Contexts;
using Project.Repository.Identity.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project.Repository.Identity
{
    public static class StoreIdentityDbContextSeed
    {
        public async static Task SeedAppUserAsync(UserManager<AppUser> _userManager)
        {
            if (_userManager.Users.Count() == 0) 
            {
                var user = new AppUser()
                {
                    Email = "abdullah11@gmail.com",
                    DisplayName = "Abdullah Bakr",
                    UserName = "Abdullah.Bakr",
                    PhoneNumber = "1234567890",
                    Address = new Address()
                    {
                        FName = "Abdullah",
                        LName = "Bakr",
                        City = "Cairo",
                        Country = "Egypt",
                        Street = "Elnoor",
                    }

                };

                await _userManager.CreateAsync(user, "P@ssW0rd");

            }
        }
    }
}
