using Microsoft.AspNetCore.Authentication;
using Proyecto_Poo.Service.Interface;

namespace Proyecto_Poo.Service
{
    public class AuthService : IAuthService
    {
        public string GetUserId()
        {
            return "a1b2c3d4-e5f6-7890-abcd-ef1234567890";
        }
    }
}

