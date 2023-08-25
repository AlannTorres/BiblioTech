using BiblioTech.Domain.Enum;
using BiblioTech.Domain.Models;

namespace BiblioTech.Domain;

public class Reserve : BaseEntity
{
    public Book? Book { get; set; }
    public User? User { get; set; }
    public Employee? Employee { get; set; }
    public DateTime Reserve_Date { get; set; }
    public DateTime EstimatedArrival_Date { get; set; }
    public ReserveEnum Reserve_Status { get; set; }
}
