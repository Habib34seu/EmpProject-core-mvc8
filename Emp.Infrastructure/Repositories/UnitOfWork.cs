using Emp.Infrastructure.Data;
using Emp.Infrastructure.Repositories.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emp.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private ApplicationDbContext _context;
    public IEmployeeRepository Employee { get; private set; }
    public UnitOfWork(ApplicationDbContext context) 
    {
        _context = context;
        Employee = new EmployeeRepository(_context);
    }

    

    public void Save()
    {
        _context.SaveChanges();
    }
}
