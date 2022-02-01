using Core.Entities.Concrete;
using Core.Entities.DTOs;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Security.JWT;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<AccessToken> Register(UserForRegisterDto userForRegisterDto);
        IDataResult<AccessToken> Login(UserForLoginDto userForLoginDto);
        IDataResult<AccessToken> CreateAccessToken(User user);
        IDataResult<AccessToken> RefreshToken(); 
        IDataResult<AccessToken> UpdateStudent(StudentForUpdateDto studentForUpdateDto);
    }
}