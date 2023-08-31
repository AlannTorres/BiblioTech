namespace BiblioTech.Application.DataContract.Request;

public class CreateLoanRequest
{
    public string? User_email { get; set; }
    public string? Employee_email { get; set; }
    public List<CreateBookLoanRequest>? Books { get; set; }
}
