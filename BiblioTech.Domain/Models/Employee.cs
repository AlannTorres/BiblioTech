namespace BiblioTech.Domain.Models;

public class Employee : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string PasswordHash { get; set; }
    public string CPF { get; set; }
}
