using BiblioTech.Domain.Enum;

namespace BiblioTech.Domain;

public class BookReserve : BaseEntity
{
    public Book? Book { get; set; }
    public User? User { get; set; }
    public DateTime Reserver_Date { get; set; }
    public BookReserveEnum Reserver_Status { get; set; }
}
