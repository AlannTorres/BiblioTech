using BiblioTech.Domain.Enum;
using BiblioTech.Domain.Models;

namespace BiblioTech.Domain;

public class Loan : BaseEntity
{
    public User? User { get; set; }
    public User? Employee { get; set; }
    public DateTime Loan_Date { get; set; }
    public LoanEnum Status_Checkout { get; set; }
    public List<BookLoan>? Books { get; set; }
}
