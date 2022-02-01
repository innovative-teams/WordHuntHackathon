using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class StudentsController : ControllerRepository<Student, int>
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService) : base(studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("[action]")]
        public IActionResult GetByPointForLeaderBord()
        {
            var result = _studentService.GetByPointForLeaderBord();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
