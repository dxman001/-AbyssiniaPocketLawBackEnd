using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PocketLawDB.Models;

namespace PocketLawDB.Data
{
    public partial class PocketLawDBContext : DbContext
    {
        public PocketLawDBContext()
        {
        }

        public PocketLawDBContext(DbContextOptions<PocketLawDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cassation> Cassations { get; set; } = null!;
        public virtual DbSet<Law> Laws { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Cassation>(entity =>
            {
                entity.ToTable("cassation");

                entity.HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Category).HasMaxLength(50);

                entity.Property(e => e.Decision).HasMaxLength(100);

                entity.Property(e => e.Download).HasMaxLength(500);

                entity.Property(e => e.Given).HasMaxLength(417);

                entity.Property(e => e.Keywords).HasMaxLength(500);

                entity.Property(e => e.Title).HasMaxLength(250);

                entity.Property(e => e.Volume).HasMaxLength(50);
            });

            modelBuilder.Entity<Law>(entity =>
            {
                entity.ToTable("laws");

                entity.HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Category).HasMaxLength(34);

                entity.Property(e => e.Download).HasMaxLength(500);

                entity.Property(e => e.Entry)
                    .HasMaxLength(29)
                    .HasColumnName("entry");

                entity.Property(e => e.Jurisdiction).HasMaxLength(12);

                entity.Property(e => e.Keywords).HasMaxLength(474);

                entity.Property(e => e.Status).HasMaxLength(850);

                entity.Property(e => e.Title)
                    .HasMaxLength(290)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FullName)
                    .HasMaxLength(30)
                    .HasColumnName("Full_Name");

                entity.Property(e => e.PassWord)
                    .HasMaxLength(30)
                    .HasColumnName("pass_word");

                entity.Property(e => e.Role).HasMaxLength(30);

                entity.Property(e => e.UserName)
                    .HasMaxLength(30)
                    .HasColumnName("user_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
