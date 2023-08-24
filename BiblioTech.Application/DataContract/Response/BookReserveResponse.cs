using BiblioTech.Domain;

namespace BiblioTech.Application.DataContract.Response;

public class BookReserveResponse
{
    public string? Id { get; set; }
    public Book? Book { get; set; }
    public string? BookTitle { get; set; }
    public User? User { get; set; }
    public string? UserName { get; set; }
    public DateTime reserve_Date { get; set; }
    public string? Status_Reserve { get; set; }
}
