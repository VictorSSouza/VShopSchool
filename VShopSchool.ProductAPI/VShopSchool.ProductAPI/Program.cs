using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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

            builder.Services.AddControllers().AddJsonOptions(x=>            
                    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VShopSchool.ProductApi", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"Digite 'Bearer' [space] seu token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                          new List<string> ()
                    }
                });
            });

	        var mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
	                builder.Services.AddDbContext<ShopDbContext>(options => options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddCors(options =>{
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyMethod()
                                      .AllowAnyOrigin()
                                      .AllowAnyHeader());
            });

	        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
	        builder.Services.AddScoped<IProductRepository, ProductRepository>();
	        builder.Services.AddScoped<ICategoryService, CategoryService>();
	        builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.AddAuthentication("Bearer")
                            .AddJwtBearer("Bearer", options =>
                            {
                                options.Authority =
                                        builder.Configuration["VShopSchool.IdentityServer:ApplicationUrl"];

                                options.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateAudience = false
                                };
                            });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "vshopschool");
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}