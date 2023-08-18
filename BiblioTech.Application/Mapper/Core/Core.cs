using AutoMapper;
using BiblioTech.Application.DataContract.Request;
using BiblioTech.Application.DataContract.Response;
using LibrarySystem_2.Domain;

namespace BiblioTech.Application.Mapper.Core;

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
