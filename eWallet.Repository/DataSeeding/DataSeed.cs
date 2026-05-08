using E_Wallet.Repository.Data;
using eWallet.Core.Entities.eWallet;
using eWallet.Core.Entities.Identity;
using eWallet.Repository.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eWallet.Repository.DataSeeding
{
    public static class DataSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Hajer",
                    Email = "Hajer@test.com",
                    UserName = "Hajer@test.com",
                };

                var result = await userManager.CreateAsync(user, "Pa$$w0rd");
                if (!result.Succeeded)
                {
                    throw new Exception("Failed to seed user: " 
                        + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
            
        }

        public static async Task SeedWalletDataAsync
            (eWalletDbContext context , UserManager<AppUser> userManager)
        {
            var user = await userManager.FindByEmailAsync("Hajer@test.com");

            if (user is null) return; 

            if (!context.Wallets.Any())
            {

                var wallet = new Wallet
                {
                    AppUserId = user.Id, 
                    Balance = 1000,
                    Currency = "USD",
                   
                };
                context.Wallets.Add(wallet);
                await context.SaveChangesAsync();
            }
        
         }


        public static async Task SeedCategoryAsync(eWalletDbContext context)
        {
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category 
                    { 
                        Name = "Other",
                        Description = "General Expenses" ,
                        AppUserId = null
                    },
                    new Category 
                    { 
                        Name = "General Deposit",
                        Description = "General Income" ,
                        AppUserId = null
                    }
                };

               await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();

            }
        }
    }
}
