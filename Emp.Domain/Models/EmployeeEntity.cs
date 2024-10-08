using System.ComponentModel.DataAnnotations;

namespace Emp.Domain.Models;

public class EmployeeEntity
{
    [Key]
    public int EmployeeId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string Division { get; set; } = default!;
    public string Building { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Room { get; set; } = default!;
}
