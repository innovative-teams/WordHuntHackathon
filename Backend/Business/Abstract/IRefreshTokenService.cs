using Core.Business;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IRefreshTokenService : IServiceRepository<RefreshToken, int>
    {
        IDataResult<RefreshToken> GetByRefreshToken(string refreshToken);
        IDataResult<List<RefreshToken>> GetByUserId(int userId);
    }
}