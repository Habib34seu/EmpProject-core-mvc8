using Emp.Infrastructure.Data;
using Emp.Infrastructure.Repositories.IRepo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emp.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private ApplicationDbContext _context;

    private IDbContextTransaction _transaction;
    public IEmployeeRepository Employee { get; private set; }
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Employee = new EmployeeRepository(_context);
        _transaction = _context.Database.BeginTransaction();
    }


    public async Task BeginTransaction()
    {
        _transaction = await Task.Run(() => _context.Database.BeginTransaction());
    }

   

    public async Task SaveAsync()
    {
        try
        {
            await SaveChangesAsync();
            await CommitAsync();
        }
        catch (Exception commitException)
        {
            await RollbackAsync(); 
            throw new ApplicationException("Error committing changes. See inner exceptions for details.", commitException);
        }
        finally
        {
            await ResetTransactionAsync();
        }

       
    }
    public async Task<int> SaveChangesAsync()
    {
        try
        {
            return await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Error saving changes. See inner exception for details.", ex);
        }
    }
    public async Task CommitAsync()
    {
        try
        {
            await _transaction.CommitAsync();
        }
        catch (Exception commitException)
        {
            await RollbackAsync(); // Rollback on commit failure
            throw new ApplicationException("Error committing changes. See inner exceptions for details.", commitException);
        }
        finally
        {
            await ResetTransactionAsync();
        }
    }
    public async Task RollbackAsync()
    {
        await _transaction.RollbackAsync();
        await ResetTransactionAsync();
    }
    private async Task ResetTransactionAsync()
    {
        try
        {
            await _transaction.DisposeAsync();
        }
        catch (Exception disposeException)
        {
            // Log or handle the dispose exception as needed
        }

        _transaction = _context.Database.BeginTransaction(); 
    }
}
