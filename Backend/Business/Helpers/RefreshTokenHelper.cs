using Business.Abstract;
using Core.Entities.Concrete;
using System;
using System.Security.Cryptography;

namespace Business.Helpers
{
    public class RefreshTokenHelper : BusinessService, IRefreshTokenHelper
    {
        private readonly IRefreshTokenService _refreshTokenService;
        private string _refreshToken;

        public RefreshTokenHelper(IRefreshTokenService refreshTokenService)
        {
            _refreshTokenService = refreshTokenService;
        }

        public string CreateRefreshToken()
        {
            var number = new byte[32];
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }

        public void CreateDifferentRefreshToken(RefreshToken refreshToken)
        {
            while (_refreshTokenService.GetByRefreshToken(refreshToken.RefreshTokenValue).Data != null)
                refreshToken.RefreshTokenValue = CreateRefreshToken();
        }

        public RefreshToken CreateNewRefreshToken(User user)
        {
            var newRefreshToken = new RefreshToken
            {
                UserId = user.UserId,
                RefreshTokenValue = CreateRefreshToken(),
            };

            CreateDifferentRefreshToken(newRefreshToken);
            var oldRefreshToken = _refreshTokenService.GetByRefreshToken(_refreshToken).Data;

            if (oldRefreshToken != null)
            {
                newRefreshToken.RefreshTokenId = oldRefreshToken.RefreshTokenId;
                _refreshTokenService.Update(newRefreshToken);
            }
            else
            {
                _refreshTokenService.Add(newRefreshToken);
            }

            return newRefreshToken;
        }

        public RefreshToken UpdateOldRefreshToken()
        {
            _refreshToken = HttpContextAccessor.HttpContext.Request.Headers["RefreshToken"];

            if (!Control(_refreshToken))
                return null;

            var newRefreshToken = _refreshTokenService.GetByRefreshToken(_refreshToken).Data;
            if (newRefreshToken != null)
            {
                CreateDifferentRefreshToken(newRefreshToken);
                _refreshTokenService.Update(newRefreshToken);
                return newRefreshToken;
            }

            return null;
        }

        public bool Control(params string[] args)
        {
            foreach (string arg in args)
            {
                if (arg == null || arg == "" || arg == "null")
                    return false;
            }

            return true;
        }
    }
}
