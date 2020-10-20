using IndustrialStudentPositionHunters.DTO;
using StudentPositionHunters.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IndustrialStudentPositionHunters.Models
{
    public class Position
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PositionId { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public IList<Advertisement> Advertisements { get; set; }
    }
}
