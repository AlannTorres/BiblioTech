namespace BiblioTech.Application.DataContract.Response;

public class ReserveResponse
{
    public string? Id { get; set; }
    public BookResponse? Book { get; set; }
    public UserResponse? User { get; set; }
    public UserResponse? Employee { get; set; }
    public DateTime Reserve_Date { get; set; }
    public string? Status_Reserve { get; set; }
}
