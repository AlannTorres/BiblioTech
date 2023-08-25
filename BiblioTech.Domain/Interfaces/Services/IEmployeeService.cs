using BiblioTech.Domain.Models;
using BiblioTech.Domain.Validations.Base;

namespace BiblioTech.Domain.Interfaces.Services;

public interface IEmployeeService
{
    Task<Response<bool>> AutheticationAsync(string password, Employee employee);
    Task<Response<List<Employee>>> ListAllEmployeesByFilterAsync(string employee_id = null, string name = null);
    Task<Response<List<Loan>>> ListAllLoanEmployeeAsync(string employee_id);
    Task<Response<List<Reserve>>> ListAllReserveEmployeeAsync(string employee_id);
}
