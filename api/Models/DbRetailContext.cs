using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace manuel_fajardo_prueba_tecnica.Models;

public partial class DbRetailContext : DbContext
{
  public DbRetailContext()
  {
  }

  public DbRetailContext(DbContextOptions<DbRetailContext> options)
      : base(options)
  {
  }

  public virtual DbSet<Branch>? Branches { get; set; }

  public virtual DbSet<Category>? Categories { get; set; }

  public virtual DbSet<Person>? People { get; set; }

  public virtual DbSet<Question>? Questions { get; set; }

  public virtual DbSet<Survey>? Surveys { get; set; }

  public virtual DbSet<SurveyQuestion>? SurveyQuestions { get; set; }

  public virtual DbSet<SurveyByBranch>? SurveyByBranches { get; set; }
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      => optionsBuilder.UseSqlServer("Server=DESKTOP-JA8M2NU;Database=db_retail;Trusted_Connection=True;TrustServerCertificate=True;");

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<SurveyByBranch>(entity =>
    {
      entity.HasNoKey();
    });

    modelBuilder.Entity<Branch>(entity =>
    {
      entity.HasKey(e => e.IdBranch).HasName("PK__Branch__ECC84CD966937ECD");

      entity.ToTable("Branch");

      entity.Property(e => e.IdBranch).HasColumnName("id_branch");
      entity.Property(e => e.Active)
              .HasDefaultValueSql("((1))")
              .HasColumnName("active");
      entity.Property(e => e.City)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasColumnName("city");
      entity.Property(e => e.Name)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasColumnName("name");
      entity.Property(e => e.Province)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasColumnName("province");
    });

    modelBuilder.Entity<Category>(entity =>
    {
      entity.HasKey(e => e.IdCategory).HasName("PK__Category__E548B6737620799E");

      entity.ToTable("Category");

      entity.Property(e => e.IdCategory).HasColumnName("id_category");
      entity.Property(e => e.Name)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasColumnName("name");
    });

    modelBuilder.Entity<Person>(entity =>
    {
      entity.HasKey(e => e.IdPerson).HasName("PK__Person__E9AB6A41D7E8C140");

      entity.ToTable("Person");

      entity.Property(e => e.IdPerson).HasColumnName("id_person");
      entity.Property(e => e.Active)
              .HasDefaultValueSql("((1))")
              .HasColumnName("active");
      entity.Property(e => e.Age).HasColumnName("age");
      entity.Property(e => e.Identification)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasColumnName("identification");
      entity.Property(e => e.Name)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasColumnName("name");
      entity.Property(e => e.Sex)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasColumnName("sex");
    });

    modelBuilder.Entity<Question>(entity =>
    {
      entity.HasKey(e => e.IdQuestion).HasName("PK__Question__2BD9247783E2B183");

      entity.ToTable("Question");

      entity.Property(e => e.IdQuestion).HasColumnName("id_question");
      entity.Property(e => e.Active)
              .HasDefaultValueSql("((1))")
              .HasColumnName("active");
      entity.Property(e => e.Content)
              .HasMaxLength(200)
              .IsUnicode(false)
              .HasColumnName("content");
      entity.Property(e => e.IdCategory).HasColumnName("id_category");
      entity.Property(e => e.Type).HasColumnName("type");

      entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Questions)
              .HasForeignKey(d => d.IdCategory)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK__Question__id_cat__3A81B327");
    });

    modelBuilder.Entity<Survey>(entity =>
    {
      entity.HasKey(e => e.IdSurvey).HasName("PK__Survey__F6237DE792EAA95B");

      entity.ToTable("Survey");

      entity.Property(e => e.IdSurvey).HasColumnName("id_survey");
      entity.Property(e => e.Active)
              .HasDefaultValueSql("((1))")
              .HasColumnName("active");
      entity.Property(e => e.Date)
              .HasDefaultValueSql("(getdate())")
              .HasColumnType("date")
              .HasColumnName("date");
      entity.Property(e => e.IdBranch).HasColumnName("id_branch");
      entity.Property(e => e.IdPerson).HasColumnName("id_person");
      entity.Property(e => e.Percentage).HasColumnName("percentage");

      entity.HasOne(d => d.IdBranchNavigation).WithMany(p => p.Surveys)
              .HasForeignKey(d => d.IdBranch)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK__Survey__id_branc__45F365D3");

      entity.HasOne(d => d.IdPersonNavigation).WithMany(p => p.Surveys)
              .HasForeignKey(d => d.IdPerson)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK__Survey__id_perso__46E78A0C");
    });

    modelBuilder.Entity<SurveyQuestion>(entity =>
    {
      entity.HasKey(e => new { e.IdSurvey, e.IdQuestion }).HasName("PK__SurveyQu__949EEFA022F5DAC7");

      entity.ToTable("SurveyQuestion");

      entity.Property(e => e.IdSurvey).HasColumnName("id_survey");
      entity.Property(e => e.IdQuestion).HasColumnName("id_question");
      entity.Property(e => e.Answer)
              .HasMaxLength(200)
              .IsUnicode(false)
              .HasColumnName("answer");
      entity.Property(e => e.Percentage).HasColumnName("percentage");

      entity.HasOne(d => d.IdQuestionNavigation).WithMany(p => p.SurveyQuestions)
              .HasForeignKey(d => d.IdQuestion)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK__SurveyQue__id_qu__4AB81AF0");

      entity.HasOne(d => d.IdSurveyNavigation).WithMany(p => p.SurveyQuestions)
              .HasForeignKey(d => d.IdSurvey)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK__SurveyQue__id_su__49C3F6B7");
    });

    OnModelCreatingPartial(modelBuilder);
  }

  partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
