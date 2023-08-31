namespace BiblioTech.Application.DataContract.Response;

public sealed class BookLoanResponse
{
    public BookResponse? Book { get; set; }
    public DateTime Devolution_Date { get; set; }
    public string Loan_Status { get; set; }
}
