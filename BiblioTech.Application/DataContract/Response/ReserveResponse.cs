namespace BiblioTech.Application.DataContract.Response;

public sealed class ReserveResponse
{
    public BookResponse? Book { get; set; }
    public UserResponse? User { get; set; }
    public UserResponse? Employee { get; set; }
    public string? Status_Reserve { get; set; }
    public DateTime Reserve_Date { get; set; }
    public DateTime EstimatedArrival_Date { get; set; }
}
