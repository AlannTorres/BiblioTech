namespace BiblioTech.Domain;

public class User : BaseEntity
{
    public string? CPF { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public string? Adress { get; set; }
    public string? Telephone { get; set; }
    public string? Role { get; set; }
}
