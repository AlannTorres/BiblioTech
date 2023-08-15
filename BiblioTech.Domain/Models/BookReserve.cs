using LibrarySystem_2.Domain.Enum;

namespace LibrarySystem_2.Domain
{
    public class BookReserve : BaseEntity
    {
        public int book_id { get; set; }
        public int user_id { get; set; }
        public DateTime Reserver_Date { get; set; }
        public BookReserveEnum Reserver_Status { get; set; }
    }
}
