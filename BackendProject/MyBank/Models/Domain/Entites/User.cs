using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Models.Domain.Entites
{
    public class User : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress, MaxLength(100)]
        public string Username { get; set; }

        public decimal Balance { get; set; } = 0;

        [NotMapped]
        public int RegisterUserId { get; set; }
        public virtual RegisterUser RegisterUser { get; set; }
    }
}
