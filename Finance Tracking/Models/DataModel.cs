using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Finance_Tracking.Models
{
    public partial class DataModel : DbContext
    {
        public DataModel()
            : base("name=DataModel")
        {
        }

        public virtual DbSet<FunderModel> Funders { get; set; }
        public virtual DbSet<Funder_EmployeeModel> Funder_EmployeesModels { get; set; }
        public virtual DbSet<StudentModel> Students { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FunderModel>()
                .HasMany(e => e.Funder_EmployeeModel)
                .WithRequired(e => e.FunderModel)
                .HasForeignKey(e => e.Organization_Name)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Funder_EmployeeModel>()
                .Property(e => e.Admin_Code)
                .IsFixedLength();

            modelBuilder.Entity<StudentModel>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<StudentModel>()
                .Property(e => e.Gender)
                .IsUnicode(false);
        }
    }
}
