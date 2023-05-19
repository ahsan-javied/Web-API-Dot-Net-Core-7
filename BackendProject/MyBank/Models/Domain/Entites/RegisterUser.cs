using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Models.Domain.Entites
{
    public class RegisterUser : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RegisterUserId { get; set; }
        public int? UserId { get; set; }

        [Required]
        [EmailAddress, MaxLength(100)]
        public string Username { get; set; }

        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }

        [MaxLength(100)]
        public string Device { get; set; }

        [MaxLength(100)]
        public string Ipaddress { get; set; }
        [MaxLength(250)]

        public string Token { get; set; }
        public string Browser { get; set; }
        public DateTime? LastLogin { get; set; }

        public virtual User User { get; set; }
    }
}
