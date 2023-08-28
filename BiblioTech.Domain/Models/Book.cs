namespace BiblioTech.Domain;

public class Book : BaseEntity
{
    public string ISBN { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int Year_publication { get; set; }
    public int Quantity { get; set; }
    public string? Publishing { get; set; }
}
