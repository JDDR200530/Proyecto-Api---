using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Proyecto_Poo.Database.Entity;
using Proyecto_Poo.Dtos.Auth;
using Proyecto_Poo.Dtos.Common;
using Proyecto_Poo.Service.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Proyecto_Poo.Database.Contex;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Proyecto_Poo.Service
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<UserEntity> signInManager;
        private readonly UserManager<UserEntity> userManager;
        private readonly IConfiguration configuration;
        private readonly ILogger<AuthService> logger;
        private readonly PackageServiceDbContext context;

        public AuthService(
            SignInManager<UserEntity> signInManager,
            UserManager<UserEntity> userManager,
            IConfiguration configuration,
            ILogger<AuthService> logger,
            PackageServiceDbContext context
        )
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.configuration = configuration;
            this.logger = logger;
            this.context = context;
        }

        public ClaimsPrincipal GetTokenPrincipal(string token)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWT:Secret").Value));

            var validation = new TokenValidationParameters
            {
                IssuerSigningKey = securityKey,
                ValidateLifetime = false,
                ValidateActor = false,
                ValidateIssuer = false,
                ValidateAudience = false,
            };
            return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
        }

        public async Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginDto dto)
        {
            var result = await signInManager
                .PasswordSignInAsync(dto.Email, dto.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var userEntity = await userManager.FindByEmailAsync(dto.Email);

                // Claim list creation
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userEntity.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserId", userEntity.Id)
                };

                var userRoles = await userManager.GetRolesAsync(userEntity);
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var jwtToken = GenerateToken(authClaims);

                return new ResponseDto<LoginResponseDto>
                {
                    StatusCode = 200,
                    Status = true,
                    Message = "Inicio de sesión satisfactorio",
                    Data = new LoginResponseDto
                    {
                        FullName = $"{userEntity.FirstName} {userEntity.LastName}",
                        Email = userEntity.Email,
                        Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                        TokenExpiration = jwtToken.ValidTo
                    }
                };
            }

            return new ResponseDto<LoginResponseDto>
            {
                StatusCode = 401,
                Status = false,
                Message = "Error en el inicio de sesión"
            };
        }

        public async Task<ResponseDto<LoginResponseDto>> RefreshTokenAsync(RefreshTokenDto dto)
        {
            string email = "";
            try
            {
                var principal = GetTokenPrincipal(dto.Token);
                var emailClaim = principal.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress");
                var userIdClaim = principal.Claims.Where(x => x.Type == "UserId").FirstOrDefault();
                if (emailClaim is null)
                {
                    return new ResponseDto<LoginResponseDto>
                    {
                        StatusCode = 401,
                        Status = false,
                        Message = "Acceso no autorizado, no se encontro un correo valido"
                    };

                }

                email = emailClaim.Value;
                var userEntity = await userManager.FindByEmailAsync(email);
                if (userEntity is null)
                {
                    return new ResponseDto<LoginResponseDto>
                    {
                        StatusCode = 401,
                        Status = false,
                        Message = "Acceso no autorizado : el usuario no existe"
                    };
                };
                if (userEntity.RefreshToken != dto.RefreshToken)
                {
                    return new ResponseDto<LoginResponseDto>
                    {
                        StatusCode = 401,
                        Status = false,
                        Message = "Acceso no autorizado: la sesion no es valida"
                    };
                }
                if (userEntity.RefreshTokenExpired < DateTime.Now)
                {
                    return new ResponseDto<LoginResponseDto>
                    {
                        StatusCode = 401,
                        Status = false,
                        Message = "Acceso no autorizado: La sesion ha expirado"
                    };
                }
                List<Claim> autClaims = await GetClaims(userEntity);
                var jwtToken = GetToken(autClaims);
                var loginResponseDto = new LoginResponseDto
                {
                    Email = email,
                    FullName = $"{userEntity.FirstName} {userEntity.LastName}",
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    TokenExpiration = jwtToken.ValidTo,
                    RefreshToken = GenerateRefreshTokenString()
                };
                userEntity.RefreshToken = loginResponseDto.RefreshToken;
                userEntity.RefreshTokenExpired = DateTime.Now.AddMinutes(int.Parse(configuration["JWT:RefreshTokenExpire"] ?? "30"));

                context.Entry(userEntity);
                await context.SaveChangesAsync();
                return new ResponseDto<LoginResponseDto>
                {
                    Status = true,
                    StatusCode = 200,
                    Message = "Token renovado exitosamente",
                    Data = loginResponseDto
                };
            }
            catch (Exception e)
            {
                logger.LogError(exception: e, message: e.Message);
                return new ResponseDto<LoginResponseDto>
                {
                    StatusCode = 500,
                    Status = false,
                    Message = "Osurrio un error al renovar el token"
                };
            }
        }

        public async Task<ResponseDto<LoginResponseDto>> RegisterAsync(RegisterDto dto)
        {
            var user = new UserEntity
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                UserName = dto.Email,
                Email = dto.Email
            };

            var result = await userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                var userEntity = await userManager.FindByEmailAsync(dto.Email);

                // Assigning default role
                await userManager.AddToRoleAsync(userEntity, "USER");

                // Claim list creation
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userEntity.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserId", userEntity.Id),
                    new Claim(ClaimTypes.Role, "USER")
                };

                var jwtToken = GenerateToken(authClaims);

                return new ResponseDto<LoginResponseDto>
                {
                    StatusCode = 200,
                    Status = true,
                    Message = "Registro de usuario realizado satisfactoriamente",
                    Data = new LoginResponseDto
                    {
                        FullName = $"{userEntity.FirstName} {userEntity.LastName}",
                        Email = userEntity.Email,
                        Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                        TokenExpiration = jwtToken.ValidTo
                    }
                };
            }

            return new ResponseDto<LoginResponseDto>
            {
                StatusCode = 400,
                Status = false,
                Message = "Error al registrar el usuario"
            };
        }

        private JwtSecurityToken GenerateToken(List<Claim> authClaims)
        {
            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            return new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(int.Parse(configuration["JWT:Expires"] ?? "15")),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
            );
        }

        private string GenerateRefreshTokenString()
        {
            var randomNumber = new byte[64];
            using (var numberGenerator = RandomNumberGenerator.Create())
            {
                numberGenerator.GetBytes(randomNumber);
            }
            return Convert.ToBase64String(randomNumber);
        }

        private JwtSecurityToken GetToken (List<Claim> authClaims)
        {
            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            return new JwtSecurityToken
                (
                  issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(int.Parse(configuration["JWT:Expires"] ?? "15")),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey,
                    SecurityAlgorithms.HmacSha256)
                );
        }
        private async Task<List<Claim>> GetClaims(UserEntity userEntity)
        {
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userEntity.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserId", userEntity.Id),
                };

            var userRoles = await userManager.GetRolesAsync(userEntity);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            return authClaims;
        }
    }
}
