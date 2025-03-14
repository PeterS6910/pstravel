﻿// <auto-generated />
using System;
using ImportXml.AfiTravelModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ImportXml.Migrations
{
    [DbContext(typeof(OfferDbContex))]
    partial class OfferDbContexModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("ImportXml.AfiTravelModel.Actionattributes", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Attr")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<Guid>("OfferId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("OfferId");

                    b.ToTable("Actionattributes");
                });

            modelBuilder.Entity("ImportXml.AfiTravelModel.Airports", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Airport")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<Guid>("OfferId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("OfferId");

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("ImportXml.AfiTravelModel.Coords", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("HotelInfoId")
                        .HasColumnType("char(36)");

                    b.Property<double>("Lat")
                        .HasColumnType("double");

                    b.Property<double>("Lng")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("HotelInfoId")
                        .IsUnique();

                    b.ToTable("Coords");
                });

            modelBuilder.Entity("ImportXml.AfiTravelModel.Destination", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<string>("Locality")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("varchar(120)");

                    b.Property<Guid>("OfferId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("OfferId")
                        .IsUnique();

                    b.ToTable("Destination");
                });

            modelBuilder.Entity("ImportXml.AfiTravelModel.HotelInfo", b =>
                {
                    b.Property<Guid>("HotelInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Id")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<Guid>("OfferId")
                        .HasColumnType("char(36)");

                    b.Property<double?>("Rating")
                        .HasColumnType("double");

                    b.Property<int?>("RatingCount")
                        .HasColumnType("int");

                    b.Property<int>("Stars")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.HasKey("HotelInfoId");

                    b.HasIndex("OfferId")
                        .IsUnique();

                    b.ToTable("HotelInfo");
                });

            modelBuilder.Entity("ImportXml.AfiTravelModel.Offer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Food")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Hotel")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ImageHeight")
                        .HasColumnType("int");

                    b.Property<int>("ImageWidth")
                        .HasColumnType("int");

                    b.Property<Guid>("OffersId")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("PriceCurrency")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<decimal>("Tax")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("TaxCurrency")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("TermType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("TotalPriceCurrency")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Transportation")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("OffersId");

                    b.ToTable("Offer");
                });

            modelBuilder.Entity("ImportXml.AfiTravelModel.Offers", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("ImportXml.AfiTravelModel.Photo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("OfferId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("OfferId");

                    b.ToTable("Photo");
                });

            modelBuilder.Entity("ImportXml.AfiTravelModel.Term", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("From")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Length")
                        .HasColumnType("int");

                    b.Property<Guid>("OfferId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("To")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("OfferId")
                        .IsUnique();

                    b.ToTable("Term");
                });

            modelBuilder.Entity("ImportXml.AfiTravelModel.TourType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("OfferId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("OfferId");

                    b.ToTable("TourType");
                });

            modelBuilder.Entity("ImportXml.AfiTravelModel.Actionattributes", b =>
                {
                    b.HasOne("ImportXml.AfiTravelModel.Offer", null)
                        .WithMany("Actionattributes")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ImportXml.AfiTravelModel.Airports", b =>
                {
                    b.HasOne("ImportXml.AfiTravelModel.Offer", null)
                        .WithMany("Airports")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ImportXml.AfiTravelModel.Coords", b =>
                {
                    b.HasOne("ImportXml.AfiTravelModel.HotelInfo", null)
                        .WithOne("Coords")
                        .HasForeignKey("ImportXml.AfiTravelModel.Coords", "HotelInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ImportXml.AfiTravelModel.Destination", b =>
                {
                    b.HasOne("ImportXml.AfiTravelModel.Offer", null)
                        .WithOne("Destination")
                        .HasForeignKey("ImportXml.AfiTravelModel.Destination", "OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ImportXml.AfiTravelModel.HotelInfo", b =>
                {
                    b.HasOne("ImportXml.AfiTravelModel.Offer", null)
                        .WithOne("HotelInfo")
                        .HasForeignKey("ImportXml.AfiTravelModel.HotelInfo", "OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ImportXml.AfiTravelModel.Offer", b =>
                {
                    b.HasOne("ImportXml.AfiTravelModel.Offers", null)
                        .WithMany("Offer")
                        .HasForeignKey("OffersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ImportXml.AfiTravelModel.Photo", b =>
                {
                    b.HasOne("ImportXml.AfiTravelModel.Offer", null)
                        .WithMany("Photos")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ImportXml.AfiTravelModel.Term", b =>
                {
                    b.HasOne("ImportXml.AfiTravelModel.Offer", null)
                        .WithOne("Term")
                        .HasForeignKey("ImportXml.AfiTravelModel.Term", "OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ImportXml.AfiTravelModel.TourType", b =>
                {
                    b.HasOne("ImportXml.AfiTravelModel.Offer", null)
                        .WithMany("TourType")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ImportXml.AfiTravelModel.HotelInfo", b =>
                {
                    b.Navigation("Coords")
                        .IsRequired();
                });

            modelBuilder.Entity("ImportXml.AfiTravelModel.Offer", b =>
                {
                    b.Navigation("Actionattributes");

                    b.Navigation("Airports");

                    b.Navigation("Destination")
                        .IsRequired();

                    b.Navigation("HotelInfo")
                        .IsRequired();

                    b.Navigation("Photos");

                    b.Navigation("Term")
                        .IsRequired();

                    b.Navigation("TourType");
                });

            modelBuilder.Entity("ImportXml.AfiTravelModel.Offers", b =>
                {
                    b.Navigation("Offer");
                });
#pragma warning restore 612, 618
        }
    }
}
