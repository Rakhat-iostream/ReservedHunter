using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IndustrialStudentPositionHunters.DTO
{
    public class PositionDTO
    {
        [Required]
        public string Name { get; set; }
        public int Amount { get; set; }
    }
}
