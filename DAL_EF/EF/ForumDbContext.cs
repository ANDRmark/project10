using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_EF.EF
{
    class ForumDbContext : DbContext
    {
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserInfo> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

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
            modelBuilder.Entity<Message>().HasRequired(m => m.User).WithMany().HasForeignKey(m => m.UserId).WillCascadeOnDelete(true);
            modelBuilder.Entity<UserInfo>().HasMany(u => u.Roles).WithMany(r => r.Users);
            modelBuilder.Entity<Role>().Property(r => r.Name).IsRequired().HasMaxLength(50).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("Index") { IsUnique = true } }));
            base.OnModelCreating(modelBuilder);
        }
    }
}
