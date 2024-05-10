using System.Configuration;
using EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Data
{
    public class EFCoreContext : DbContext
    {
        public EFCoreContext(DbContextOptions<EFCoreContext> options) : base(options)
        {
        }

        public DbSet<Departments> Departments { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Project_Employee> ProjectEmployees { get; set; }
        public DbSet<Salaries> Salaries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent
            modelBuilder.Entity<Departments>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Employees>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Projects>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Salaries>()
                .HasKey(x => x.Id);


            modelBuilder.Entity<Employees>()
                .HasOne(e => e.Salary)
                .WithOne(s => s.Employee)
                .HasForeignKey<Salaries>(s => s.EmployeeId);

            modelBuilder.Entity<Departments>()
                .HasMany(d => d.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId);

            modelBuilder.Entity<Project_Employee>()
                .HasKey(pe => new { pe.ProjectId, pe.EmployeeId });

            modelBuilder.Entity<Project_Employee>()
                .HasOne(pe => pe.Project)
                .WithMany(p => p.ProjectEmployees)
                .HasForeignKey(pe => pe.ProjectId);

            modelBuilder.Entity<Project_Employee>()
                .HasOne(pe => pe.Employee)
                .WithMany(e => e.ProjectEmployee)
                .HasForeignKey(pe => pe.EmployeeId);

            modelBuilder.Entity<Departments>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Employees>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30);
            modelBuilder.Entity<Employees>()
                .Property(e => e.DepartmentId)
                .IsRequired()
                .HasMaxLength(1);
            modelBuilder.Entity<Employees>()
                .Property(e => e.JoinedDate)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<Projects>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Project_Employee>()
                .Property(e => e.ProjectId)
                .IsRequired()
                .HasMaxLength(5);
            modelBuilder.Entity<Project_Employee>()
                .Property(e => e.EmployeeId)
                .IsRequired()
                .HasMaxLength(5);
            modelBuilder.Entity<Project_Employee>()
                .Property(e => e.Enable)
                .IsRequired()
                .HasMaxLength(1);

            modelBuilder.Entity<Salaries>()
                .Property(e => e.EmployeeId)
                .IsRequired()
                .HasMaxLength(5);
            modelBuilder.Entity<Salaries>()
                .Property(e => e.Salary)
                .IsRequired()
                .HasMaxLength(20);


            // Seeding data
            modelBuilder.Entity<Departments>().HasData(
                new Departments { Id = 1, Name = "Software Development" },
                new Departments { Id = 2, Name = "Finance" },
                new Departments { Id = 3, Name = "Accountant" },
                new Departments { Id = 4, Name = "HR" }
            );
        }
    }
}
