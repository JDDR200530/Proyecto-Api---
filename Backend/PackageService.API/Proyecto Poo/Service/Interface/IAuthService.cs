using Proyecto_Poo.Dtos.Auth;
using Proyecto_Poo.Dtos.Common;
using System.Security.Claims;

namespace Proyecto_Poo.Service.Interface
{
    public interface IAuthService
    {
        Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginDto dto);

        Task<ResponseDto<LoginResponseDto>> RegisterAsync(RegisterDto dto);

        Task<ResponseDto<LoginResponseDto>> RefreshTokenAsync (RefreshTokenDto dto);

        ClaimsPrincipal GetTokenPrincipal(string token);

    }
}
