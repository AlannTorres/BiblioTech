using BiblioTech.Domain.Enum;

namespace BiblioTech.Domain;

public class Reserve : BaseEntity
{
    public Book? Book { get; set; }
    public User? User { get; set; }
    public User? Employee { get; set; }
    public DateTime Reserve_Date { get; set; }
    public DateTime EstimatedArrival_Date { get; set; }
    public ReserveEnum Status_Reserve { get; set; }
}
