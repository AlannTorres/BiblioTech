using BiblioTech.Domain.Common.Interfaces;
using BiblioTech.Domain.Interface.Services;
using BiblioTech.Domain.Interfaces.Services;
using BiblioTech.Domain.Validations;
using BiblioTech.Domain.Validations.Base;
using BiblioTech.Interfaces.Repositories;

namespace BiblioTech.Domain.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IGenerators _generators;
    private readonly ISecurityService _securityService;

    public UserService(IUserRepository userRepository,
                       IGenerators generators,
                       ISecurityService securityService)
    {
        _userRepository = userRepository;
        _generators = generators;
        _securityService = securityService;
    }

    public async Task<Response<bool>> AutheticationAsync(string password, User user)
    {
        return await _securityService.VerifyPassword(password, user);
    } // Feito

    public async Task<Response<User>> GetUserByIdAsync(string user_id)
    {
        var response = new Response<User>();

        var data = await _userRepository.GetUserByIdAsync(user_id);

        if (data.Equals(null))
        {
            response.Report.Add(Report.Create($"User {user_id} not exist"));
            return response;
        }

        response.Data = data;

        return response;
    } // Feito

    public async Task<Response<User>> GetUserByEmailAsync(string user_email)
    {
        var response = new Response<User>();

        var data = await _userRepository.GetUserByEmailAsync(user_email);

        if (data.Equals(null))
        {
            response.Report.Add(Report.Create($"User {user_email} not exist"));
            return response;
        }

        response.Data = data;

        return response;
    } // Feito

    public async Task<Response> CreateAsync(User user)
    {
        var response = new Response();

        var validation = new UserValidation();
        var errors = validation.Validate(user).GetErrors();

        if (errors.Report.Count > 0) { return errors; }

        //user.Id = _generators.Generate();

        await _userRepository.CreateAsync(user);

        return response;
    } // Feito

    public async Task<Response> UpdateAsync(User user)
    {
        var response = new Response();
        var validation = new UserValidation();
        var errors = validation.Validate(user).GetErrors();

        if (errors.Report.Count > 0) return errors;

        var exists = await _userRepository.ExistsByCpfAsync(user.CPF);

        if (!exists) 
        { 
            response.Report.Add(Report.Create($"Usuario {user.Name} not exist"));
            return response;
        }

        await _userRepository.UpdateAsync(user);

        return response;
    } // Feito

    public async Task<Response> DeleteAsync(string user_id)
    {
        var response = new Response();

        var exists = await _userRepository.GetUserByIdAsync(user_id);

        if (exists.Equals(null))
        {
            response.Report.Add(Report.Create($"Usuario {user_id} not exist"));
            return response;
        }

        await _userRepository.DeleteAsync(user_id);

        return response;
    } // Feito

    public async Task<Response<List<BookCheckout>>> ListAllBooksCheckoutUserAsync(string user_id)
    {
        var response = new Response<List<BookCheckout>>();

        var exist = await _userRepository.GetUserByIdAsync(user_id);

        if (exist.Equals(null))
        {
            response.Report.Add(Report.Create($"Usuario {user_id} not exist"));
            return response;
        }

        var data = await _userRepository.ListAllBooksCheckoutUserAsync(user_id);

        response.Data = data;

        return response;
    } // Feito

    public async Task<Response<List<BookReserve>>> ListAllBooksReserveUserAsync(string user_id)
    {
        var response = new Response<List<BookReserve>>();

        var exist = await _userRepository.GetUserByIdAsync(user_id);

        if (exist.Equals(null))
        {
            response.Report.Add(Report.Create($"Usuario {user_id} not exist"));
            return response;
        }

        var data = await _userRepository.ListAllBooksReserveUserAsync(user_id);

        response.Data = data;

        return response;
    } // Feito

    public async Task<Response<List<User>>> ListByFilterAsync(string user_id = null, string name = null)
    {
        var response = new Response<List<User>>();

        if (!string.IsNullOrWhiteSpace(user_id))
        {
            var exists = await _userRepository.GetUserByIdAsync(user_id);

            if (exists.Equals(null))
            {
                response.Report.Add(Report.Create($"User {user_id} not exists!"));
                return response;
            }
        }

        var data = await _userRepository.ListByFilterAsync(user_id, name);
        response.Data = data;

        return response;
    } // Feito

}
