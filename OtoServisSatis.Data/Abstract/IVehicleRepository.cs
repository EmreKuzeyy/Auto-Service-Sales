using OtoServisSatis.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OtoServisSatis.Data.Abstract
{
    public interface IVehicleRepository : IRepository<Vehicle>
    {
        Task<IEnumerable<Vehicle>> GetCustomVehicleList();
        Task<List<Vehicle>> GetCustomVehicleList(Expression<Func<Vehicle, bool>> expression);
        Task<Vehicle> GetVehicleById(int id);
    }
}
