using Core.Entities.Abstract;

namespace Entities.DTOs
{
    public class StudentForUpdateDto : IDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}
