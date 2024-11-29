using Proyecto_Poo.Service.Interface;

namespace Proyecto_Poo.Service
{
    public class AuditService : IAudtiService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        // Cambia el constructor a público
        public AuditService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            var idClaim = httpContextAccessor.HttpContext
                .User.Claims.Where(x => x.Type == "UserId").FirstOrDefault();

            return idClaim.Value;
        }
    }

}