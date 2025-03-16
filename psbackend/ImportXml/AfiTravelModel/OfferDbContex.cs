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
        public DbSet<JobState> JobState { get; set; }
        public DbSet<JobCode> JobCode { get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<Cestovka> Cestovka { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Locality> Locality { get; set; }
        public DbSet<Hotel> Hotel { get; set; }
        public DbSet<HotelDetails> HotelDetails { get; set; }
        public DbSet<Offer> Offer { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<Food> Food { get; set; }
        public DbSet<TourTypes> TourTypes { get; set; }
        public DbSet<Transport> Transport { get; set; }
        public DbSet<Images> Images { get; set; }

        public DbSet<OfferTourTypes> OfferTourTypes { get; set; }

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
            modelBuilder.Entity<Job>(entity =>
            {
                entity.HasKey(j => j.Id); // Primárny kľúč

                entity.Property(j => j.Name)
                    .IsRequired()
                    .HasMaxLength(30); // Povinné a max 30 znakov

                entity.Property(j => j.JobCodeId)
                    .IsRequired(); // Povinné pole

                entity.Property(j => j.JobStateId)
                    .IsRequired(); // Povinné pole

                entity.Property(j => j.ScheduledTime)
                    .IsRequired();

                entity.Property(j => j.InputParameters)
                    .IsRequired(false); // Môže byť null

                entity.Property(j => j.Description)
                    .HasMaxLength(50); // Max 50 znakov

                entity.Property(j => j.CreatedAt)
                    .IsRequired();

                entity.Property(j => j.UpdatedAt)
                    .IsRequired(false); // Môže byť null

                entity.Property(j => j.DeletedAt)
                    .IsRequired(false); // Môže byť null

                entity.HasOne<JobCode>()
                    .WithMany()
                    .HasForeignKey(j => j.JobCodeId)
                    .OnDelete(DeleteBehavior.Restrict); // Nechceme, aby sa JobCode mazal s Job

                // 1:1 vzťah medzi Job a JobState
                entity.HasOne<JobState>()
                    .WithMany()
                    .HasForeignKey(j => j.JobStateId)
                    .OnDelete(DeleteBehavior.Restrict); // Nechceme, aby sa JobState mazal s Job
            });

            modelBuilder.Entity<JobCode>(entity =>
            {
                entity.HasKey(jc => jc.Id); // Nastavenie primárneho kľúča

                entity.Property(jc => jc.Id)
                    .IsRequired();

                entity.Property(jc => jc.Code)
                    .IsRequired()
                    .HasMaxLength(5); // Maximálna dĺžka 5 znakov

                entity.Property(jc => jc.Name)
                    .IsRequired()
                    .HasMaxLength(30); // Maximálna dĺžka 30 znakov

                entity.Property(jc => jc.Description)
                    .HasMaxLength(50); // Maximálna dĺžka 50 znakov (nepovinné)

                entity.Property(jc => jc.CreatedAt)
                    .IsRequired();

                entity.Property(jc => jc.UpdatedAt)
                    .IsRequired(false); // Nullable - môže byť null, ak nebol aktualizovaný

                entity.Property(jc => jc.DeletedAt)
                    .IsRequired(false); // Nullable - ak je null, záznam nie je zmazaný
            });

            modelBuilder.Entity<JobState>(entity =>
            {
                entity.HasKey(js => js.Id); // Nastavenie primárneho kľúča

                entity.Property(js => js.Id)
                    .IsRequired();

                entity.Property(js => js.Name)
                    .IsRequired()
                    .HasMaxLength(30); // Maximálna dĺžka 30 znakov

                entity.Property(js => js.Code)
                    .IsRequired()
                    .HasMaxLength(30); // Maximálna dĺžka 30 znakov

                entity.Property(js => js.CreatedAt)
                    .IsRequired();

                entity.Property(js => js.UpdatedAt)
                    .IsRequired(false); // Nullable - môže byť null, ak nebol aktualizovaný

                entity.Property(js => js.DeletedAt)
                    .IsRequired(false); // Nullable - ak je null, záznam nie je zmazaný
            });

            modelBuilder.Entity<Cestovka>(entity =>
            {
                // Nastavenie primárneho kľúča
                entity.HasKey(c => c.Id);

                // Definovanie generovania GUID automaticky
                entity.Property(c => c.Id)
                    .IsRequired();

                // Nastavenie názvu cestovky (max. 80 znakov)
                entity.Property(c => c.Nazov)
                    .IsRequired()
                    .HasMaxLength(80);

                // Nastavenie IČO (max. 10 znakov)
                entity.Property(c => c.ICO)
                    .IsRequired()
                    .HasMaxLength(10);

                // Nastavenie voliteľného kontaktu (max. 50 znakov)
                entity.Property(c => c.Kontakt)
                    .HasMaxLength(50);

                // Nastavenie voliteľného emailu (max. 50 znakov)
                entity.Property(c => c.Email)
                    .HasMaxLength(50);

                // Dátumy vytvorenia, aktualizácie a zmazania
                entity.Property(c => c.CreatedAt)
                    .IsRequired();

                entity.Property(c => c.UpdatedAt)
                    .IsRequired(false);

                entity.Property(c => c.DeletedAt)
                    .IsRequired(false);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(c => c.Id); // Nastavenie primárneho kľúča

                entity.Property(c => c.Id)
                    .IsRequired();

                entity.Property(c => c.CountryName)
                    .IsRequired()
                    .HasMaxLength(50); // Maximálna dĺžka 50 znakov pre názov v slovenčine

                entity.Property(c => c.IsNajziadanejsia)
                    .IsRequired(false); // Nullable - môže byť null, ak nie je špecifikované

                entity.Property(c => c.CreatedAt)
                    .IsRequired();

                entity.Property(c => c.UpdatedAt)
                    .IsRequired(false); // Nullable - môže byť null, ak nebol aktualizovaný

                entity.Property(c => c.DeletedAt)
                    .IsRequired(false); // Nullable - ak je null, záznam nie je zmazaný
            });

            modelBuilder.Entity<Locality>(entity =>
            {
                // Nastavenie primárneho kľúča
                entity.HasKey(l => l.Id);

                // Definovanie generovania GUID automaticky
                entity.Property(l => l.Id)
                    .IsRequired();

                entity.HasOne<Country>()    // Lokality sa viaže na jednu krajinu
                    .WithMany()              // Zatiaľ nepoužívame navigačný property na strane `Country`
                    .HasForeignKey(l => l.CountryId) // Cudzie kľúčové pole `CountryId`
                    .OnDelete(DeleteBehavior.Restrict); // Predídeme odstráneniu Country, ak existuje Locality

                entity.HasOne<Locality>()  // Lokality sa môžu viazať na nadriadenú lokalitu
                    .WithMany()  // Pre tento príklad sa nenastavuje navigačný property v ParentLocality
                    .HasForeignKey(l => l.ParentLocalityId) // Cudzie kľúčové pole `ParentLocalityId`
                    .OnDelete(DeleteBehavior.SetNull); // Ak je nadriadená lokalita zmazaná, ParentLocalityId sa nastaví na null

                // Definovanie vlastností s dĺžkou
                entity.Property(l => l.LocalityName)
                    .IsRequired()
                    .HasMaxLength(80);

                // Dátumy vytvorenia, aktualizácie a zmazania
                entity.Property(l => l.CreatedAt)
                    .IsRequired();

                entity.Property(l => l.UpdatedAt)
                    .IsRequired(false);

                entity.Property(l => l.DeletedAt)
                    .IsRequired(false);
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                // Nastavenie primárneho kľúča
                entity.HasKey(h => h.Id);

                // Definovanie generovania GUID automaticky
                entity.Property(h => h.Id)
                    .IsRequired();

                // Vzťah s `Country` (1:N) - Jeden Hotel môže patriť len do jednej Krajiny
                entity.HasOne<Country>() // Hotel sa viaže na jednu Krajinu
                    .WithMany() // Krajina môže mať viacero hotelov (Nie je nastavená navigácia v `Country`)
                    .HasForeignKey(h => h.CountryId) // Cudzie kľúčové pole `CountryId`
                    .OnDelete(DeleteBehavior.Restrict); // Ak sa zmaže krajina, hotel zostane

                // Vzťah s `Locality` (1:N) - Jeden Hotel môže patriť k jednej Locality
                entity.HasOne<Locality>() // Hotel sa viaže na jednu Locality
                    .WithMany() // Locality môže mať viacero hotelov (Nie je nastavená navigácia v `Locality`)
                    .HasForeignKey(h => h.LocalityId) // Cudzie kľúčové pole `LocalityId`
                    .OnDelete(DeleteBehavior.SetNull); // Ak sa zmaže Locality, hodnota `LocalityId` sa nastaví na null

                // Nastavenie vlastnosti `Name` - povinná a max. dĺžka 120 znakov
                entity.Property(h => h.Name)
                    .IsRequired()
                    .HasMaxLength(120);

                // Nastavenie vlastností pre GPS koordináty
                entity.Property(h => h.Latitude)
                    .HasColumnType("float");

                entity.Property(h => h.Longitude)
                    .HasColumnType("float");

                // Nastavenie hodnoty `Stars` (počet hviezdičiek) - povinná vlastnosť
                entity.Property(h => h.Stars)
                    .IsRequired();

                // Nastavenie hodnoty `Rating` - môže byť null
                entity.Property(h => h.Rating)
                    .HasColumnType("float"); // Typ `float` pre hodnotenie

                // Nastavenie hodnoty `RatingCount` - počet hodnotení, môže byť null
                entity.Property(h => h.RatingCount)
                    .IsRequired(false);

                // Dátumy vytvorenia, aktualizácie a zmazania
                entity.Property(h => h.CreatedAt)
                    .IsRequired();

                entity.Property(h => h.UpdatedAt)
                    .IsRequired(false);

                entity.Property(h => h.DeletedAt)
                    .IsRequired(false);
            });

            modelBuilder.Entity<HotelDetails>(entity =>
            {
                // Nastavenie primárneho kľúča
                entity.HasKey(hd => hd.Id);

                // Definovanie generovania GUID automaticky
                entity.Property(hd => hd.Id)
                    .IsRequired();

                // Definovanie cudzieho kľúča na entitu Hotel
                entity.Property(hd => hd.HotelId)
                    .IsRequired();

                // Definovanie cudzieho kľúča na entitu Cestovka
                entity.Property(hd => hd.CestovkaId)
                    .IsRequired();

                // IdHotelaCestovky je povinné a má maximálnu dĺžku 45 znakov
                entity.Property(hd => hd.IdHotelaCestovky)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(hd => hd.Description)
                   .IsRequired(false);

                // Vytvorenie vzťahu medzi HotelDetails a Hotel (1:1 alebo M:1)
                entity.HasOne<Hotel>()
                    .WithMany()  // Predpokladáme, že Hotel nemá navigáciu (1:1 alebo M:1)
                    .HasForeignKey(hd => hd.HotelId)
                    .OnDelete(DeleteBehavior.Cascade);  // Ak sa zmaže Hotel, vymažú sa aj detaily hotela

                // Vytvorenie vzťahu medzi HotelDetails a Cestovka (1:1 alebo M:1)
                entity.HasOne<Cestovka>()
                    .WithMany()  // Predpokladáme, že Cestovka nemá navigáciu (1:1 alebo M:1)
                    .HasForeignKey(hd => hd.CestovkaId)
                    .OnDelete(DeleteBehavior.Cascade);  // Ak sa zmaže Cestovka, vymažú sa aj detaily hotela
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasKey(c => c.Id); // Nastavenie primárneho kľúča

                entity.Property(c => c.Id)
                    .IsRequired();

                entity.Property(c => c.CurrencyName)
                    .IsRequired()
                    .HasMaxLength(3); // Maximálna dĺžka 50 znakov pre názov v slovenčine

                entity.Property(c => c.CreatedAt)
                    .IsRequired();

                entity.Property(c => c.UpdatedAt)
                    .IsRequired(false); // Nullable - môže byť null, ak nebol aktualizovaný

                entity.Property(c => c.DeletedAt)
                    .IsRequired(false); // Nullable - ak je null, záznam nie je zmazaný
            });

            modelBuilder.Entity<Food>(entity =>
            {
                entity.HasKey(c => c.Id); // Nastavenie primárneho kľúča

                entity.Property(c => c.Id)
                    .IsRequired();

                entity.Property(c => c.FoodName)
                    .IsRequired()
                    .HasMaxLength(40); // Maximálna dĺžka 50 znakov pre názov v slovenčine

                entity.Property(c => c.CreatedAt)
                    .IsRequired();

                entity.Property(c => c.UpdatedAt)
                    .IsRequired(false); // Nullable - môže byť null, ak nebol aktualizovaný

                entity.Property(c => c.DeletedAt)
                    .IsRequired(false); // Nullable - ak je null, záznam nie je zmazaný
            });

            modelBuilder.Entity<TourTypes>(entity =>
            {
                entity.HasKey(c => c.Id); 

                entity.Property(c => c.Id)
                    .IsRequired();

                entity.Property(c => c.TourTypesName)
                    .IsRequired()
                    .HasMaxLength(30); 

                entity.Property(c => c.CreatedAt)
                    .IsRequired();

                entity.Property(c => c.UpdatedAt)
                    .IsRequired(false); 

                entity.Property(c => c.DeletedAt)
                    .IsRequired(false); 
            });

            modelBuilder.Entity<Transport>(entity =>
            {
                // Nastavenie primárneho kľúča
                entity.HasKey(l => l.Id);

                // Definovanie generovania GUID automaticky
                entity.Property(l => l.Id)
                    .IsRequired();

                entity.HasOne<Transport>()  
                    .WithMany()  
                    .HasForeignKey(l => l.ParentTransportId) 
                    .OnDelete(DeleteBehavior.SetNull); 

                // Definovanie vlastností s dĺžkou
                entity.Property(l => l.TransportName)
                    .IsRequired()
                    .HasMaxLength(30);

                // Dátumy vytvorenia, aktualizácie a zmazania
                entity.Property(l => l.CreatedAt)
                    .IsRequired();

                entity.Property(l => l.UpdatedAt)
                    .IsRequired(false);

                entity.Property(l => l.DeletedAt)
                    .IsRequired(false);
            });

            modelBuilder.Entity<Offer>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Price)
                      .IsRequired();


                entity.Property(e => e.Tax)
                      .IsRequired();


                entity.Property(e => e.TotalPrice)
                      .IsRequired();


                entity.Property(e => e.Url)
                  .IsRequired();


                entity.Property(e => e.SOfferId)
                      .HasMaxLength(50);

                entity.Property(e => e.Discount)
                      .HasMaxLength(10);

                entity.Property(e => e.From)
                      .IsRequired();

                entity.Property(e => e.To)
                      .IsRequired();

                entity.Property(e => e.Length)
                      .IsRequired();

                // Foreign keys
                entity.HasOne<Cestovka>()
                      .WithMany()
                      .HasForeignKey(e => e.CestovkaId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne<Hotel>()
                      .WithMany()
                      .HasForeignKey(e => e.HotelId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne<Currency>()
                      .WithMany()
                      .HasForeignKey(e => e.CurrencyId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Food>()
                      .WithMany()
                      .HasForeignKey(e => e.FoodId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Transport>()
                      .WithMany()
                      .HasForeignKey(e => e.TransportationId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<OfferTourTypes>(entity =>
            {
                // Nastavenie primárneho kľúča
                entity.HasKey(l => l.Id);

                // Definovanie generovania GUID automaticky
                entity.Property(l => l.Id)
                    .IsRequired();

                entity.HasOne<Offer>()    
                    .WithMany()              
                    .HasForeignKey(l => l.OfferId) 
                    .OnDelete(DeleteBehavior.Restrict); 

                entity.HasOne<TourTypes>()  
                    .WithMany()  
                    .HasForeignKey(l => l.TourTypesId) 
                    .OnDelete(DeleteBehavior.Restrict); 

               
                entity.Property(l => l.CreatedAt)
                    .IsRequired();

                entity.Property(l => l.UpdatedAt)
                    .IsRequired(false);

                entity.Property(l => l.DeletedAt)
                    .IsRequired(false);
            });

            modelBuilder.Entity<Offer>()
                .HasOne<Cestovka>()  
                .WithMany()  
                .HasForeignKey(o => o.CestovkaId);

            // Vzťah Images -> Hotel (každý obrázok patrí k jednému hotelu)
            modelBuilder.Entity<Images>()
                .HasOne<Hotel>()
                .WithMany()
                .HasForeignKey(i => i.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            // Vzťah HotelCestovka -> Hotel (každý záznam patrí k jednému hotelu)
            modelBuilder.Entity<HotelDetails>()
                .HasOne<Hotel>()
                .WithMany()
                .HasForeignKey(hc => hc.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            // Vzťah HotelCestovka -> Cestovka (každý záznam patrí k jednej cestovnej kancelárii)
            modelBuilder.Entity<HotelDetails>()
                .HasOne<Cestovka>()
                .WithMany()
                .HasForeignKey(hc => hc.CestovkaId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
