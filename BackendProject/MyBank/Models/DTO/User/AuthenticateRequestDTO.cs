using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.DTO.User
{
    public class AuthenticateRequestDTO
    {
        [Required]
        [EmailAddress]
        [StringLength(maximumLength: 250, MinimumLength = 5)]
        public string Username { get; set; }

        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 5)]
        public string Password { get; set; }

        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string Device { get; set; }

        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string Ipaddress { get; set; }

        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 3)] 
        public string Browser { get; set; }
    }
}
