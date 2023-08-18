using BiblioTech.Application.DataContract.Request;
using BiblioTech.Domain.Validations.Base;

namespace BiblioTech.Application.Interfaces;

public interface IUserApplication
{
    Task<Response> CreateAsync(CreateUserRequest user);

}
