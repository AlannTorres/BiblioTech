namespace BiblioTech.Domain;

public class Book : BaseEntity
{
    public long ISBN { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public int year_publication { get; set; }
    public int quantity { get; set; }
    public string publishing { get; set; }
}
