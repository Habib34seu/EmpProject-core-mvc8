using Emp.Domain.Models;
using Emp.Infrastructure.Data;
using Emp.Infrastructure.Repositories.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Emp.Infrastructure.Repositories;

public class EmployeeRepository : Repository<EmployeeEntity>, IEmployeeRepository
{
    private ApplicationDbContext _context;
    public EmployeeRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    

    public void Update(EmployeeEntity obj)
    {
        _context.Emps.Update(obj);
    }
}
