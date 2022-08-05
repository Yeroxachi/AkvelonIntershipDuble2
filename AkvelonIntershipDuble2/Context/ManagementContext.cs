using AkvelonIntershipDuble2.Entities;
using Microsoft.EntityFrameworkCore;

namespace AkvelonIntershipDuble2.Context
{
    public class ManagementContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> Tasks { get; set; }

        protected ManagementContext()
        {
        }

        public ManagementContext(DbContextOptions options) : base(options)
        {
        }
    }
}