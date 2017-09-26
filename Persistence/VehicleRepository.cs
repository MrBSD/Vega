using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vega.Core;
using Vega.Core.Models;

namespace Vega.Persistence
{
    public class VehicleRepository: IVehicleRepository
    {
        private readonly VegaDbContext context;

        public VehicleRepository(VegaDbContext context)
        {
            this.context = context;
        }

        public void Add(Vehicle vehicle)
        {
            context.Add(vehicle);
        }

        public async Task<Vehicle> GetVehicle(int id, bool includeRelated=true)
        {
            if (!includeRelated)
                return await context.Vehicles.FindAsync(id);
            
            return await context.Vehicles
            .Include(v => v.Features)
                .ThenInclude(vf => vf.Feature)
            .Include(v => v.Model)
                .ThenInclude(m => m.Make)
            .SingleOrDefaultAsync (v =>v.Id==id);
        }
        
        public async Task<IEnumerable<Vehicle>> GetVehicles(VehicleQuery queryObj)
        {
           var query = context.Vehicles
            .Include (v => v.Model)
                .ThenInclude(m =>m.Make)
            .Include(v => v.Features)
                .ThenInclude(vf => vf.Feature)
            .AsQueryable();

            if (queryObj.MakeId.HasValue)
            {
                query = query.Where(v => v.Model.MakeId == queryObj.MakeId.Value);
                
            }

            if (queryObj.SortBy == "make")
                query = (queryObj.IsSortAscending) ? query.OrderBy(v =>v.Model.Make.Name) : query.OrderByDescending(v =>v.Model.Make.Name);

            if (queryObj.SortBy == "model")
                query = (queryObj.IsSortAscending) ? query.OrderBy(v =>v.Model.Name) : query.OrderByDescending(v =>v.Model.Name);
                
            if (queryObj.SortBy == "contactName")
                query = (queryObj.IsSortAscending) ? query.OrderBy(v =>v.ContactName) : query.OrderByDescending(v =>v.ContactName);
           
            return await query.ToListAsync();
        }

        public void Remove(Vehicle vehicle)
        {
            context.Remove(vehicle);
        }
    }
}