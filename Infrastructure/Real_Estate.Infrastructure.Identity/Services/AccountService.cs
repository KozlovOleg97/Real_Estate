using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Real_Estate.Core.Application.DTOs.Account;
using Real_Estate.Core.Application.Enums;
using Real_Estate.Core.Application.Interfaces.Services;
using Real_Estate.Core.Domain.Settings;
using Real_Estate.Infrastructure.Identity.Entities;

namespace Real_Estate.Infrastructure.Identity.Services
{
	public class AccountService : IAccountService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly JWTSettings _jwtSettings;

		public AccountService(UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			IOptions<JWTSettings> jwtSettings)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_jwtSettings = jwtSettings.Value;
		}

		public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
		{
			AuthenticationResponse response = new();

			var user = await _userManager.FindByEmailAsync(request.Email);

			if (user == null)
			{
				response.HasError = true;
				response.Error = $"No Accounts registered with {request.Email}";
				return response;
			}

			var result = await _signInManager.PasswordSignInAsync(user.UserName,
				request.Password, false, lockoutOnFailure: false);

			if (!result.Succeeded)
			{
				response.HasError = true;
				response.Error = $"Invalid credentials for {request.Email}";
				return response;
			}

			if (!user.EmailConfirmed)
			{
				response.HasError = true;
				response.Error = $"Account not confirmed for {request.Email}";
				return response;
			}

			JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);

			response.Id = user.Id;
			response.Email = user.Email;
			response.UserName = user.UserName;

			var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

			response.Roles = rolesList.ToList();
			response.IsVerified = user.EmailConfirmed;
			response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

			var refreshToken = GenerateRefreshToken();

			response.RefreshToken = refreshToken.Token;

			return response;
		}

		public async Task<RegisterResponse> RegisterUserAsync(RegisterRequest request, string origin, Roles typeOfUser)
		{
			RegisterResponse response = new()
			{
				HasError = false
			};

			var userWithTheSameUserName = await _userManager.FindByNameAsync(request.UserName);

			if (userWithTheSameUserName != null)
			{
				response.HasError = true;
				response.Error = $"username '{request.UserName}' is already taken.";

				return response;
			}

			var user = new ApplicationUser
			{
				Email = request.Email,
				FirstName = request.FirstName,
				LastName = request.LastName,
				UserName = request.UserName,
				EmailConfirmed = true
			};

			var result = await _userManager.CreateAsync(user, request.Password);

			if (result.Succeeded == false)
			{
				response.HasError = true;
				foreach (var error in result.Errors)
				{
					response.Error = error.Description;
				}

				return response;
			}

			await _userManager.AddToRoleAsync(user, typeOfUser.ToString());

			return response;
		}

		private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
		{
			var userClaims = await _userManager.GetClaimsAsync(user);

			var roles = await _userManager.GetRolesAsync(user);

			var roleClaims = new List<Claim>();

			foreach (var role in roles)
			{
				roleClaims.Add(new Claim("roles", role));
			}

			var claims = new[]
				{
					new Claim(JwtRegisteredClaimNames.Sub, user.UserName),

					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

					new Claim(JwtRegisteredClaimNames.Email, user.Email),

					new Claim("uid", user.Id)
				}
				.Union(userClaims)
				.Union(roleClaims);

			var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

			var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

			var jwtSecurityToken = new JwtSecurityToken(
				issuer: _jwtSettings.Issuer,
				audience: _jwtSettings.Audience,
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
				signingCredentials: signingCredentials);

			return jwtSecurityToken;
		}

		private RefreshToken GenerateRefreshToken()
		{
			return new RefreshToken
			{
				Token = RandomTokenString(),
				Expires = DateTime.UtcNow.AddDays(7),
				Created = DateTime.UtcNow
			};
		}

		private string RandomTokenString()
		{
			using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();

			var randomBytes = new byte[40];

			rngCryptoServiceProvider.GetBytes(randomBytes);

			return BitConverter.ToString(randomBytes).Replace("-", "");
		}
	}
}