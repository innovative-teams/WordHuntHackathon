using Business.Abstract;
using Core.Entities.DTOs;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("[action]")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var result = _authService.Login(userForLoginDto);
            if (result.Success) return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("[action]")]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var result = _authService.Register(userForRegisterDto);
            if (result.Success) return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("[action]")]
        public IActionResult RefreshToken()
        {
            var result = _authService.RefreshToken();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("[action]")]
        public IActionResult UpdateStudent(StudentForUpdateDto studentForUpdateDto)
        {
            var result = _authService.UpdateStudent(studentForUpdateDto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}