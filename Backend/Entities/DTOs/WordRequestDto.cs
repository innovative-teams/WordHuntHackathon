using Core.Entities.Abstract;

namespace Entities.DTOs
{
    public class WordRequestDto : IDto
    {
        public string Word { get; set; }
        public string TargetCode { get; set; }
    }
}
