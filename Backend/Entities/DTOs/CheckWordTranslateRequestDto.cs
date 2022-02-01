using Core.Entities.Abstract;

namespace Entities.DTOs
{
    public class CheckWordTranslateRequestDto : IDto
    {
        public string Word { get; set; }
        public string Option { get; set; }
    }
}
