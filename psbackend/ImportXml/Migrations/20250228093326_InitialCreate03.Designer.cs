﻿// <auto-generated />
using System;
using ImportXml.AfiTravelModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ImportXml.Migrations
{
    [DbContext(typeof(OfferDbContex))]
    [Migration("20250228093326_InitialCreate03")]
    partial class InitialCreate03
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("ImportXml.AfiTravelModel.Offer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ImageHeight")
                        .HasColumnType("int");

                    b.Property<int>("ImageWidth")
                        .HasColumnType("int");

                    b.Property<Guid>("OffersId")
                        .HasColumnType("char(36)");

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

            modelBuilder.Entity("ImportXml.AfiTravelModel.Offer", b =>
                {
                    b.HasOne("ImportXml.AfiTravelModel.Offers", null)
                        .WithMany("Offer")
                        .HasForeignKey("OffersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ImportXml.AfiTravelModel.Offers", b =>
                {
                    b.Navigation("Offer");
                });
#pragma warning restore 612, 618
        }
    }
}
