using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using VShopSchool.IdentityServer.Data;

namespace VShopSchool.IdentityServer.Services
{
	public class ProfileAppService : IProfileService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaims;
		private readonly RoleManager<IdentityRole> _roleManager;

		public ProfileAppService(UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> userClaims, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_userClaims = userClaims;
			_roleManager = roleManager;
		}

		public async Task GetProfileDataAsync(ProfileDataRequestContext context)
		{
			// Armazena a id do usuario e o localiza
			string id = context.Subject.GetSubjectId();
			ApplicationUser user = await _userManager.FindByIdAsync(id);

			// Cria ClaimsPrincipal para o usuario
			ClaimsPrincipal userClaims = await _userClaims.CreateAsync(user);

			// Define a colecao de Claims com nome e sobrenome
			List<Claim> claims = userClaims.Claims.ToList();
			claims.Add(new Claim(JwtClaimTypes.FamilyName, user.LastName));
			claims.Add(new Claim(JwtClaimTypes.GivenName, user.FirstName));

			if(_userManager.SupportsUserRole)
			{
				// Armazena a lista dos nomes das roles para o usuario
				IList<string> roles = await _userManager.GetRolesAsync(user);

				foreach(string role in roles)
				{
					// Adiciona a role na claim 
					claims.Add(new Claim(JwtClaimTypes.Role, role));

					if(_roleManager.SupportsRoleClaims)
					{
						// localiza pelo nome do perfil
						IdentityRole identityRole = await _roleManager.FindByNameAsync(role);

						// Adiciona o perfil e as claims associadas com a role
						if(identityRole!= null)
						{
							claims.AddRange(await _roleManager.GetClaimsAsync(identityRole));
						}
					}
				}
			}
			context.IssuedClaims = claims; // retorna no contexto
		}

		public async Task IsActiveAsync(IsActiveContext context)
		{
			// armazena e localiza o usuario pelo id
			string userId = context.Subject.GetSubjectId();
			ApplicationUser user = await _userManager.FindByIdAsync(userId);

			// Verifica a ativacao do usuario no contexto
			context.IsActive = user is not null;
		}
	}
}
