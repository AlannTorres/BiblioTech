using AutoMapper;
using BiblioTech.Application.DataContract.Request;
using BiblioTech.Application.DataContract.Response;
using BiblioTech.Domain;
using BiblioTech.Domain.Models;

namespace BiblioTech.Application.Mapper;

public class Core : Profile
{
    public Core()
    {
        UserMap();
    }

    private void UserMap()
    {
        CreateMap<User, UserResponse>();
        CreateMap<CreateUserRequest, User>()
            .ForPath(target => target.PasswordHash,
                       opt => opt.MapFrom(source => source.Password));

        CreateMap<Loan, LoanResponse>();
        CreateMap<CreateLoanRequest, Loan>()
            .ForPath(target => target.User.Id, 
                       opt  => opt.MapFrom(source => source.User_id))
            .ForPath(target => target.Employee.Id,
                       opt => opt.MapFrom(source => source.Employee_id));

        CreateMap<BookLoan, BookLoanResponse>();
        CreateMap<CreateBookLoanRequest, BookLoan>()
            .ForPath(target => target.Book.Id,
                     opt => opt.MapFrom(source => source.Book_id));

        CreateMap<Reserve, ReserveResponse>();
        CreateMap<CreateReserveRequest, Reserve>()
            .ForPath(target => target.User.Id,
                       opt => opt.MapFrom(source => source.User_id))
            .ForPath(target => target.Book.Id,
                       opt => opt.MapFrom(source => source.Book_id))
            .ForPath(target => target.Employee.Id,
                       opt => opt.MapFrom(source => source.Employee_id));

        CreateMap<Book, BookResponse>();
        CreateMap<UpdateBookRequest, Book>();
        CreateMap<CreateBookRequest, Book>();
    }
}
