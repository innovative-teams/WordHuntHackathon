using Core.Entities.Concrete;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper<T>
    {
        AccessToken CreateToken(T user);
        JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, T user, SigningCredentials signingCredentials);
        IEnumerable<Claim> SetClaims(T user);

    }
}