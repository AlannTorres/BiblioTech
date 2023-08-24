namespace BiblioTech.Application.DataContract.Request;

public class CreateBookCheckoutRequest
{
    public string? Book_id { get; set; }
    public string? User_id { get; set; }
    public DateTime Checkout_Date { get; set; }
    public DateTime Due_Date { get; set; }
}
