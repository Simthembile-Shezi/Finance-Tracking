//using System;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity;
//using System.Linq;

//namespace Finance_Tracking.Models
//{
//    public partial class DataContext : DbContext
//    {
//        public DataContext()
//            : base("name=DataContext")
//        {
//        }

//        public virtual DbSet<Academic_Record> Academic_Records { get; set; }
//        public virtual DbSet<Application> Applications { get; set; }
//        public virtual DbSet<Bursar_Fund> Bursar_Funds { get; set; }
//        public virtual DbSet<Bursary> Bursaries { get; set; }
//        public virtual DbSet<Enrolled_At> Enrolled_Ats { get; set; }
//        public virtual DbSet<Finacial_Record> Finacial_Records { get; set; }
//        public virtual DbSet<Funder> Funders { get; set; }
//        public virtual DbSet<Funder_Employee> Funder_Employees { get; set; }
//        public virtual DbSet<Institution> Institutions { get; set; }
//        public virtual DbSet<Institution_Employee> Institution_Employees { get; set; }
//        public virtual DbSet<Student> Students { get; set; }

//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<Academic_Record>()
//                .Property(e => e.Avarage_Marks)
//                .HasPrecision(18, 0);

//            modelBuilder.Entity<Application>()
//                .HasOptional(e => e.Bursar_Funds)
//                .WithRequired(e => e.Application);

//            modelBuilder.Entity<Bursary>()
//                .Property(e => e.Bursary_Amount)
//                .HasPrecision(19, 4);

//            modelBuilder.Entity<Bursary>()
//                .Property(e => e.Number_Available)
//                .HasPrecision(18, 0);

//            modelBuilder.Entity<Bursary>()
//                .HasMany(e => e.Applications)
//                .WithRequired(e => e.Bursary)
//                .WillCascadeOnDelete(false);

//            modelBuilder.Entity<Enrolled_At>()
//                .HasMany(e => e.Academic_Records)
//                .WithRequired(e => e.Enrolled_At)
//                .WillCascadeOnDelete(false);

//            modelBuilder.Entity<Enrolled_At>()
//                .HasMany(e => e.Finacial_Records)
//                .WithRequired(e => e.Enrolled_At)
//                .WillCascadeOnDelete(false);

//            modelBuilder.Entity<Finacial_Record>()
//                .Property(e => e.Balance_Amount)
//                .HasPrecision(19, 4);

//            modelBuilder.Entity<Funder>()
//                .HasMany(e => e.Bursaries)
//                .WithRequired(e => e.Funder)
//                .WillCascadeOnDelete(false);

//            modelBuilder.Entity<Funder>()
//                .HasMany(e => e.Funder_Employee)
//                .WithRequired(e => e.Funder)
//                .HasForeignKey(e => e.Organization_Name)
//                .WillCascadeOnDelete(false);

//            modelBuilder.Entity<Funder_Employee>()
//                .Property(e => e.Admin_Code)
//                .IsFixedLength();

//            modelBuilder.Entity<Institution>()
//                .HasMany(e => e.Enrolled_At)
//                .WithRequired(e => e.Institution)
//                .WillCascadeOnDelete(false);

//            modelBuilder.Entity<Institution>()
//                .HasMany(e => e.Institution_Employee)
//                .WithRequired(e => e.Institution)
//                .HasForeignKey(e => e.Organization_Name)
//                .WillCascadeOnDelete(false);

//            modelBuilder.Entity<Institution_Employee>()
//                .Property(e => e.Admin_Code)
//                .IsFixedLength();

//            modelBuilder.Entity<Student>()
//                .Property(e => e.Title)
//                .IsUnicode(false);

//            modelBuilder.Entity<Student>()
//                .Property(e => e.Gender)
//                .IsUnicode(false);

//            modelBuilder.Entity<Student>()
//                .HasMany(e => e.Applications)
//                .WithRequired(e => e.Student)
//                .WillCascadeOnDelete(false);

//            modelBuilder.Entity<Student>()
//                .HasMany(e => e.Enrolled_At)
//                .WithRequired(e => e.Student)
//                .WillCascadeOnDelete(false);
//        }
//    }
//}
