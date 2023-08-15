using LibrarySystem_2.Domain.Enum;
using Microsoft.VisualBasic;

namespace LibrarySystem_2.Domain
{
    public class BookCheckout : BaseEntity
    {
        public int book_id { get; set; }
        public int user_id { get; set; }
        public DateTime checkout_Date { get; set; }
        public DateTime due_Date { get; set; }
        public BookCheckoutEnum status_Checkout { get; set; }
    }
}
