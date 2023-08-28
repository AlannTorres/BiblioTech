using AutoMapper;
using BiblioTech.Application.DataContract.Request;
using BiblioTech.Application.DataContract.Response;
using BiblioTech.Application.Interfaces;
using BiblioTech.Application.Interfaces.Security;
using BiblioTech.Domain;
using BiblioTech.Domain.Interface.Services;
using BiblioTech.Domain.Interfaces.Services;
using BiblioTech.Domain.Models;
using BiblioTech.Domain.Validations.Base;

namespace BiblioTech.Application.Applications;

public class UserApplication : IUserApplication
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly ISecurityService _securityService;
    private readonly ITokenManager _tokenManager;

    public UserApplication(IUserService userService, 
        IMapper mapper, 
        ISecurityService securityService, 
        ITokenManager tokenManager)
    {
        _userService = userService;
        _mapper = mapper;
        _securityService = securityService;
        _tokenManager = tokenManager;
    }

    public async Task<Response<AuthResponse>> AuthAsync(AuthRequest auth)
    {
        var user = await _userService.GetUserByEmailAsync(auth.Email);

        if (user.Report.Any())
        {
            return Response.Unprocessable<AuthResponse>(user.Report);
        }

        var isAutehticated = await _userService.AutheticationAsync(auth.Password, user.Data);

        if (!isAutehticated.Data)
        {
            return Response.Unprocessable<AuthResponse>(user.Report);
        }

        var token = await _tokenManager.GeneraterTokenAsync(user.Data);

        return new Response<AuthResponse>(token);
    }

    public async Task<Response> CreateUserClientAsync(CreateUserRequest userRequest)
    {
        try
        {
            var passwordEncripted = await _securityService.EncryptPassword(userRequest.Password);

            userRequest.Password = passwordEncripted.Data;

            var user = _mapper.Map<User>(userRequest);

            user.Role = "client";

            return await _userService.CreateUserAsync(user);
        }
        catch (Exception ex)
        {
            var response = Report.Create(ex.Message);

            return Response.Unprocessable(response);
        }
    } 

    public async Task<Response> CreateUserEmployeeAsync(CreateUserRequest userRequest)
    {
        try
        {
            var passwordEncripted = await _securityService.EncryptPassword(userRequest.Password);

            userRequest.Password = passwordEncripted.Data;

            var user = _mapper.Map<User>(userRequest);

            user.Role = "employee";

            return await _userService.CreateUserAsync(user);
        }
        catch (Exception ex)
        {
            var response = Report.Create(ex.Message);

            return Response.Unprocessable(response);
        }
    } 

    public async Task<Response> UpdateUserAsync(CreateUserRequest userRequest, string user_email)
    {
        try
        {
            var userOld = await _userService.GetUserByEmailAsync(user_email);


            if (string.IsNullOrWhiteSpace(userRequest.Password))
            {
                userRequest.Password = userOld.Data.PasswordHash;
            }
            else
            {
                var passwordEncripted = await _securityService.EncryptPassword(userRequest.Password);

                userRequest.Password = passwordEncripted.Data;
            }

            var user = _mapper.Map<User>(userRequest);

            if (string.IsNullOrWhiteSpace(user.Adress)) user.Name = userOld.Data.Name;
            
            if (string.IsNullOrWhiteSpace(user.Adress)) user.Adress = userOld.Data.Adress;
            
            if (string.IsNullOrWhiteSpace(user.Telephone)) user.Telephone = userOld.Data.Telephone;

            if (string.IsNullOrWhiteSpace(user.CPF)) user.CPF = userOld.Data.CPF;

            if (string.IsNullOrWhiteSpace(user.Email)) user.Email = userOld.Data.Email;
            
            user.Role = userOld.Data.Role;

            return await _userService.UpdateUserAsync(user, user_email);
        }
        catch (Exception ex)
        {
            var response = Report.Create(ex.Message);

            return Response.Unprocessable(response);
        }
    }
    
    public async Task<Response> DeleteUserAsync(string user_email)
    {
        return await _userService.DeleteUserAsync(user_email);
    }

    public async Task<Response<List<UserResponse>>> ListEmployeesByFilterAsync(string? user_email = null, string? name = null)
    {
        Response<List<User>> user = await _userService.ListEmployeesByFilterAsync(user_email, name);

        if (user.Report.Any())
            return Response.Unprocessable<List<UserResponse>>(user.Report);

        var response = _mapper.Map<List<UserResponse>>(user.Data);

        return Response.OK(response);
    }

    public async Task<Response<List<LoanResponse>>> ListAllLoanEmployeeAsync(string user_email)
    {
        Response<List<Loan>> user = await _userService.ListAllLoanEmployeesAsync(user_email);

        if (user.Report.Any())
            return Response.Unprocessable<List<LoanResponse>>(user.Report);

        var response = _mapper.Map<List<LoanResponse>>(user.Data);

        return Response.OK(response);
    }

    public async Task<Response<List<ReserveResponse>>> ListAllReserveEmployeeAsync(string user_email)
    {
        Response<List<Reserve>> user = await _userService.ListAllReserveEmployeesAsync(user_email);

        if (user.Report.Any())
            return Response.Unprocessable<List<ReserveResponse>>(user.Report);

        var response = _mapper.Map<List<ReserveResponse>>(user.Data);

        return Response.OK(response);
    }

    public async Task<Response<List<UserResponse>>> ListClientsByFilterAsync(string? user_email = null, string? name = null)
    {
        Response<List<User>> user = await _userService.ListClientsByFilterAsync(user_email, name);

        if (user.Report.Any())
            return Response.Unprocessable<List<UserResponse>>(user.Report);

        var response = _mapper.Map<List<UserResponse>>(user.Data);

        return Response.OK(response);
    }

    public async Task<Response<List<BookLoanResponse>>> ListAllBooksClientAsync(string user_email)
    {
        Response<List<BookLoan>> user = await _userService.ListAllBooksClientAsync(user_email);

        if (user.Report.Any())
            return Response.Unprocessable<List<BookLoanResponse>>(user.Report);

        var response = _mapper.Map<List<BookLoanResponse>>(user.Data);

        return Response.OK(response);
    }

    public async Task<Response<List<ReserveResponse>>> ListAllReserveClientAsync(string user_email)
    {
        Response<List<Reserve>> user = await _userService.ListAllReserveClientAsync(user_email);

        if (user.Report.Any())
            return Response.Unprocessable<List<ReserveResponse>>(user.Report);

        var response = _mapper.Map<List<ReserveResponse>>(user.Data);

        return Response.OK(response);
    }
}
