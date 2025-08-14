using OtoServisSatis.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OtoServisSatis.Data.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetCustomUserList();
        Task<List<User>> GetCustomUserList(Expression<Func<User, bool>> expression);
    }
}
