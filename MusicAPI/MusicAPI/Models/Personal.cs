using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicAPI.Models
{
    [Table("Personal")]
    public class Personal
    {
        [Key]
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string EmployeeName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public char Gender { get; set; }

        public int? PersonId { get; set; }
    }
}
