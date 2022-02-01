using Business.Abstract;
using Core.Business;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class RefreshTokenManager : BusinessService, IRefreshTokenService
    {
        private readonly IRefreshTokenDal _refreshTokenDal;

        public RefreshTokenManager(IRefreshTokenDal refreshTokenDal)
        {
            _refreshTokenDal = refreshTokenDal;
        }

        public IResult Add(RefreshToken entity)
        {
            _refreshTokenDal.Add(entity);
            return new SuccessResult();
        }

        public IResult Delete(DeleteModel entity)
        {
            var entityToDelete = GetById((int)entity.ID).Data;
            _refreshTokenDal.Delete(entityToDelete);
            return new SuccessResult();
        }

        public IDataResult<List<RefreshToken>> GetAll()
        {
            return new SuccessDataResult<List<RefreshToken>>(_refreshTokenDal.GetAll());
        }

        public IDataResult<RefreshToken> GetById(int id)
        {
            return new SuccessDataResult<RefreshToken>(_refreshTokenDal.Get(r => r.RefreshTokenId == id));
        }

        public IDataResult<RefreshToken> GetByRefreshToken(string refreshToken)
        {
            return new SuccessDataResult<RefreshToken>(_refreshTokenDal.Get(r => r.RefreshTokenValue == refreshToken));
        }

        public IDataResult<List<RefreshToken>> GetByUserId(int userId)
        {
            return new SuccessDataResult<List<RefreshToken>>(_refreshTokenDal.GetAll(r => r.UserId == userId));
        }

        public IResult Update(RefreshToken entity)
        {
            _refreshTokenDal.Update(entity);
            return new SuccessResult();
        }
    }
}