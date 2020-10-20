using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IndustrialStudentPositionHunters.DTO
{
    public class NewsDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime? PublishDate { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
