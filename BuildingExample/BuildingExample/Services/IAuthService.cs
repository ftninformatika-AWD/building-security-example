using BuildingExample.DTOs;
using System.Security.Claims;

namespace BuildingExample.Services
{
    public interface IAuthService
    {
        Task Register(RegistrationDTO data);
        Task<string> Login(LoginDTO data);
        Task<ProfileDTO> GetProfile(ClaimsPrincipal userPrincipal);
    }
}
