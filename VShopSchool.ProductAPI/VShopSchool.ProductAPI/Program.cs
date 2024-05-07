
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using VShopSchool.ProductAPI.Context;
using VShopSchool.ProductAPI.Interfaces;
using VShopSchool.ProductAPI.Repositories;
using VShopSchool.ProductAPI.Services;

namespace VShopSchool.ProductAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(x=>            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

	        var mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
	                builder.Services.AddDbContext<ShopDbContext>(options => options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
	        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
	        builder.Services.AddScoped<IProductRepository, ProductRepository>();
	        builder.Services.AddScoped<ICategoryService, CategoryService>();
	        builder.Services.AddScoped<IProductService, ProductService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}