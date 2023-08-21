namespace BiblioTech.Domain.Validations.Base;

public  class Response
{
    public Response()
    {
        Report = new List<Report>();
    }

    public Response(List<Report> reports)
    {
        Report = reports ?? new List<Report>();
    }

    public Response(Report report) : this(new List<Report>() { report }) { }

    public List<Report> Report { get; }

    public static Response OK() => new ();
    public static Response<T> OK<T>(T data) => new (data);
    public static Response Unprocessable(Report report) => new (report);
    public static Response Unprocessable(List<Report> reports) => new (reports);
    public static Response<T> Unprocessable<T>(List<Report> reports) => new (reports);
}

public class Response<T> : Response
{
    public Response() { }

    public Response(List<Report> reports) : base(reports) { }

    public Response(T data, List<Report> reports = null) : base(reports)
    {
        Data = data;
    }

    public T Data { get; set; }
}
