using KASHOP.BLL.Services.Class;
using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.Data;
using KASHOP.DAL.Model;
using KASHOP.DAL.Repositories.Class;
using KASHOP.DAL.Repositories.Interfaces;
using KASHOP.DAL.Repositortrs;
using KASHOP.DAL.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace KASHOP.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
            builder.Services.AddScoped<ICategoryServices,CategoryServices>();
            builder.Services.AddScoped<IBrandRepository, BrandRepositories>();
            builder.Services.AddScoped<IBrandServices, BrandServices>();
            builder.Services.AddScoped<ISeedData,SeedData>();
            builder.Services.AddScoped<IAuthenticationServices, AuthenticationServices>();
            builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }
           var scope=  app.Services.CreateScope();
           var objectOfSeedData= scope.ServiceProvider.GetRequiredService<ISeedData>();

           await  objectOfSeedData.DataSeedingAsync();
            await objectOfSeedData.IdentityDataSeedingAsync();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
