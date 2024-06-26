using Duende.IdentityServer.Models;
using Duende.IdentityServer;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel;

namespace VShopSchool.IdentityServer.Configuration
{
    public class IdentityConfiguration
    {
        public const string Admin = "Admin";
        public const string Client = "Client";

        // Acesso aos recursos do IdentityServer
        public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };

        // Escopo de funcoes de ler, escrever e excluir dados acessando a aplicacao web "vshopschool"
        public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
        {
            new ApiScope("vshopschool", "VShopSchool Server"),
            new ApiScope(name: "read", "Read data."),
            new ApiScope(name: "write", "Write data."),
            new ApiScope(name: "delete", "Delete data.")
        };

        // Gerando clientes
        public static IEnumerable<Client> Clients => new List<Client>
        {
	        // Genérico
	        new Client
            {
                ClientId = "client",
                ClientSecrets = { new Secret("tastetheblood#swallowyourpride".Sha256())},
                AllowedGrantTypes = GrantTypes.ClientCredentials, // Credenciais necessarias
		        AllowedScopes = {"read", "write", "profile" }
            },
            new Client
            {
                ClientId = "vshopschool",
                ClientSecrets = { new Secret("tastetheblood#swallowyourpride".Sha256())},
                AllowedGrantTypes = GrantTypes.Code, // Via codigo
		        RedirectUris = {"https://localhost:7029/signin-oidc"}, // Login
		        PostLogoutRedirectUris = {"https://localhost:7029/signout-callback-oidc"}, // Logout
		        AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "vshopschool"
                }
            }
        };

    }
}
