using Emp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emp.Infrastructure.Repositories.IRepo;

public interface IEmployeeRepository : IRepository<EmployeeEntity>
{
    void Update(EmployeeEntity obj);
    
}
