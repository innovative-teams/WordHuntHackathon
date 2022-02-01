using Core.Entities.Abstract;
using System;

namespace Core.Business
{
    public class DeleteModel : IEntity
    {
        public int ID { get; set; }
        public Guid UUID { get; set; }
    }
}
