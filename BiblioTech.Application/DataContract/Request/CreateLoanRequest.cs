namespace BiblioTech.Application.DataContract.Request;

public class CreateLoanRequest
{
    public string? User_id { get; set; }
    public string? Employee_id { get; set; }
    public DateTime Loan_Date { get; set; }
    public List<CreateBookLoanRequest>? Books { get; set; }
}
