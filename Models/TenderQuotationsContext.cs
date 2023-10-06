using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tenders_Quotations.Models;

public partial class TenderQuotationsContext : DbContext
{
    public TenderQuotationsContext()
    {
    }

    public TenderQuotationsContext(DbContextOptions<TenderQuotationsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ad> Ads { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Quotation> Quotations { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tender> Tenders { get; set; }

    public virtual DbSet<TendersTaken> TendersTakens { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Currentuser> Currentusers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ad>(entity =>
        {
            entity.HasKey(e => e.AdId).HasName("PK__Ads__7130D5AEEBCB0ECA");

            entity.Property(e => e.AdPoster)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.AdTitle)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0BFEFEB42F");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(40)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Quotation>(entity =>
        {
            entity.HasKey(e => e.QuotationId).HasName("PK__Quotatio__E19752933BED1FE9");

            entity.Property(e => e.Authority)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.CompanyName)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.EstablishedDate)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProjectEndDate).HasColumnType("datetime");
            entity.Property(e => e.ProjectStartDate).HasColumnType("datetime");
            entity.Property(e => e.Proprieator)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.QuotedDate).HasColumnType("datetime");
            entity.Property(e => e.TenderName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Tender).WithMany(p => p.Quotations)
                .HasForeignKey(d => d.TenderId)
                .HasConstraintName("FK__Quotation__Tende__4CA06362");

            entity.HasOne(d => d.User).WithMany(p => p.Quotations)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Quotation__UserI__4BAC3F29");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A16857D94");

            entity.Property(e => e.RoleName)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Tender>(entity =>
        {
            entity.HasKey(e => e.TenderId).HasName("PK__Tenders__B21B4268B5565438");

            entity.Property(e => e.Authority)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.IsTaken).HasDefaultValueSql("((0))");
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProjectEndDate).HasColumnType("datetime");
            entity.Property(e => e.ProjectStartDate).HasColumnType("datetime");
            entity.Property(e => e.Referencenumber)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.TenderClosingDate).HasColumnType("datetime");
            entity.Property(e => e.TenderName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TenderOpeningDate).HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.Tenders)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Tenders__Categor__403A8C7D");
        });

        modelBuilder.Entity<TendersTaken>(entity =>
        {
            entity.HasKey(e => e.TakenId).HasName("PK__TendersT__D3CA0B74407E1241");

            entity.ToTable("TendersTaken", tb => tb.HasTrigger("TenderGiven"));

            entity.Property(e => e.Authority)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.CompanyName)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProjectEndDate).HasColumnType("datetime");
            entity.Property(e => e.ProjectStartDate).HasColumnType("datetime");
            entity.Property(e => e.Proprieator)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.TenderName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Quotation).WithMany(p => p.TendersTakens)
                .HasForeignKey(d => d.QuotationId)
                .HasConstraintName("FK__TendersTa__Quota__4F7CD00D");

            entity.HasOne(d => d.Tender).WithMany(p => p.TendersTakens)
                .HasForeignKey(d => d.TenderId)
                .HasConstraintName("FK__TendersTa__Tende__01142BA1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CA18C83D0");

            entity.ToTable(tb => tb.HasTrigger("EncryptPassword"));

            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CompanyName)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.CompanySector)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Crn).HasColumnName("CRN");
            entity.Property(e => e.Email)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.EstablishedDate)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Gstin)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("GSTIN");
            entity.Property(e => e.LastLoginDate).HasColumnType("datetime");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProfilePic).HasMaxLength(200);
            entity.Property(e => e.Proprieator)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Token).HasColumnName("token");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleId__3D5E1FD2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
