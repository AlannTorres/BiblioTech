using AutoMapper;
using BiblioTech.Application.DataContract.Request;
using BiblioTech.Application.DataContract.Response;
using BiblioTech.Application.Interfaces;
using BiblioTech.Domain;
using BiblioTech.Domain.Interfaces.Services;
using BiblioTech.Domain.Validations.Base;

namespace BiblioTech.Application.Applications;

public class ReserveApplication : IReserveApplication
{
    private readonly IReserveService _reserveService;
    private readonly IMapper _mapper;

    public ReserveApplication(IReserveService reserveService,
        IMapper mapper)
    {
        _reserveService = reserveService;
        _mapper = mapper;
    }

    public async Task<Response> CreateReserveAsync(CreateReserveRequest reserveRequest)
    {
        try
        {
            var reserve = _mapper.Map<Reserve>(reserveRequest);

            return await _reserveService.CreateReserveAsync(reserve);
        }
        catch (Exception ex)
        {
            var response = Report.Create(ex.Message);

            return Response.Unprocessable(response);
        }
    }

    public async  Task<Response> CloseReserveAsync(string user_email, string book_id)
    {
        try
        {
            return await _reserveService.CloseReserveAsync(user_email, book_id);
        }
        catch (Exception ex)
        {
            var response = Report.Create(ex.Message);

            return Response.Unprocessable(response);
        }
    }

    public async Task<Response<List<ReserveResponse>>> ListAllBookReserveByFilterAsync(string book_name = null, string user_name = null)
    {
        Response<List<Reserve>> reserve = await _reserveService.ListAllBookReserveByFilterAsync(book_name, user_name);

        if (reserve.Report.Any())
            return Response.Unprocessable<List<ReserveResponse>>(reserve.Report);

        var response = _mapper.Map<List<ReserveResponse>>(reserve.Data);

        return Response.OK(response);
    }
}
