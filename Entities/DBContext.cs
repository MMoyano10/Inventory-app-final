using Microsoft.EntityFrameworkCore;

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
        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }
        public virtual DbSet<TypeProduct> TypeProducts { get; set; }
        public virtual DbSet<TransactionProduct> TransactionProducts { get; set; }

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

            modelBuilder.Entity<Actor>(entity =>
            {
                entity.HasKey(e => e.IdActor);

                entity.ToTable("actor");

                entity.Property(e => e.IdActor).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Phono)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.DocumentId)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.IdActor)
                    .IsRequired()
                    .HasMaxLength(45);

            });


            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.IdProduct);

                entity.ToTable("product");

                entity.Property(e => e.IdProduct).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CostProduct)
                    .IsRequired();

                entity.Property(e => e.CostSell)
                    .IsRequired();

                entity.Property(e => e.Unit)
                    .IsRequired();
                
                entity.Property(e => e.IdTypeProduct)
                    .IsRequired();
            });

            modelBuilder.Entity<Transactions>(entity =>
            {
                entity.HasKey(e => e.IdTransactions);

                entity.ToTable("transactions");

                entity.Property(e => e.IdTransactions).HasColumnType("int(11)");

                entity.Property(e => e.TypeTransaction)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Quantity)
                    .IsRequired();

                entity.Property(e => e.Value)
                    .IsRequired();

                entity.Property(e => e.Date_Transaction)
                    .IsRequired();
                
                entity.HasMany(b => b.Products)
                .WithOne();
            });

            modelBuilder.Entity<TransactionProduct>(entity =>
            {
                entity.HasKey(e => e.IdTransactionProduct);

                entity.ToTable("transactionproduct");

                entity.Property(e => e.IdTransactionProduct).HasColumnType("int(11)");

                entity.Property(e => e.IdProduct)
                    .IsRequired();

                entity.Property(e => e.IdTransactions)
                    .IsRequired();
            });
            modelBuilder.Entity<TypeProduct>(entity =>
            {
                entity.HasKey(e => e.IdTypeProduct);

                entity.ToTable("typeproduct");

                entity.Property(e => e.IdTypeProduct).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45);

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
