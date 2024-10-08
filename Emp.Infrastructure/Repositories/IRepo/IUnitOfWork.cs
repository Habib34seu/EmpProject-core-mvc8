using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emp.Infrastructure.Repositories.IRepo;

public interface IUnitOfWork
{
    IEmployeeRepository Employee { get; }

    void Save();
}
