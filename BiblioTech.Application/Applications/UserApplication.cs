using AutoMapper;
using BiblioTech.Application.DataContract.Request;
using BiblioTech.Application.Interfaces;
using BiblioTech.Domain.Interface.Services;
using BiblioTech.Domain.Validations.Base;
using BiblioTech.Domain;

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
        var cliente = _mapper.Map<User>(user);

        return await _userService.CreateAsync(cliente);
    }
}
