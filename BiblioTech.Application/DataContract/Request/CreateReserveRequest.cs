namespace BiblioTech.Application.DataContract.Request;

public class CreateReserveRequest
{
    public string? Book_id { get; set; }
    public string? User_id { get; set; }
    public string? Employee_id { get; set; }
    public DateTime reserve_Date { get; set; }
}
