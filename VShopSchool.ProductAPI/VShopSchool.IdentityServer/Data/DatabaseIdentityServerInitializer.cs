using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using VShopSchool.IdentityServer.Configuration;

namespace VShopSchool.IdentityServer.Data
{
    public class DatabaseIdentityServerInitializer : IDatabaseSeedInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DatabaseIdentityServerInitializer(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager= userManager;
            _roleManager= roleManager;
        }

        public void InitializeSeedRoles()
        {
            // Senao existir o Admin entao e criado o perfil
            if (!_roleManager.RoleExistsAsync(IdentityConfiguration.Admin).Result)
            {
                // Criando o perfil Admin 
                IdentityRole roleAdmin = new();
                roleAdmin.Name = IdentityConfiguration.Admin.ToUpper();
                roleAdmin.NormalizedName = IdentityConfiguration.Admin.ToUpper();
                _roleManager.CreateAsync(roleAdmin).Wait();
            }

            // Senao existir o Client entao e criado o perfil
            if (!_roleManager.RoleExistsAsync(IdentityConfiguration.Client).Result)
            {
                // Criando o perfil Client
                IdentityRole roleClient = new();
                roleClient.Name = IdentityConfiguration.Client.ToUpper();
                roleClient.NormalizedName = IdentityConfiguration.Client.ToUpper();
                _roleManager.CreateAsync(roleClient).Wait();
            }

        }

        public void InitializeSeedUsers()
        {
            if (_userManager.FindByEmailAsync("admin@com.br").Result == null)
            {       
                ApplicationUser admin = new()
                {
                    UserName = "admin1",
                    NormalizedUserName = "ADMIN1",
                    Email = "admin1@com.br",
                    NormalizedEmail = "ADMIN1@COM.BR",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    PhoneNumber = "+55 (79) 12345-6789",
                    FirstName = "Usuario",
                    LastName = "Admin1",
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                IdentityResult resultAdmin = _userManager.CreateAsync(admin, "NumSey#2022").Result;
                if (resultAdmin.Succeeded)
                {
                    _userManager.AddToRoleAsync(admin, IdentityConfiguration.Admin).Wait();

                    var adminClaims = _userManager.AddClaimsAsync(admin, new Claim[]
                    {
                        new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                        new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                        new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                        new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin)
                    }).Result;
                }

            }

            if (_userManager.FindByEmailAsync("client1@com.br").Result == null)
            {
                ApplicationUser client = new()
                {
                    UserName = "client1",
                    NormalizedUserName = "CLIENT1",
                    Email = "client1@com.br",
                    NormalizedEmail = "CLIENT1@COM.BR",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    PhoneNumber = "+55 (79) 12345-6789",
                    FirstName = "Usuario",
                    LastName = "Client1",
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                IdentityResult resultClient = _userManager.CreateAsync(client, "NumSey#2022").Result;
                if (resultClient.Succeeded)
                {
                    _userManager.AddToRoleAsync(client, IdentityConfiguration.Client).Wait();

                    var clientClaims = _userManager.AddClaimsAsync(client, new Claim[]
                    {
                        new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                        new Claim(JwtClaimTypes.GivenName, client.FirstName),
                        new Claim(JwtClaimTypes.FamilyName, client.LastName),
                        new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client)
                    }).Result;
                }

            }

        }
    }
}
