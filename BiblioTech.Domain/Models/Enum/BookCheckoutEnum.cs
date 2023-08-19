namespace BiblioTech.Domain.Enum;

public enum BookCheckoutEnum
{
    Pending = 0,
    Returned
}

public static class BookCheckoutEnumExtensions
{
    public static string GetString(this BookCheckoutEnum status)
    {
        switch (status)
        {
            case BookCheckoutEnum.Pending:
                return "pending";
            case BookCheckoutEnum.Returned:
                return "returned";
            default:
                return null;
        }
    }
}
