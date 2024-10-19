using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Emp.Infrastructure.Repositories.IRepo;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter);
    Task<T> Get(Expression<Func<T, bool>> filter);
    Task Add (T entity);
    Task Remove(T entity);
}
