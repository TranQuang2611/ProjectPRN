using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WheyStoreVN.Models
{
    public partial class WheystoreContext : DbContext
    {
        public WheystoreContext()
        {
        }

        public WheystoreContext(DbContextOptions<WheystoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<FavoriteProduct> FavoriteProducts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductDetail> ProductDetails { get; set; }
        public virtual DbSet<ProductSavor> ProductSavors { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Savor> Savors { get; set; }
        public virtual DbSet<Shipper> Shippers { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=Wheystore;user=sa;password=12345678");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.AccountId).HasColumnName("accountID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.EmailVerify)
                    .HasColumnName("emailVerify")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Role).HasColumnName("role");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.RoleNavigation)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.Role)
                    .HasConstraintName("FK__Account__role__276EDEB3");
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.Property(e => e.AdminId)
                    .ValueGeneratedNever()
                    .HasColumnName("adminID");

                entity.Property(e => e.ImageUrl).HasColumnName("imageURL");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.HasOne(d => d.AdminNavigation)
                    .WithOne(p => p.Admin)
                    .HasForeignKey<Admin>(d => d.AdminId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Admin__adminID__2E1BDC42");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("Brand");

                entity.Property(e => e.BrandId).HasColumnName("brandID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CatId)
                    .HasName("PK__Category__17B6DD26F386F357");

                entity.ToTable("Category");

                entity.Property(e => e.CatId).HasColumnName("catID");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.Property(e => e.CountryId).HasColumnName("countryID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CustomerId)
                    .ValueGeneratedNever()
                    .HasColumnName("customerID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address");

                entity.Property(e => e.CashInWallet).HasColumnName("cashInWallet");

                entity.Property(e => e.ImageUrl).HasColumnName("imageURL");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.HasOne(d => d.CustomerNavigation)
                    .WithOne(p => p.Customer)
                    .HasForeignKey<Customer>(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Customer__custom__2B3F6F97");
            });

            modelBuilder.Entity<FavoriteProduct>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.DetailId })
                    .HasName("PK__Favorite__3E21BC1EE1A1CFA9");

                entity.ToTable("FavoriteProduct");

                entity.Property(e => e.CustomerId).HasColumnName("customerID");

                entity.Property(e => e.DetailId).HasColumnName("detailID");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.FavoriteProducts)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FavoriteP__custo__52593CB8");

                entity.HasOne(d => d.Detail)
                    .WithMany(p => p.FavoriteProducts)
                    .HasForeignKey(d => d.DetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FavoriteP__detai__534D60F1");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId).HasColumnName("orderID");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.CustomerId).HasColumnName("customerID");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Location).HasColumnName("location");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("orderDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ShipperId).HasColumnName("shipperID");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Order__customerI__59063A47");

                entity.HasOne(d => d.Shipper)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ShipperId)
                    .HasConstraintName("FK__Order__shipperID__5AEE82B9");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.DetailId, e.SavorId })
                    .HasName("PK__OrderDet__55C04CA43E216147");

                entity.ToTable("OrderDetail");

                entity.Property(e => e.OrderId).HasColumnName("orderID");

                entity.Property(e => e.DetailId).HasColumnName("detailID");

                entity.Property(e => e.SavorId).HasColumnName("savorID");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Detail)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.DetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderDeta__detai__5FB337D6");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderDeta__order__5EBF139D");

                entity.HasOne(d => d.Savor)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.SavorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderDeta__savor__60A75C0F");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductId).HasColumnName("productID");

                entity.Property(e => e.BrandId).HasColumnName("brandID");

                entity.Property(e => e.CountryId).HasColumnName("countryID");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Img)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("img");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SubCatId).HasColumnName("subCatID");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK__Product__brandID__44FF419A");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__Product__country__440B1D61");

                entity.HasOne(d => d.SubCat)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SubCatId)
                    .HasConstraintName("FK__Product__subCatI__4316F928");
            });

            modelBuilder.Entity<ProductDetail>(entity =>
            {
                entity.HasKey(e => e.DetailId)
                    .HasName("PK__ProductD__830778398B88D091");

                entity.ToTable("ProductDetail");

                entity.Property(e => e.DetailId).HasColumnName("detailID");

                entity.Property(e => e.DefaultPrice).HasColumnName("defaultPrice");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Img)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("img");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.ProductId).HasColumnName("productID");

                entity.Property(e => e.SellPrice).HasColumnName("sellPrice");

                entity.Property(e => e.Serving)
                    .HasMaxLength(255)
                    .HasColumnName("serving");

                entity.Property(e => e.ServingSize)
                    .HasMaxLength(255)
                    .HasColumnName("servingSize");

                entity.Property(e => e.SizeId).HasColumnName("sizeID");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__ProductDe__produ__48CFD27E");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.ProductDetails)
                    .HasForeignKey(d => d.SizeId)
                    .HasConstraintName("FK__ProductDe__sizeI__49C3F6B7");
            });

            modelBuilder.Entity<ProductSavor>(entity =>
            {
                entity.HasKey(e => new { e.DetailId, e.SavorId })
                    .HasName("PK__ProductS__DC97FD94227BBC24");

                entity.ToTable("ProductSavor");

                entity.Property(e => e.DetailId).HasColumnName("detailID");

                entity.Property(e => e.SavorId).HasColumnName("savorID");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Detail)
                    .WithMany(p => p.ProductSavors)
                    .HasForeignKey(d => d.DetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductSa__detai__4D94879B");

                entity.HasOne(d => d.Savor)
                    .WithMany(p => p.ProductSavors)
                    .HasForeignKey(d => d.SavorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductSa__savor__4E88ABD4");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("roleID");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("roleName");
            });

            modelBuilder.Entity<Savor>(entity =>
            {
                entity.ToTable("Savor");

                entity.Property(e => e.SavorId).HasColumnName("savorID");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Shipper>(entity =>
            {
                entity.ToTable("Shipper");

                entity.Property(e => e.ShipperId).HasColumnName("shipperID");

                entity.Property(e => e.Company)
                    .HasMaxLength(255)
                    .HasColumnName("company");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone");
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.ToTable("Size");

                entity.Property(e => e.SizeId).HasColumnName("sizeID");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.HasKey(e => e.SubCatId)
                    .HasName("PK__SubCateg__FB8BF6D0989706C6");

                entity.ToTable("SubCategory");

                entity.Property(e => e.SubCatId).HasColumnName("subCatID");

                entity.Property(e => e.CatId).HasColumnName("catID");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.SubCategories)
                    .HasForeignKey(d => d.CatId)
                    .HasConstraintName("FK__SubCatego__catID__398D8EEE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
