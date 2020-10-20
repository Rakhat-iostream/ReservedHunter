using IndustrialStudentPositionHunters.Models;
using Microsoft.EntityFrameworkCore;
using StudentPositionHunters.Models;

namespace StudentPositionHunters.Data
{
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions<EntityContext> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Position> Positions { get; set; }
    }
}
