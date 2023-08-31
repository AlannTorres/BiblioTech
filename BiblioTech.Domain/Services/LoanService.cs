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

        foreach (var book in loan.Books)
        {
            int quantity = await _unitOfWork.BookRepository.GetQuantityBookAsync(book.Book.Id);

            if (quantity == 0)
            {
                response.Report.Add(Report.Create($"O livro de id: {book.Id} não esta disponivel!"));
                return response;
            }
        }

        var validation = new LoanValidation();
        var errors = validation.Validate(loan).GetErrors();

        if (errors.Report.Count > 0)
            return errors;

        loan.Loan_Date = DateTime.Now;

        foreach (var book in loan.Books)
        {
            book.Loan = loan;
            book.Loan_Status = Enum.LoanEnum.Pending;
            book.Devolution_Date = DateTime.Now + TimeSpan.FromDays(days);

            int newquantity = await _unitOfWork.BookRepository.GetQuantityBookAsync(book.Book.Id);
            await _unitOfWork.BookRepository.UpdateQuantityBookAsync(book.Book.Id, newquantity - 1);
        }

        await _unitOfWork.LoanRepository.CreateLoanAsync(loan);

        return response;
    }

    public async Task<Response> RegisterReturnAsync(string user_email, string book_id)
    {
        var response = new Response();

        var exist = await _unitOfWork.LoanRepository.ExistLoanByUserEmail(user_email);

        if (!exist)
        {
            response.Report.Add(Report.Create($"Não existe Emprestimos para o email {user_email}!"));
            return response;
        }

        int newquantity = await _unitOfWork.BookRepository.GetQuantityBookAsync(book_id);
        await _unitOfWork.BookRepository.UpdateQuantityBookAsync(book_id, newquantity + 1);
        await _unitOfWork.LoanRepository.RegisterReturnBookLoanAsync(user_email, book_id);

        return response;
    }
    
    public async Task<Response<List<Loan>>> ListAllLoanByFilterAsync(string user_name = null)
    {
        var response = new Response<List<Loan>>();

        var data = await _unitOfWork.LoanRepository.ListAllLoanByFilterAsync(user_name);

        if (data.Equals(null))
        {
            response.Report.Add(Report.Create($"Nenhum empréstimo encontrado!"));
            return response;
        }

        response.Data = data;

        return response;
    }
}
