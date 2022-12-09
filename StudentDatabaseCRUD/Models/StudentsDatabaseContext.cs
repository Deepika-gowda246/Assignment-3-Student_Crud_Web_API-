using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StudentDatabaseCRUD.Models;

public partial class StudentsDatabaseContext : DbContext
{
    

    public StudentsDatabaseContext(DbContextOptions<StudentsDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Student> Students { get; set; }

   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Student__32C52A79F95BF1CA");

            entity.ToTable("Student");

            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Class)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
