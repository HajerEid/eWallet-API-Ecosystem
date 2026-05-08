
using E_Wallet.Repository.Data;
using eWallet.API.Extensions;
using eWallet.API.MiddleWares;
using eWallet.Core.Entities.Identity;
using eWallet.Repository.DataSeeding;
using eWallet.Repository.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace eWallet.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy.AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });


            // Add services to the container.

            builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerDocumentation();


           

            builder.Services.AddDbContext<eWalletDbContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("WalletConnection"));
            });

            builder.Services.AddDbContext<AppIdentityDbContext>(options =>{
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });



            builder.Services.AddApplicationServices();
            builder.Services.AddIdentityService(builder.Configuration);

            

            var app = builder.Build();
            app.UseCors("AllowAll");


            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                // update wallet
                var walletContext = services.GetRequiredService<eWalletDbContext>();
                await walletContext.Database.MigrateAsync();

                // update identity
                var identityContext = services.GetRequiredService<AppIdentityDbContext>();
                await identityContext.Database.MigrateAsync();

                //Data Seeding 
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                var dbContext = services.GetRequiredService<eWalletDbContext>();

                await DataSeed.SeedUsersAsync(userManager);
                await DataSeed.SeedCategoryAsync(dbContext);
                await DataSeed.SeedWalletDataAsync(dbContext,userManager);
                

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "Error during appling migration");

                var message = ex.InnerException?.Message ?? ex.Message;
                logger.LogError(ex, "error is: " + message);
            }


            app.UseMiddleware<ExceptionMiddleWare>();
            app.UseSwaggerDocumentation();




            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
               // app.UseStatusCodePagesWithReExecute("/errors/{0}");

            }


            


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
