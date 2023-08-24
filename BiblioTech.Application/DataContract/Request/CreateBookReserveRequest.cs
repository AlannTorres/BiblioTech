using BiblioTech.Domain;

namespace BiblioTech.Application.DataContract.Request;

public class CreateBookReserveRequest
{
    public string? Book_id { get; set; }
    public string? User_id { get; set; }
    public DateTime reserve_Date { get; set; }
    public string? Status_Reserve { get; set; }
}
