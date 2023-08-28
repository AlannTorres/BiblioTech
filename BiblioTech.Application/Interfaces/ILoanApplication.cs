using BiblioTech.Domain.Validations.Base;
using BiblioTech.Application.DataContract.Request;
using BiblioTech.Application.DataContract.Response;

namespace BiblioTech.Application.Interfaces;

public interface ILoanApplication
{
    Task<Response> CreateLoanAsync(CreateLoanRequest loanRequest, int days);
    Task<Response> ResgisterReturnAsync(string user_email, string book_id);
    Task<Response<List<LoanResponse>>> ListAllLoanByFilterAsync(string? user_name = null);
}
