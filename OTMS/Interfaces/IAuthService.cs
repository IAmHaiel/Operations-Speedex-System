using OTMS.DTOs;
using OTMS.Models;

namespace OTMS.Interfaces;

public interface IAuthService
{
    Task<TokenResponseDTO?> LoginAsync(User_LoginDTO request);
    Task<Employee?> RegisterAsync(User_RegisterDTO request);
    Task<TokenResponseDTO?> RefreshTokensAsync(RefreshTokenRequestDTO request);
}