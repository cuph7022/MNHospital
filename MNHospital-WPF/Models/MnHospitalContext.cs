using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MNHospital_WPF.Models;

public partial class MnHospitalContext : DbContext
{
    public MnHospitalContext()
    {
    }

    public MnHospitalContext(DbContextOptions<MnHospitalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Bacsi> Bacsis { get; set; }

    public virtual DbSet<Benh> Benhs { get; set; }

    public virtual DbSet<Benhnhan> Benhnhans { get; set; }

    public virtual DbSet<Datlich> Datliches { get; set; }

    public virtual DbSet<Hoso> Hosos { get; set; }

    public virtual DbSet<Ketqua> Ketquas { get; set; }

    public virtual DbSet<Kham> Khams { get; set; }

    public virtual DbSet<Khoakham> Khoakhams { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		var congfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
		if (!optionsBuilder.IsConfigured)
		{
			optionsBuilder.UseSqlServer(congfig.GetConnectionString("DBContext"));
		}
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__account__F3DBC5736936DACB");

            entity.ToTable("account");

            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Userpassword)
                .HasMaxLength(50)
                .HasColumnName("userpassword");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.Role)
                .HasConstraintName("FK__account__role__4BAC3F29");
        });

        modelBuilder.Entity<Bacsi>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__bacsi__3213E83FA7C03349");

            entity.ToTable("bacsi");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.Khoa).HasColumnName("khoa");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.KhoaNavigation).WithMany(p => p.Bacsis)
                .HasForeignKey(d => d.Khoa)
                .HasConstraintName("FK__bacsi__khoa__5629CD9C");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Bacsis)
                .HasForeignKey(d => d.Username)
                .HasConstraintName("FK__bacsi__username__5535A963");
        });

        modelBuilder.Entity<Benh>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__benh__3213E83F4CB36E81");

            entity.ToTable("benh");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Benhnhan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__benhnhan__3213E83F50A5B044");

            entity.ToTable("benhnhan");

            entity.HasIndex(e => e.Cccd, "UQ__benhnhan__37D42BFA615B458A").IsUnique();

            entity.HasIndex(e => e.Baohiem, "UQ__benhnhan__EE7EC37DB7E5D979").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Baohiem)
                .HasMaxLength(20)
                .HasColumnName("baohiem");
            entity.Property(e => e.Cccd)
                .HasMaxLength(12)
                .HasColumnName("cccd");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.Gender)
                .HasMaxLength(6)
                .HasColumnName("gender");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Benhnhans)
                .HasForeignKey(d => d.Username)
                .HasConstraintName("FK__benhnhan__userna__5070F446");
        });

        modelBuilder.Entity<Datlich>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__datlich__3213E83F");

            entity.ToTable("datlich");

			entity.Property(e => e.Id)
				.ValueGeneratedOnAdd()
				.HasColumnName("Id");

            entity.Property(e => e.Ngaykham).HasColumnName("ngaykham");
            entity.Property(e => e.Giokham).HasColumnName("giokham");
            entity.Property(e => e.Idbenhnhan).HasColumnName("idbenhnhan");
            entity.Property(e => e.Idkham).HasColumnName("idkham");
			entity.Property(e => e.Status)
	      .HasColumnName("status")
	      .HasDefaultValue(1); 

			entity.HasOne(d => d.IdbenhnhanNavigation).WithMany(p => p.Datliches)
                .HasForeignKey(d => d.Idbenhnhan)
                .HasConstraintName("FK__datlich__idbenhn__656C112C");

            entity.HasOne(d => d.IdkhamNavigation).WithMany(p => p.Datliches)
                .HasForeignKey(d => d.Idkham)
                .HasConstraintName("FK__datlich__idkham__66603565");
        });

        modelBuilder.Entity<Hoso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__hoso__3213E83F5EC7ABBE");

            entity.ToTable("hoso");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Idbenhnhan).HasColumnName("idbenhnhan");
            entity.Property(e => e.Idketqua).HasColumnName("idketqua");

            entity.HasOne(d => d.IdbenhnhanNavigation).WithMany(p => p.Hosos)
                .HasForeignKey(d => d.Idbenhnhan)
                .HasConstraintName("FK__hoso__idbenhnhan__619B8048");

            entity.HasOne(d => d.IdketquaNavigation).WithMany(p => p.Hosos)
                .HasForeignKey(d => d.Idketqua)
                .HasConstraintName("FK__hoso__idketqua__628FA481");
        });

        modelBuilder.Entity<Ketqua>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ketqua__3213E83F8D98B431");

            entity.ToTable("ketqua");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Chuandoan)
                .HasMaxLength(255)
                .HasColumnName("chuandoan");
            entity.Property(e => e.Idbacsi).HasColumnName("idbacsi");
            entity.Property(e => e.Idbenh).HasColumnName("idbenh");
            entity.Property(e => e.Idbenhnhan).HasColumnName("idbenhnhan");
            entity.Property(e => e.Ketqua1)
                .HasMaxLength(255)
                .HasColumnName("ketqua");
            entity.Property(e => e.Ngaykham).HasColumnName("ngaykham");

            entity.HasOne(d => d.IdbacsiNavigation).WithMany(p => p.Ketquas)
                .HasForeignKey(d => d.Idbacsi)
                .HasConstraintName("FK__ketqua__idbacsi__5EBF139D");

            entity.HasOne(d => d.IdbenhNavigation).WithMany(p => p.Ketquas)
                .HasForeignKey(d => d.Idbenh)
                .HasConstraintName("FK__ketqua__idbenh__5CD6CB2B");

            entity.HasOne(d => d.IdbenhnhanNavigation).WithMany(p => p.Ketquas)
                .HasForeignKey(d => d.Idbenhnhan)
                .HasConstraintName("FK__ketqua__idbenhnh__5DCAEF64");
        });

        modelBuilder.Entity<Kham>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__kham__3213E83FA25FFFE5");

            entity.ToTable("kham");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Time).HasColumnName("time");
        });

        modelBuilder.Entity<Khoakham>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__khoakham__3213E83F82231CE3");

            entity.ToTable("khoakham");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__role__3213E83FB4BB516F");

            entity.ToTable("role");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
