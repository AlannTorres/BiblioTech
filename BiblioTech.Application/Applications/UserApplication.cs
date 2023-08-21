using AutoMapper;
using BiblioTech.Application.DataContract.Request;
using BiblioTech.Application.Interfaces;
using BiblioTech.Domain.Interface.Services;
using BiblioTech.Domain.Validations.Base;
using BiblioTech.Domain;
using BiblioTech.Application.DataContract.Response;

namespace BiblioTech.Application.Applications;

public class UserApplication : IUserApplication
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserApplication(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<Response> CreateAsync(CreateUserRequest user)
    {
        // Transformar o CreateUserResquest em UserModel
        var userMapper = _mapper.Map<User>(user);

        return await _userService.CreateAsync(userMapper);
    }

    public async Task<Response<UserResponse>> GetByIdAsync(int user_id)
    {
        Response<User> user = await _userService.GetUserByIdAsync(user_id);

        if (user.Report.Any())
            return Response.Unprocessable<UserResponse>(user.Report);

        var response = _mapper.Map<UserResponse>(user.Data);

        return Response.OK(response);
    }

    public async Task<Response<List<BookCheckoutResponse>>> ListBooksCheckoutUser(int user_id)
    {
        Response<List<BookCheckout>> bookCheckout = await _userService.ListAllBooksCheckoutUserAsync(user_id);

        if (bookCheckout.Report.Any())
            return Response.Unprocessable<List<BookCheckoutResponse>>(bookCheckout.Report);

        var response = _mapper.Map<List<BookCheckoutResponse>>(bookCheckout.Data);

        return Response.OK(response);
    }
}
