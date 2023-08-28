namespace BiblioTech.Application.DataContract.Request;

public sealed class CreateUserRequest
{
    public string? CPF { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Adress { get; set; }
    public string? Telephone { get; set; }
}
