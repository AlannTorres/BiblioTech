namespace BiblioTech.Application.DataContract.Request;

public class CreateBookRequest
{
    public string? ISBN { get; set; }
    public string? Title { get; set; }
    public int? Year_publication { get; set; }
    public string? Description { get; set; }
    public string? Quantity { get; set; }
    public string? Publishing { get; set; }
}
