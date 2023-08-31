namespace BiblioTech.Application.DataContract.Request;

public class CreateReserveRequest
{
    public string? Book_id { get; set; }
    public string? User_email { get; set; }
    public string? Employee_email { get; set; }
}
