using Core.Entities.Abstract;

namespace Entities.DTOs
{

    public class NewWordDto : IDto
    {
        public string Word { get; set; }
        public string Result { get; set; }
    }
}
