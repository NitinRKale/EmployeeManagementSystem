using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystemUI.Models;

public partial class EmployeeMgmtSystemContext : DbContext
{
    public EmployeeMgmtSystemContext()
    {
    }

    public EmployeeMgmtSystemContext(DbContextOptions<EmployeeMgmtSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DepartmentMaster2> DepartmentMasters { get; set; }

    public virtual DbSet<DesignationMaster2> DesignationMasters { get; set; }

    public virtual DbSet<EmployeeMaster> EmployeeMasters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        ////#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        /// => optionsBuilder.UseSqlServer("Server=LAPTOP-P3HI8HAD;Database=EmployeeMgmtSystem;Trusted_Connection=True;TrustServerCertificate=True;");
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<DepartmentMaster2>(entity =>
        //{
        //    entity.HasKey(e => e.DepartmentId);
        //});

        //modelBuilder.Entity<DesignationMaster2>(entity =>
        //{
        //    entity.HasKey(e => e.DesignationId);
        //});

        //modelBuilder.Entity<EmployeeMaster>(entity =>
        //{
        //    entity.HasKey(e => e.EmpId);

        //    entity.HasIndex(e => e.DepartmentId, "IX_EmployeeMasters_DepartmentId");

        //    entity.HasIndex(e => e.DesignationId, "IX_EmployeeMasters_DesignationId");

        //    entity.Property(e => e.EmpFirstName).HasMaxLength(20);
        //    entity.Property(e => e.EmpGender)
        //        .HasMaxLength(15)
        //        .HasDefaultValue("");
        //    entity.Property(e => e.EmpLastName).HasMaxLength(20);
        //    entity.Property(e => e.EmpMiddleName).HasMaxLength(20);
        //    entity.Property(e => e.PhoneNumber).HasMaxLength(13);
        //    entity.Property(e => e.Salary).HasColumnType("decimal(18, 2)");

        //    entity.HasOne(d => d.Department2).WithMany(p => p.EmployeeMasters).HasForeignKey(d => d.DepartmentId);

        //    entity.HasOne(d => d.Designation).WithMany(p => p.EmployeeMasters).HasForeignKey(d => d.DesignationId);
        //});

        //OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
