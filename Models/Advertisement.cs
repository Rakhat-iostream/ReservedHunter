using IndustrialStudentPositionHunters.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentPositionHunters.Models
{
    public class Advertisement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdvertisementId { get; set; }
        public int EmployerId { get; set; }
        public Employer Employer { get; set; }
        public string Title { get; set; }
        public uint Salary { get; set; }
        public string Description { get; set; }
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public Position Position { get; set; }
    }
}
