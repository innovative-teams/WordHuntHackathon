using Core.Entities.Abstract;
using Entities.Concrete;

namespace Entities.DTOs
{
    public class VerifyTheWordRequestDto : IDto
    {
        public Word Word { get; set; }
        public string NewWord { get; set; }
    }
}
