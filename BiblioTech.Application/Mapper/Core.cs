using AutoMapper;
using BiblioTech.Application.DataContract.Request;
using BiblioTech.Application.DataContract.Response;
using BiblioTech.Domain;

namespace BiblioTech.Application.Mapper;

public class Core : Profile
{
    public Core()
    {
        UserMap();
    }

    private void UserMap()
    {
        CreateMap<CreateUserRequest, User>();

        CreateMap<User, UserResponse>();
    }
}
