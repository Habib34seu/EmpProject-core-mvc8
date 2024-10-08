
using Emp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Emp.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<EmployeeEntity> Emps { get; set; }
}
