﻿namespace BiblioTech.Application.DataContract.Response;

public sealed class UserResponse
{
    public long Id { get; set; }
    public string CPF { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Address { get; set; }
    public string Telephone { get; set; }
}