namespace BiblioTech.Application.DataContract.Request;

public sealed class AuthRequest
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}
