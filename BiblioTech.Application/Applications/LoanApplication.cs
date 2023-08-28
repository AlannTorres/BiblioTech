using AutoMapper;
using BiblioTech.Application.DataContract.Request;
using BiblioTech.Application.DataContract.Response;
using BiblioTech.Application.Interfaces;
using BiblioTech.Domain;
using BiblioTech.Domain.Interfaces.Services;
using BiblioTech.Domain.Validations.Base;

namespace BiblioTech.Application.Applications;

public class LoanApplication : ILoanApplication
{
    private readonly ILoanService _loanService;
    private readonly IMapper _mapper;

    public LoanApplication(
        ILoanService loanService,
        IMapper mapper)
    {
        _loanService = loanService;
        _mapper = mapper;
    }

    public async Task<Response> CreateLoanAsync(CreateLoanRequest loanRequest, int days)
    {
        try
        {
            var loan = _mapper.Map<Loan>(loanRequest);

            return await _loanService.CreateLoanAsync(loan, days);
        }
        catch (Exception ex)
        {
            var response = Report.Create(ex.Message);

            return Response.Unprocessable(response);
        }
    }

    public async Task<Response> ResgisterReturnAsync(string user_email, string book_id)
    {
        return await _loanService.ResgisterReturnAsync(user_email, book_id);
    }

    public async Task<Response<List<LoanResponse>>> ListAllLoanByFilterAsync(string? user_name = null)
    {
        Response<List<Loan>> loan = await _loanService.ListAllLoanByFilterAsync(user_name);

        if (loan.Report.Any())
            return Response.Unprocessable<List<LoanResponse>>(loan.Report);

        var response = _mapper.Map<List<LoanResponse>>(loan.Data);

        return Response.OK(response);
    }

}
