using Microsoft.EntityFrameworkCore;
using ScholarshipPortal.Models;

namespace ScholarshipPortal.Data
{
    // ApplicationDbContext will be used to communicate with the SQL Server database
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets represent the tables in the database
        public DbSet<RegisterViewModel> RegisteredUsers { get; set; }
        public DbSet<ContactViewModel> ContactSubmissions { get; set; }
        public DbSet<InstituteRegistration> Institutes { get; set; }

        public DbSet<MinistryUser> MinistryUsers { get; set; }

        // *** CRITICAL FIX: ADDED NEW DbSet for Scholarship Application ***
        public DbSet<ScholarshipApplicationViewModel> ScholarshipApplications { get; set; }

        public DbSet<StateOfficer> StateUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure keys and types for RegisterViewModel
            modelBuilder.Entity<RegisterViewModel>()
                .HasKey(r => r.Email);
            modelBuilder.Entity<RegisterViewModel>()
                .Property(r => r.DOB)
                .HasColumnType("date");

            // Enforce unique email for State Officers
            modelBuilder.Entity<StateOfficer>()
                .HasIndex(s => s.Email)
                .IsUnique();


            // Configure decimal types for ScholarshipApplicationViewModel
            modelBuilder.Entity<ScholarshipApplicationViewModel>().Property(a => a.FamilyAnnualIncome).HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<ScholarshipApplicationViewModel>().Property(a => a.AdmissionFee).HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<ScholarshipApplicationViewModel>().Property(a => a.TuitionFee).HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<ScholarshipApplicationViewModel>().Property(a => a.OtherFee).HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<ScholarshipApplicationViewModel>().Property(a => a.PreviousClassPercentage).HasColumnType("decimal(5, 2)");
            modelBuilder.Entity<ScholarshipApplicationViewModel>().Property(a => a.Class10Percentage).HasColumnType("decimal(5, 2)");
            modelBuilder.Entity<ScholarshipApplicationViewModel>().Property(a => a.Class12Percentage).HasColumnType("decimal(5, 2)");
            modelBuilder.Entity<ScholarshipApplicationViewModel>().Property(a => a.DisabilityPercentage).HasColumnType("decimal(5, 2)");
        }
    }
}