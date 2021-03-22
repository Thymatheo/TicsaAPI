using Microsoft.EntityFrameworkCore;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TicsaAPI.DAL.Models
{
    public partial class TicsaContext : DbContext
    {
        public TicsaContext()
        {
        }

        public TicsaContext(DbContextOptions<TicsaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Commentary> Commentary { get; set; }
        public virtual DbSet<Gamme> Gamme { get; set; }
        public virtual DbSet<GammeType> GammeType { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderContent> OrderContent { get; set; }
        public virtual DbSet<Producer> Producer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.CompagnieName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Commentary>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Commentar__IdCli__3A81B327");
            });

            modelBuilder.Entity<Gamme>(entity =>
            {
                entity.Property(e => e.CostHisto).HasColumnType("text");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StockHisto).HasColumnType("text");

                entity.HasOne(d => d.IdProducerNavigation)
                    .WithMany(p => p.Gamme)
                    .HasForeignKey(d => d.IdProducer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Gamme__IdProduce__300424B4");

                entity.HasOne(d => d.IdTypeNavigation)
                    .WithMany(p => p.Gamme)
                    .HasForeignKey(d => d.IdType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Gamme__IdType__2F10007B");
            });

            modelBuilder.Entity<GammeType>(entity =>
            {
                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order__IdClient__34C8D9D1");
            });

            modelBuilder.Entity<OrderContent>(entity =>
            {
                entity.HasOne(d => d.IdGammeNavigation)
                    .WithMany(p => p.OrderContent)
                    .HasForeignKey(d => d.IdGamme)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderCont__IdGam__38996AB5");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.OrderContent)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderCont__IdOrd__37A5467C");
            });

            modelBuilder.Entity<Producer>(entity =>
            {
                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CompagnieName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber).HasColumnName("phoneNumber");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
