using BiblioTech.Domain.Interface.Services;
using BiblioTech.Domain.Validations;
using BiblioTech.Domain.Validations.Base;
using BiblioTech.Interfaces.Repositories;

namespace BiblioTech.Domain.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    } 

    async Task<Response> IUserService.AutheticationAsync(User user)
    {
        throw new NotImplementedException();
    } 

    async Task<Response<User>> IUserService.GetUserByIdAsync(int user_id)
    {
        var response = new Response<User>();

        var data = await _userRepository.GetUserByIdAsync(user_id);

        if (data.Equals(null))
        {
            response.Report.Add(Report.Create($"Usuario {user_id} not exist"));
            return response;
        }

        response.Data = data;

        return response;
    } // Feito

    async Task<Response> IUserService.CreateAsync(User user)
    {
        var response = new Response();
        var validation = new UserValidation();
        var errors = validation.Validate(user).GetErrors();

        if (errors.Report.Count > 0){ return errors; }

        await _userRepository.CreateAsync(user);

        return response;
    } // Feito

    async Task<Response> IUserService.UpdateAsync(User user)
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
     
    async Task<Response> IUserService.DeleteAsync(int user_id)
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

    async Task<Response<List<BookCheckout>>> IUserService.ListAllBooksCheckoutUserAsync(int user_id)
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

    async Task<Response<List<BookReserve>>> IUserService.ListAllBooksReserveUserAsync(int user_id)
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

}
