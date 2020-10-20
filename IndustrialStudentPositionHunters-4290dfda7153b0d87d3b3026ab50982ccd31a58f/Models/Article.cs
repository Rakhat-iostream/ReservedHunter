using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.DTO;

namespace StudentPositionHunters.Models
{
    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public  string PublishDateString
        {
            get {
                var formatProvider = new DateTimeFormatInfo
                {
                    ShortDatePattern = "dd MMM yyyy",
                    AbbreviatedMonthNames = "Январь,Февраль,Март,Апрель,Май,Июнь,Июль,Август,Сентябрь,Октябрь,Ноябрь,Декабрь,"
                    .Split(',')
                };
                return PublishDate.ToString("D", formatProvider);
            }
        }
        public string Description { get; set; }
    }
}
