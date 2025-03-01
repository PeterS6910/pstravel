using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ImportXml.AfiTravelModel
{
    public class OfferDbContex: DbContext
    {
        public DbSet<Offer> Offer { get; set; }
        public DbSet<Offers> Offers { get; set; }
        public DbSet<Photo> Photo { get; set; }
        public DbSet<Destination> Destination { get; set; }
        public DbSet<Term> Term { get; set; }
        public DbSet<TourType> TourType { get; set; }
        public DbSet<HotelInfo> HotelInfo { get; set; }
        public DbSet<Coords> Coords { get; set; }
        public DbSet<Airports> Airports { get; set; }

        private readonly string _connectionString;
        public OfferDbContex()
        {
            // Nastavíme pripojenie k MySQL
            _connectionString = "Server=localhost;Database=afitravel;User=root;Password=root;Port=3306;";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString, new MySqlServerVersion(new Version(8, 0, 2)));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Offers>(entity =>
            {
                entity.HasKey(o => o.Id); // Primárny kľúč

                entity.Property(o => o.Timestamp)
                    .IsRequired(); // Povinný údaj

                entity.Property(o => o.Count)
                    .IsRequired(); // Povinný údaj

                // Konfigurácia vzťahu One-to-Many (jeden Offers -> viac Offer)
                entity.HasMany(o => o.Offer)
                    .WithOne()  // Žiadna navigácia v triede Offer
                    .HasForeignKey("OffersId")  // Cudzie kľúčové pole v Offer
                    .OnDelete(DeleteBehavior.Cascade); // Ak sa odstráni Offers, odstránia sa aj OfferItems
            });
            modelBuilder.Entity<Offer>()
                .HasMany(o => o.Photos)
                .WithOne()
                .HasForeignKey(p => p.OfferId);

            modelBuilder.Entity<Offer>()
                .HasOne(o => o.Destination)
                .WithOne()
                .HasForeignKey<Destination>(d => d.OfferId);

            modelBuilder.Entity<Offer>()
                .HasOne(o => o.Term)
                .WithOne()
                .HasForeignKey<Term>(t => t.OfferId);

            modelBuilder.Entity<Offer>()
                .HasMany(o => o.TourType)
                .WithOne()
                .HasForeignKey(t => t.OfferId);

            modelBuilder.Entity<Offer>()
                .HasOne(o => o.HotelInfo)
                .WithOne()
                .HasForeignKey<HotelInfo>(h => h.OfferId);

            modelBuilder.Entity<HotelInfo>()
                .HasOne(o => o.Coords)
                .WithOne()
                .HasForeignKey<Coords>(t => t.HotelInfoId);

            modelBuilder.Entity<Offer>()
               .HasMany(o => o.Actionattributes)
               .WithOne()
               .HasForeignKey(p => p.OfferId);

            modelBuilder.Entity<Offer>()
               .HasMany(o => o.Airports)
               .WithOne()
               .HasForeignKey(t => t.OfferId);
            
        }
    }
}
