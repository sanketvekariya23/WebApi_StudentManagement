using StudentManagementSystem.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace StudentManagementSystem.Data
{
    public class DefaultContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                OnConfiguring(optionsBuilder.UseSqlServer(GetSqlServerConnection()));
            }
        }

        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Marks> Mark { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<User> User { get; set; }

        private static string GetSqlServerConnection()
        {
            SqlConnectionStringBuilder connectionBuilder = new()
            {
                ConnectTimeout = 0,
                DataSource = "DESKTOP-KO7K9QI",
                InitialCatalog = "StudentManagementSystem",
                TrustServerCertificate = true,
                MultipleActiveResultSets = true,
                IntegratedSecurity = true
            };
            return connectionBuilder.ConnectionString;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subject>()
                .HasOne(s => s.Student)
                .WithMany(st => st.Subjects)
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Marks>()
                .HasOne(m => m.Student)
                .WithMany()
                .HasForeignKey(m => m.StudentId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Marks>()
                .HasOne(m => m.Subject)
                .WithMany()
                .HasForeignKey(m => m.SubjectId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}