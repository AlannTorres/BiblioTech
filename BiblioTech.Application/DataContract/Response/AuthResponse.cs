namespace BiblioTech.Application.DataContract.Response;

public sealed class AuthResponse
{
    public string Token { get; set; }
    public string Type { get; set; }
    public int ExpireIn { get; set; }
}
