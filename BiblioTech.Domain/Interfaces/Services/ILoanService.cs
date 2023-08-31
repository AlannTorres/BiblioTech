using BiblioTech.Domain.Validations.Base;

namespace BiblioTech.Domain.Interfaces.Services;

public interface ILoanService
{
    Task<Response> CreateLoanAsync(Loan bookCheckout, int days);
    Task<Response> RegisterReturnAsync(string user_email, string book_id);
    Task<Response<List<Loan>>> ListAllLoanByFilterAsync(string user_name = null);
}
