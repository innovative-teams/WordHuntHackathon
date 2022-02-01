using Business.Abstract;
using Business.Helpers;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Authorization;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Business;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.Entities.Concrete;
using Core.Entities.DTOs;
using Core.Utilities.Exceptions;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class AuthManager : BusinessService, IAuthService
    {
        private readonly IRefreshTokenHelper _refreshTokenHelper;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IUserService _userService;
        private readonly IStudentService _studentService;

        public AuthManager(IUserService userService, IRefreshTokenHelper refreshTokenHelper, IRefreshTokenService refreshTokenService, IStudentService studentService)
        {
            _userService = userService;
            _refreshTokenHelper = refreshTokenHelper;
            _refreshTokenService = refreshTokenService;
            _studentService = studentService;
        }


        [TransactionScopeAspect]
        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var accessToken = UserTokenHelper.CreateToken(user);

            string refreshToken = HttpContextAccessor.HttpContext.Request.Headers["RefreshToken"];
            if (_refreshTokenHelper.Control(refreshToken) && _refreshTokenService.GetByRefreshToken(refreshToken).Data != null)
                accessToken.RefreshToken = _refreshTokenHelper.UpdateOldRefreshToken();
            else
                accessToken.RefreshToken = _refreshTokenHelper.CreateNewRefreshToken(user);

            if (accessToken.RefreshToken == null)
                return new ErrorDataResult<AccessToken>();

            return new SuccessDataResult<AccessToken>(accessToken);
        }

        public IDataResult<AccessToken> RefreshToken()
        {
            string refreshToken = HttpContextAccessor.HttpContext.Request.Headers["RefreshToken"];

            var newRefreshToken = _refreshTokenService.GetByRefreshToken(refreshToken).Data;
            if (newRefreshToken != null)
            {
                var user = _userService.GetByIdForAuth(newRefreshToken.UserId).Data;
                return RefreshTokenControl(user);
            }

            RequestUserService.SetRequestUser(null);
            return new ErrorDataResult<AccessToken>();
        }

        [TransactionScopeAspect]
        [ValidationAspect(typeof(LoginValidator))]
        public IDataResult<AccessToken> Login(UserForLoginDto userForLoginDto)
        {
            var result = BusinessRules.Run(
                CheckIfUserIsNotExists(userForLoginDto)
            );

            if (!result.Success)
                return new ErrorDataResult<AccessToken>(result.Message);

            var user = _userService.GetByEmailForAuth(userForLoginDto.Email).Data;
            return new SuccessDataResult<AccessToken>(CreateAccessToken(user).Data, BusinessMessages.SuccessfulLogin());
        }

        [TransactionScopeAspect]
        [ValidationAspect(typeof(RegisterValidator))]
        public IDataResult<AccessToken> Register(UserForRegisterDto userForRegisterDto)
        {
            var result = BusinessRules.Run(
                CheckIfUserIsAlreadyExists(userForRegisterDto.Email)
            );

            if (!result.Success)
                return new ErrorDataResult<AccessToken>(result.Message);

            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out var passwordHash, out var passwordSalt);
            var user = new Student
            {
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                Age = userForRegisterDto.Age,
                Point = 0,
                Email = userForRegisterDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            _studentService.Add(user);
            return new SuccessDataResult<AccessToken>(CreateAccessToken(user).Data, BusinessMessages.SuccessfulRegister());
        }



        [LoginRequired]
        [ValidationAspect(typeof(RegisterValidator))]
        public IDataResult<AccessToken> UpdateStudent(StudentForUpdateDto studentForUpdateDto)
        {
            var student = _studentService.GetByEmail(studentForUpdateDto.Email).Data;
            student.FirstName = studentForUpdateDto.FirstName;
            student.LastName = studentForUpdateDto.LastName;
            student.Age = studentForUpdateDto.Age;

            if (!HashingHelper.VerifyPasswordHash(studentForUpdateDto.Password, student.PasswordHash, student.PasswordSalt))
            {
                return new ErrorDataResult<AccessToken>("Parola Yanlış");
            }

            if (studentForUpdateDto.NewPassword != null && studentForUpdateDto.NewPassword != "")
            {
                var errors = ValidationTool.Validate(new StudentUpdateValidator(), studentForUpdateDto);
                if (errors?.Count > 0)
                {
                    var validationErrors = new List<string>();
                    foreach (var error in errors) validationErrors.Add(error.ErrorMessage);
                    throw new ValidationException(CoreMessages.ValidationError(), validationErrors);
                }

                HashingHelper.CreatePasswordHash(studentForUpdateDto.NewPassword, out var passwordHash, out var passwordSalt);
                student.PasswordHash = passwordHash;
                student.PasswordSalt = passwordSalt;
            }

            _studentService.Update(student);

            var user = _userService.GetById(student.UserId).Data;
            return new SuccessDataResult<AccessToken>(CreateAccessToken(user).Data, "Profil Güncellendi");
        }

        private IResult CheckIfUserIsAlreadyExists(string email)
        {
            if (_userService.GetByEmailForAuth(email).Data != null)
                return new ErrorResult(BusinessMessages.UserIsAlreadyExists());

            return new SuccessResult();
        }

        private IResult CheckIfUserIsNotExists(UserForLoginDto userForLoginDto)
        {
            var user = _userService.GetByEmailForAuth(userForLoginDto.Email).Data;
            if (user == null)
                return new ErrorResult(BusinessMessages.UserIsNotExists());

            if (user != null)
            {
                if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, user.PasswordHash, user.PasswordSalt))
                    return new ErrorResult(BusinessMessages.PasswordIsNotTrue());
            }

            return new SuccessResult();
        }

        private IDataResult<AccessToken> RefreshTokenControl(User user)
        {
            var result = CreateAccessToken(user);
            if (result.Success) return result;

            RequestUserService.SetRequestUser(null);
            return result;
        }
    }
}