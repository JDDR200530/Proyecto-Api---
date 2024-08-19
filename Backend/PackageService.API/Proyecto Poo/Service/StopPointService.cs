using Microsoft.EntityFrameworkCore;
using Proyecto_Poo.Database.Contex;
using Proyecto_Poo.Database.Entity;

namespace Proyecto_Poo.Service
{
    public class StopPointService
    {
        private readonly PackageServiceDbContext _context;

        public StopPointService(PackageServiceDbContext context)
        {
            _context = context;
        }

        public async Task AddStopPointAsync(StopPointEntity stopPoint)
        {
            var existingCount = await _context.StopPoints
                .AsNoTracking()
                .CountAsync(sp => sp.StopPointId == stopPoint.StopPointId);

            if (existingCount < 2)
            {
                await _context.StopPoints.AddAsync(stopPoint);
                await _context.SaveChangesAsync();
            }
            else
            {
                // Manejar el caso en el que se ha alcanzado el límite
                throw new InvalidOperationException("No se pueden agregar más de dos StopPoints con el mismo StopPointId.");
            }
        }
    }
}
