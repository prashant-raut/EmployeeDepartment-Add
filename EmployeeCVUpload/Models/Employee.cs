using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCVUpload.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Mobile { get; set; }
        [Required]
        public string Description { get; set; }
        
        [NotMapped]
        public string Department { get; set; }
        public int DeptId { get; set; }
    }
}
