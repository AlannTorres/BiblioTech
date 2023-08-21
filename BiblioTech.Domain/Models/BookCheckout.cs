using BiblioTech.Domain.Enum;

namespace BiblioTech.Domain;

public class BookCheckout : BaseEntity
{
    public Book? Book { get; set; }
    public User? User { get; set; }
    public DateTime Checkout_Date { get; set; }
    public DateTime Due_Date { get; set; }
    public BookCheckoutEnum Status_Checkout { get; set; }
}
