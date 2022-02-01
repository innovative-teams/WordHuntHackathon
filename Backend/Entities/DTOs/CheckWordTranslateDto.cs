using Core.Entities.Abstract;

namespace Entities.DTOs
{
    public class CheckWordTranslateDto : IDto
    {
        public string CorrectWord { get; set; }
        public bool IsSuccess { get; set; }
    }
}
