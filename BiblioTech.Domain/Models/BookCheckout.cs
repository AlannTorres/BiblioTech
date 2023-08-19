using BiblioTech.Domain.Enum;

namespace BiblioTech.Domain;

public class BookCheckout : BaseEntity
{
    public int book_id { get; set; }
    public int user_id { get; set; }
    public DateTime checkout_Date { get; set; }
    public DateTime due_Date { get; set; }
    public BookCheckoutEnum status_Checkout { get; set; }
}
