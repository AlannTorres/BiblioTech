﻿using BiblioTech.Domain.Interfaces.Repositories;
using BiblioTech.Domain.Interfaces.Services;
using BiblioTech.Domain.Validations;
using BiblioTech.Domain.Validations.Base;

namespace BiblioTech.Domain.Services;

public class ReserveService : IReserveService
{
    private readonly IUnitOfWork _unitOfWork;

    public ReserveService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response> CreateReserveAsync(Reserve reserve)
    {
        var response = new Response();

        var validation = new ReserveValidation();
        var errors = validation.Validate(reserve).GetErrors();

        if (errors.Report.Count > 0)
            return errors;

        reserve.Reserve_Date = DateTime.Now;

        reserve.EstimatedArrival_Date 
            = await _unitOfWork.ReserveRepository.GetEstimatedArrivalDateByBookId(reserve.Book.Id);

        await _unitOfWork.ReserveRepository.CreateReserveAsync(reserve);

        return response;
    }

    public async Task<Response> CloseReserveAsync(string user_email, string book_id)
    {
        var response = new Response();

        var exist = await _unitOfWork.ReserveRepository.ExistReserveByUserEmail(user_email);

        if (!exist)
        {
            response.Report.Add(Report.Create($"Não existe reservas para o email {user_email}!"));
            return response;
        }

        await _unitOfWork.ReserveRepository.CloseReserveAsync(user_email, book_id);

        return response;
    }

    public async Task<Response<List<Reserve>>> ListAllBookReserveByFilterAsync(string book_name = null, string user_name = null)
    {
        var response = new Response<List<Reserve>>();

        var data = await _unitOfWork.ReserveRepository.ListAllReserveByFilterAsync(book_name, user_name);

        if (data.Equals(null))
        {
            response.Report.Add(Report.Create($"Nenhuma reserva encontrada!"));
            return response;
        }

        response.Data = data;

        return response;
    }
}