using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IndustrialStudentPositionHunters.DTO
{
    public class AdvertisementDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public uint? Salary { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string PositionName { get; set; }
    }
}
