using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ImitationShop.EFCore.DbModels;

namespace ImitationShop.EFCore.Context
{
    public partial class ImitationShopDBContext : DbContext
    {
        public ImitationShopDBContext()
        {
        }

        public ImitationShopDBContext(DbContextOptions<ImitationShopDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.ItemName).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.MailAddress).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(48);

                entity.Property(e => e.UserName).HasMaxLength(10);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
