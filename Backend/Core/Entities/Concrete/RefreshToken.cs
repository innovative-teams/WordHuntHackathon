using Core.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Concrete
{
    [Table("RefreshTokens")]
    public class RefreshToken : IEntity
    {
        [Key]
        public int RefreshTokenId { get; set; }
        public int UserId { get; set; }
        public string RefreshTokenValue { get; set; }

        public virtual User User { get; set; }
    }
}