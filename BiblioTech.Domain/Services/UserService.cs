using BiblioTech.Domain.Interface.Services;
using BiblioTech.Domain.Interfaces.Repositories;
using BiblioTech.Domain.Interfaces.Services;
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
    } // Feito 

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

    public async Task<Response> CreateAsync(User user)
    {
        var response = new Response();

        var validation = new UserValidation();
        var errors = validation.Validate(user).GetErrors();

        if (errors.Report.Count > 0) { return errors; }

        await _unitOfWork.UserRepository.CreateAsync(user);

        return response;
    }

    public async Task<Response> UpdateAsync(User user)
    {
        var response = new Response();
        var validation = new UserValidation();
        var errors = validation.Validate(user).GetErrors();

        if (errors.Report.Count > 0) return errors;

        var exists = await _unitOfWork.UserRepository.ExistsByCpfAsync(user.CPF);

        if (!exists) 
        { 
            response.Report.Add(Report.Create($"Usuario {user.Name} not exist"));
            return response;
        }

        await _unitOfWork.UserRepository.UpdateAsync(user);

        return response;
    }

    public async Task<Response> DeleteAsync(string user_id)
    {
        var response = new Response();

        var exists = await _unitOfWork.UserRepository.GetUserByIdAsync(user_id);

        if (exists.Equals(null))
        {
            response.Report.Add(Report.Create($"Usuario {user_id} not exist"));
            return response;
        }

        await _unitOfWork.UserRepository.DeleteAsync(user_id);

        return response;
    }

    public async Task<Response<List<Loan>>> ListAllBooksCheckoutUserAsync(string user_id)
    {
        var response = new Response<List<Loan>>();

        var exist = await _unitOfWork.UserRepository.GetUserByIdAsync(user_id);

        if (exist.Equals(null))
        {
            response.Report.Add(Report.Create($"Usuario {user_id} not exist"));
            return response;
        }

        var data = await _unitOfWork.UserRepository.ListAllBooksCheckoutUserAsync(user_id);

        response.Data = data;

        return response;
    }

    public async Task<Response<List<Reserve>>> ListAllBooksReserveUserAsync(string user_id)
    {
        var response = new Response<List<Reserve>>();

        var exist = await _unitOfWork.UserRepository.GetUserByIdAsync(user_id);

        if (exist.Equals(null))
        {
            response.Report.Add(Report.Create($"Usuario {user_id} not exist"));
            return response;
        }

        var data = await _unitOfWork.UserRepository.ListAllBooksReserveUserAsync(user_id);

        response.Data = data;

        return response;
    }

    public async Task<Response<List<User>>> ListByFilterAsync(string user_id = null, string name = null)
    {
        var response = new Response<List<User>>();

        if (!string.IsNullOrWhiteSpace(user_id))
        {
            var exists = await _unitOfWork.UserRepository.GetUserByIdAsync(user_id);

            if (exists.Equals(null))
            {
                response.Report.Add(Report.Create($"User {user_id} not exists!"));
                return response;
            }
        }

        var data = await _unitOfWork.UserRepository.ListAllUsersByFilterAsync(user_id, name);
        response.Data = data;

        return response;
    }

}
