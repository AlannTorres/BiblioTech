using BiblioTech.Domain;
using BiblioTech.Domain.Models;

namespace BiblioTech.Application.DataContract.Response;

public sealed class LoanResponse
{
    public string? Id { get; set; }
    public UserResponse? Employee { get; set; }
    public UserResponse? User { get; set; }
    public DateTime Loan_Date { get; set; }
    public List<BookLoanResponse>? Books { get; set; }
}
