using Core.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{

    [Table("Words")]
    public class Word : IEntity
    {
        [Key]
        public Guid WordId { get; set; }
        public int StudentId { get; set; }
        public string SearchedWord { get; set; }
        public string SearchedWordCode { get; set; }
        public string ResultWord { get; set; }
        public string ResultWordCode { get; set; }
        public bool IsShowed { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Student Student { get; set; }
    }
}
