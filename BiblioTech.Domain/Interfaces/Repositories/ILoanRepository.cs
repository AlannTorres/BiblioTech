using BiblioTech.Domain;
using BiblioTech.Domain.Models;

namespace BiblioTech.Interfaces.Repositories;

public interface ILoanRepository
{
    Task CreateLoanAsync(Loan loan);
    Task CreateBookLoanAsync(BookLoan bookLoan, int days);
    Task RegisterReturnBookLoanAsync(string user_email, string book_id);
    Task<bool> ExistLoanByUserEmail(string user_email);
    Task<List<BookLoan>> ListBookLoanByLoanIdAsync(string loan_id);
    Task<List<Loan>> ListAllLoanByFilterAsync(string book_name = null, string user_name = null);
}
