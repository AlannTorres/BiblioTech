using AutoMapper;
using BiblioTech.Application.DataContract.Request;
using BiblioTech.Application.DataContract.Response;
using BiblioTech.Application.Interfaces;
using BiblioTech.Application.Interfaces.Security;
using BiblioTech.Domain;
using BiblioTech.Domain.Interface.Services;
using BiblioTech.Domain.Interfaces.Services;
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

    public async Task<Response> CreateAsync(CreateUserRequest userRequest)
    {
        try
        {
            var passwordEncripted = await _securityService.EncryptPassword(userRequest.Password);

            userRequest.Password = passwordEncripted.Data;

            var user = _mapper.Map<User>(userRequest);

            return await _userService.CreateAsync(user);
        }
        catch (Exception ex)
        {
            var response = Report.Create(ex.Message);

            return Response.Unprocessable(response);
        }
    }

    public async Task<Response<UserResponse>> GetByIdAsync(string user_id)
    {
        Response<User> user = await _userService.GetUserByIdAsync(user_id);

        if (user.Report.Any())
            return Response.Unprocessable<UserResponse>(user.Report);

        var response = _mapper.Map<UserResponse>(user.Data);

        return Response.OK(response);
    }

    public async Task<Response<List<BookCheckoutResponse>>> ListBooksCheckoutUser(string user_id)
    {
        Response<List<Loan>> bookCheckout = await _userService.ListAllBooksCheckoutUserAsync(user_id);

        if (bookCheckout.Report.Any())
            return Response.Unprocessable<List<BookCheckoutResponse>>(bookCheckout.Report);

        var response = _mapper.Map<List<BookCheckoutResponse>>(bookCheckout.Data);

        return Response.OK(response);
    }

    public async Task<Response<List<UserResponse>>> ListByFilterAsync(string user_id = null, string name = null)
    {
        try
        {
            Response<List<User>> user = await _userService.ListByFilterAsync(user_id, name);
            
            if (user.Report.Any())
            {
                return Response.Unprocessable<List<UserResponse>>(user.Report);
            }

            var response = _mapper.Map<List<UserResponse>>(user.Data);

            return Response.OK(response);
        }
        catch (Exception ex)
        {
            var response = Report.Create(ex.Message);

            return Response.Unprocessable<List<UserResponse>>(new List<Report> { response });
        }
    }
}
