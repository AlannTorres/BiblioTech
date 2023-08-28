namespace BiblioTech.Domain.Enum;

public enum LoanEnum
{
    Pending = 0,
    Returned = 1
}

public static class BookCheckoutEnumExtensions
{
    public static string? GetString(this LoanEnum status)
    {
        switch (status)
        {
            case LoanEnum.Pending:
                return "pending";
            case LoanEnum.Returned:
                return "returned";
            default:
                return null;
        }
    }
}
