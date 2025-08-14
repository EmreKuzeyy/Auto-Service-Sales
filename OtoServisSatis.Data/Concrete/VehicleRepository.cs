using Microsoft.EntityFrameworkCore;
using OtoServisSatis.Data.Abstract;
using OtoServisSatis.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OtoServisSatis.Data.Concrete
{
    public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<Vehicle> GetVehicleById(int id)
        {
            return await _dbSet.AsNoTracking().Include(v => v.Make).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Vehicle>> GetCustomVehicleList()
        {
            return await _dbSet.AsNoTracking().Include(v => v.Make).ToListAsync();
        }

        public async Task<List<Vehicle>> GetCustomVehicleList(Expression<Func<Vehicle, bool>> expression)
        {
            return await _dbSet.Where(expression).AsNoTracking().Include(v => v.Make).ToListAsync();
        }
    }
}
