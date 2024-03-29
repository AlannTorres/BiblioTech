﻿using BiblioTech.Application.DataContract.Response;
using BiblioTech.Application.Interfaces.Security;
using BiblioTech.Application.Models;
using BiblioTech.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BiblioTech.Application.Security;

public class TokenManager : ITokenManager
{
    private readonly AuthSettings _authSettings;

    public TokenManager(IOptions<AuthSettings> authSettings)
    {
        _authSettings = authSettings.Value;
    }

    public Task<AuthResponse> GeneraterTokenAsync(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var date = DateTime.UtcNow;
        var expire = date.AddHours(_authSettings.ExpireIn);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.Secret));

        var securityToken = tokenHandler.CreateToken(new SecurityTokenDescriptor()
        {
            Issuer = "bibliotech",
            IssuedAt = date,
            NotBefore = date,
            Expires = expire,
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
            Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                })
        }); 

        var response = new AuthResponse()
        {
            Token = tokenHandler.WriteToken(securityToken),
            ExpireIn = _authSettings.ExpireIn,
            Type = JwtBearerDefaults.AuthenticationScheme
        };

        return Task.FromResult(response);
    }
}
