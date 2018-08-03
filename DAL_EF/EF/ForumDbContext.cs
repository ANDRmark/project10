using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_EF.EF
{
    class ForumDbContext : DbContext
    {
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Message> Messages { get; set; }

        public ForumDbContext(string connectionString):base(connectionString)
        {

        }
        static ForumDbContext()
        {
            Database.SetInitializer(new ForumDbContextInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>().HasRequired(m => m.Theme).WithMany(t => t.Messages).HasForeignKey(m => m.ThemeId).WillCascadeOnDelete(true);
            base.OnModelCreating(modelBuilder);
        }
    }
}
