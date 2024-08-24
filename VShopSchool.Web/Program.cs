using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using VShopSchool.Web.Services;
using VShopSchool.Web.Services.Interfaces;

namespace VShopSchool.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthentication(options =>
                    {
                        options.DefaultScheme = "Cookies";
                        options.DefaultChallengeScheme = "oidc";

                    }).AddCookie("Cookies", c =>
				       {
                           c.ExpireTimeSpan = TimeSpan.FromMinutes(10);
					       c.Events = new CookieAuthenticationEvents()
					        {
						        OnRedirectToAccessDenied = (context) =>
						        {
							        context.HttpContext.Response.Redirect(builder.Configuration["ServiceUri:IdentityServer"] + "/Account/AccessDenied");
	                                return Task.CompletedTask;
						        }
					        };
				      })
                      .AddOpenIdConnect("oidc", options =>
                        {
                            options.Events.OnRemoteFailure = context =>
                            {
                                context.Response.Redirect("/");
                                context.HandleResponse();

                                return Task.FromResult(0);
                            };
                            options.Authority = builder.Configuration["ServiceUri:IdentityServer"];
                            options.GetClaimsFromUserInfoEndpoint = true;
                            options.ClientId = "vshopschool";
                            options.ClientSecret = builder.Configuration["Client:Secret"];
                            options.ResponseType = "code";
                            options.ClaimActions.MapJsonKey("role", "role", "role");
                            options.ClaimActions.MapJsonKey("sub", "sub", "sub");
                            options.TokenValidationParameters.NameClaimType = "name";
                            options.TokenValidationParameters.RoleClaimType = "role";
                            options.Scope.Add("vshopschool");
                            options.SaveTokens = true;
                        }
             );

            builder.Services.AddHttpClient<IProductService, ProductService>("ProductApi", c =>
            {
                c.BaseAddress = new Uri(builder.Configuration["ServiceUri:ProductApi"]);
                c.DefaultRequestHeaders.Add("Connection", "Keep-Alive");
                c.DefaultRequestHeaders.Add("Keep-Alive", "3600");
                c.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-ProductAPI");
            });
            builder.Services.AddHttpClient<ICartService, CartService>("CartApi", c =>
            {
                c.BaseAddress = new Uri(builder.Configuration["ServiceUri:CartApi"]);
            });
            builder.Services.AddHttpClient<ICouponService, CouponService>("CouponApi", c =>
            {
                c.BaseAddress = new Uri(builder.Configuration["ServiceUri:CouponApi"]);
            });

            builder.Services.AddScoped<ICouponService, CouponService>();
            builder.Services.AddScoped<ICartService, CartService>();
		    builder.Services.AddScoped<IProductService, ProductService>();
			builder.Services.AddScoped<ICategoryService, CategoryService>();

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}