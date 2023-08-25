namespace BiblioTech.Domain.Enum;

public enum ReserveEnum
{
    Active = 0,
    Canceled
}

public static class BookReserverEnumExtensions
{
    public static string? GetString(this ReserveEnum status)
    {
        switch (status)
        {
            case ReserveEnum.Active:
                return "active";
            case ReserveEnum.Canceled:
                return "canceled";
            default:
                return null;
        }
    }
}
