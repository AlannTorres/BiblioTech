using BiblioTech.Domain.Models;

namespace BiblioTech.Domain.Interfaces.Repositories;

public interface IEmployeeRepository
{
    Task<List<Employee>> ListAllEmployeesByFilterAsync(string employee_id = null, string name = null);
    Task<List<Loan>> ListAllLoanEmployeeAsync(string employee_id);
    Task<List<Reserve>> ListAllReserveEmployeeAsync(string employee_id);
}
