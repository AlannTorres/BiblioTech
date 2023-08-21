using BiblioTech.Domain.Enum;
using BiblioTech.Domain;

namespace BiblioTech.Application.DataContract.Response;

public sealed class BookCheckoutResponse
{
    public long Id { get; set; }
    public long BookId { get; set; }
    public string? BookTitle { get; set; }
    public long UserId { get; set; }
    public DateTime Checkout_Date { get; set; }
    public DateTime Due_Date { get; set; }
    public string? Status_Checkout { get; set; }
}
