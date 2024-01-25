using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace PersonnelInformationWebAPI.Models
{
    public partial class AvaNyadeEmployee : DbContext
    {
        public AvaNyadeEmployee()
            : base("name=AvaNyadeEmployee")
        {
        }

        public virtual DbSet<BelongTo> BelongTo { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departments>()
                .Property(e => e.DepartmentName)
                .IsUnicode(false);

            modelBuilder.Entity<Employees>()
                .Property(e => e.EmployeeName)
                .IsUnicode(false);

            modelBuilder.Entity<Employees>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Employees>()
                .Property(e => e.BloodType)
                .IsUnicode(false);

            modelBuilder.Entity<Employees>()
                .Property(e => e.PassWords)
                .IsUnicode(false);
        }
    }
}
