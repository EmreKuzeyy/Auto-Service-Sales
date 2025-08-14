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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<IEnumerable<User>> GetCustomUserList()
        {
            return await _dbSet.AsNoTracking().Include(u => u.Role).ToListAsync();
        }

        public async Task<List<User>> GetCustomUserList(Expression<Func<User, bool>> expression)
        {
            return await _dbSet.Where(expression).AsNoTracking().Include(u => u.Role).ToListAsync();
        }
    }
}
