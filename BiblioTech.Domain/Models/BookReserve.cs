using BiblioTech.Domain.Enum;

namespace BiblioTech.Domain;

public class BookReserve : BaseEntity
{
    public int book_id { get; set; }
    public int user_id { get; set; }
    public DateTime Reserver_Date { get; set; }
    public BookReserveEnum Reserver_Status { get; set; }
}
