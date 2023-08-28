using BiblioTech.Domain.Interface.Services;
using BiblioTech.Domain.Interfaces.Repositories;
using BiblioTech.Domain.Interfaces.Services;
using BiblioTech.Domain.Models;
using BiblioTech.Domain.Validations;
using BiblioTech.Domain.Validations.Base;

namespace BiblioTech.Domain.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISecurityService _securityService;

    public UserService(IUnitOfWork unitOfWork,
                       ISecurityService securityService)
    {
        _unitOfWork = unitOfWork;
        _securityService = securityService;
    }

    public async Task<Response<bool>> AutheticationAsync(string password, User user)
    {
        return await _securityService.VerifyPassword(password, user);
    } 

    public async Task<Response<User>> GetUserByEmailAsync(string user_email)
    {
        var response = new Response<User>();

        var data = await _unitOfWork.UserRepository.GetUserByEmailAsync(user_email);

        if (data.Equals(null))
        {
            response.Report.Add(Report.Create($"User {user_email} not exist"));
            return response;
        }

        response.Data = data;

        return response;
    }

    public async Task<Response> CreateUserAsync(User user)
    {
        var response = new Response();

        var validation = new UserValidation();
        var errors = validation.Validate(user).GetErrors();

        if (errors.Report.Count > 0) { return errors; }

        await _unitOfWork.UserRepository.CreateUserAsync(user);

        return response;
    }

    public async Task<Response> UpdateUserAsync(User user, string user_email)
    {
        var response = new Response();
        var validation = new UserValidation();
        var errors = validation.Validate(user).GetErrors();

        if (errors.Report.Count > 0) return errors;

        var exists = await _unitOfWork.UserRepository.GetUserByEmailAsync(user_email);

        if (exists.Equals(null)) 
        { 
            response.Report.Add(Report.Create($"Usuario com email:{user_email} não existe"));
            return response;
        }

        await _unitOfWork.UserRepository.UpdateUserAsync(user, user_email);

        return response;
    }

    public async Task<Response> DeleteUserAsync(string user_email)
    {
        var response = new Response();

        var exists = await _unitOfWork.UserRepository.GetUserByEmailAsync(user_email);

        if (exists.Equals(null))
        {
            response.Report.Add(Report.Create($"Usuario {user_email} not exist"));
            return response;
        }

        await _unitOfWork.UserRepository.DeleteUserAsync(user_email);

        return response;
    }

    public async Task<Response<List<User>>> ListEmployeesByFilterAsync(string user_email = null, string name = null)
    {
        var response = new Response<List<User>>();

        if (!string.IsNullOrWhiteSpace(user_email))
        {
            var exists = await _unitOfWork.UserRepository.GetUserByEmailAsync(user_email);

            if (exists.Equals(null))
            {
                response.Report.Add(Report.Create($"Usuario com email: {user_email} não existe!"));
                return response;
            }
        }

        var data = await _unitOfWork.UserRepository.ListEmployeesByFilterAsync(user_email, name);
        response.Data = data;

        return response;
    }

    public async Task<Response<List<Loan>>> ListAllLoanEmployeesAsync(string user_email)
    {
        var response = new Response<List<Loan>>();

        if (!string.IsNullOrWhiteSpace(user_email))
        {
            var exists = await _unitOfWork.UserRepository.GetUserByEmailAsync(user_email);

            if (exists.Equals(null))
            {
                response.Report.Add(Report.Create($"Usuario com email: {user_email} não existe!"));
                return response;
            }
        }

        var data = await _unitOfWork.UserRepository.ListAllLoanEmployeeAsync(user_email);
        response.Data = data;

        return response;
    }

    public async Task<Response<List<Reserve>>> ListAllReserveEmployeesAsync(string user_email)
    {
        var response = new Response<List<Reserve>>();

        if (!string.IsNullOrWhiteSpace(user_email))
        {
            var exists = await _unitOfWork.UserRepository.GetUserByEmailAsync(user_email);

            if (exists.Equals(null))
            {
                response.Report.Add(Report.Create($"Usuario com email: {user_email} não existe!"));
                return response;
            }
        }

        var data = await _unitOfWork.UserRepository.ListAllReserveEmployeeAsync(user_email);
        response.Data = data;

        return response;
    }

    public async Task<Response<List<User>>> ListClientsByFilterAsync(string user_email = null, string name = null)
    {
        var response = new Response<List<User>>();

        if (!string.IsNullOrWhiteSpace(user_email))
        {
            var exists = await _unitOfWork.UserRepository.GetUserByEmailAsync(user_email);

            if (exists.Equals(null))
            {
                response.Report.Add(Report.Create($"Usuario com email: {user_email} não existe!"));
                return response;
            }
        }

        var data = await _unitOfWork.UserRepository.ListClientsByFilterAsync(user_email, name);
        response.Data = data;

        return response;
    }

    public async Task<Response<List<BookLoan>>> ListAllBooksClientAsync(string user_email)
    {
        var response = new Response<List<BookLoan>>();

        if (!string.IsNullOrWhiteSpace(user_email))
        {
            var exists = await _unitOfWork.UserRepository.GetUserByEmailAsync(user_email);

            if (exists.Equals(null))
            {
                response.Report.Add(Report.Create($"Usuario com email: {user_email} não existe!"));
                return response;
            }
        }

        var data = await _unitOfWork.UserRepository.ListAllBooksClientAsync(user_email);
        response.Data = data;

        return response;
    }

    public async Task<Response<List<Reserve>>> ListAllReserveClientAsync(string user_email)
    {
        var response = new Response<List<Reserve>>();

        if (!string.IsNullOrWhiteSpace(user_email))
        {
            var exists = await _unitOfWork.UserRepository.GetUserByEmailAsync(user_email);

            if (exists.Equals(null))
            {
                response.Report.Add(Report.Create($"Usuario com email: {user_email} não existe!"));
                return response;
            }
        }

        var data = await _unitOfWork.UserRepository.ListAllReserveClientAsync(user_email);
        response.Data = data;

        return response;
    }
}
