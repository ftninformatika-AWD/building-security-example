using BuildingExample.DTOs;
using BuildingExample.Exceptions;
using BuildingExample.Migrations;
using BuildingExample.Models;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BuildingExample.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task Register(RegistrationDTO data)
        {
            var user = new ApplicationUser
            {
                UserName = data.Username,
                Email = data.Email,
                Name = data.Name,
                Surname = data.Surname
            };

            var result = await _userManager.CreateAsync(user, data.Password);
            if (!result.Succeeded)
            {
                // kako se može desiti da je više informacija novog korisnika nevalidno definisano,
                // preuzima se tekstualni opis svake od grešaka i spaja u jedan string koji se vraća klijentu
                string errorMessage = string.Join(" ", result.Errors.Select(e => e.Description));
                throw new InvalidRegistrationException(errorMessage);
            }

            await _userManager.AddToRoleAsync(user, "Seller");
        }

        public async Task<string> Login(LoginDTO data)
        {
            // pronalaženje korisnika prema korisničkom imenu
            var user = await _userManager.FindByNameAsync(data.Username);
            if (user == null)
            {
                throw new InvalidCredentialsException("user with provided username was not found");
            }

            // provera da li uneta lozinka odgovara pronađenom korisniku
            if (!await _userManager.CheckPasswordAsync(user, data.Password))
            {
                throw new InvalidCredentialsException("password is invalid");
            }

            string token = await GenerateJwtToken(user);
            return token;
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),  // jedinstvena informacija za određivanje korisnika (subject) za kog se izdaje token
                new Claim(ClaimTypes.NameIdentifier, user.UserName),    // jedinstveni identifikator korisnika za kog se izdaje token, koji koristi Identity
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())  // jedinstveni identifikator tokena
            };

            // Dobaviti sve role korisnika i svaku dodati kao posebnu tvrdnju
            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            // formiranje informacija za generisanje tokena
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),    // token se može koristiti 60 minuta
                signingCredentials: creds
            );

            // pokretanje procesa generisanja tokena
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<ProfileDTO> GetProfile(ClaimsPrincipal userPrincipal)
        {
            // preuzimanje korisničkom imena iz tokena
            var username = userPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (username == null)
            {
                throw new InvalidCredentialsException("token is invalid");
            }

            // preuzimanje korisnika na osnovu korisničkog imena
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                throw new InvalidCredentialsException("user with provided token was not found");
            }

            return new ProfileDTO
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.UserName,
                Name = user.Name,
                Surname = user.Surname
            };
        }
    }
}
