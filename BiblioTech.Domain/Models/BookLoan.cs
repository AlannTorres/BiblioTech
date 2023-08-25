using BiblioTech.Domain.Enum;

namespace BiblioTech.Domain.Models;

public class BookLoan : BaseEntity
{
    public Loan? Loan { get; set; }
    public Book? Book { get; set; }
    public DateTime Devolution_Date { get; set; }
    public LoanEnum Load_Status { get; set; }
}
