﻿using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Entities
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=rootroot;database=inventoryapp");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.ToTable("user");

                entity.Property(e => e.IdUser).HasColumnType("int(11)");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(45);

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}