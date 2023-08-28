using BiblioTech.Domain;
using BiblioTech.Domain.Enum;

namespace BiblioTech.Application.DataContract.Response;

public class BookLoanResponse
{
    public string? Id { get; set; }
    public BookResponse? Book { get; set; }
    public DateTime Devolution_Date { get; set; }
    public LoanEnum Loan_Status { get; set; }
}
