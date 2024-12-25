using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCVUpload.Models
{
    public class Depe
    {
        [Key]
        public int DeptId { get; set; }


        public string Department { get; set; }
    }
}
