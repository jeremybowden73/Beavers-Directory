using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeaversDirectory.Models
{
    public class Beaver
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Town { get; set; }

        [Required]
        public string Dob { get; set; }

        [Required]
        public string Lodge { get; set; }

        public bool IsLodgeLeader { get; set; }
        
        public string ParentEmail { get; set; }

        public string ParentPassword { get; set; }

    }
}
