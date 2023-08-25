using BiblioTech.Domain.Enum;
using BiblioTech.Domain.Models;

namespace BiblioTech.Domain;

public class Loan : BaseEntity
{
    public User? User { get; set; }
    public Employee? Employee { get; set; }
    public DateTime Loan_Date { get; set; }
    public DateTime Due_Date { get; set; }
    public LoanEnum Status_Checkout { get; set; }
    public List<BookLoan>? Books { get; set; }
}
