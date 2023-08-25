using BiblioTech.Domain.Interfaces.Repositories;
using BiblioTech.Domain.Interfaces.Services;
using BiblioTech.Domain.Validations;
using BiblioTech.Domain.Validations.Base;

namespace BiblioTech.Domain.Services;

public class LoanService : ILoanService
{
    private readonly IUnitOfWork _unitOfWork;

    public LoanService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response> CreateLoanAsync(Loan loan, int days)
    {
        var response = new Response();

        var validation = new LoanValidation();
        var errors = validation.Validate(loan).GetErrors();

        if (errors.Report.Count > 0)
            return errors;

        loan.Loan_Date = DateTime.Now;

        foreach (var book in loan.Books)
        {
            book.Loan = loan;
            book.Load_Status = Enum.LoanEnum.Pending;
            book.Devolution_Date = DateTime.Now + TimeSpan.FromDays(days);
        }

        await _unitOfWork.LoanRepository.CreateLoanAsync(loan);

        return response;
    }

    public async Task<Response> ResgisterReturnAsync(string user_email, string book_id)
    {
        var response = new Response();

        var exist = await _unitOfWork.LoanRepository.ExistLoanByUserEmail(user_email);

        if (!exist)
        {
            response.Report.Add(Report.Create($"Não existe Emprestimos para o email {user_email}!"));
            return response;
        }

        await _unitOfWork.LoanRepository.RegisterReturnBookLoanAsync(user_email, book_id);

        return response;
    }
    
    public async Task<Response<List<Loan>>> ListAllLoanByFilterAsync(string book_name = null, string user_name = null)
    {
        var response = new Response<List<Loan>>();
        _unitOfWork.BeginTransaction();

        try
        {
            var data = await _unitOfWork.LoanRepository.ListAllLoanByFilterAsync(book_name, user_name);

            if (data.Equals(null))
            {
                response.Report.Add(Report.Create($"Nenhum empréstimo encontrado!"));
                return response;
            }

            foreach (var loan in data)
            {
                loan.Books = await _unitOfWork.LoanRepository.ListBookLoanByLoanIdAsync(loan.Id);
            }

            _unitOfWork.CommitTransaction();

            response.Data = data;

            return response;
        }
        catch
        {
            _unitOfWork.RollbackTransaction();

            return response;
        }



    }
}
