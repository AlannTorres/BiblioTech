using BiblioTech.Domain.Validations.Base;
using BiblioTech.Application.DataContract.Request;
using BiblioTech.Application.DataContract.Response;

namespace BiblioTech.Application.Interfaces;

public interface IReserveApplication
{
    Task<Response> CreateReserveAsync(CreateReserveRequest reserveRequest);
    Task<Response> CloseReserveAsync(string user_email, string book_id);
    Task<Response<List<ReserveResponse>>> ListAllBookReserveByFilterAsync(string book_name = null, string user_name = null);
}
