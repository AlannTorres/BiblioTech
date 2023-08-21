namespace BiblioTech.Domain.Enum;

public enum BookReserveEnum
{
    Active = 0,
    Canceled
}

public static class BookReserverEnumExtensions
{
    public static string? GetString(this BookReserveEnum status)
    {
        switch (status)
        {
            case BookReserveEnum.Active:
                return "active";
            case BookReserveEnum.Canceled:
                return "canceled";
            default:
                return null;
        }
    }
}
