using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentPositionHunters.Models
{
    public class Resume
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Path { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
