﻿// <auto-generated />
using System;
using GwizdWebAPI.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GwizdWebAPI.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20231001040000_FixSecond")]
    partial class FixSecond
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GwizdWebAPI.Entities.AnimalImageEntity", b =>
                {
                    b.Property<int>("AnimalImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AnimalImageId"));

                    b.Property<int?>("DisappearedAnimalEntityDisappearedAnimalId")
                        .HasColumnType("integer");

                    b.Property<int?>("FoundedAnimalEntityFoundedAnimalId")
                        .HasColumnType("integer");

                    b.Property<byte[]>("ImageBlob")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<bool>("IsAnimalLost")
                        .HasColumnType("boolean");

                    b.HasKey("AnimalImageId");

                    b.HasIndex("DisappearedAnimalEntityDisappearedAnimalId");

                    b.HasIndex("FoundedAnimalEntityFoundedAnimalId");

                    b.ToTable("AnimalImages");
                });

            modelBuilder.Entity("GwizdWebAPI.Entities.AnimalSuggestionEntity", b =>
                {
                    b.Property<int>("AnimalSuggestionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AnimalSuggestionID"));

                    b.Property<int?>("DisappearedAnimalId")
                        .HasColumnType("integer");

                    b.Property<int?>("FoundedAnimalId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsViewed")
                        .HasColumnType("boolean");

                    b.Property<double>("Similarity")
                        .HasColumnType("double precision");

                    b.HasKey("AnimalSuggestionID");

                    b.HasIndex("DisappearedAnimalId");

                    b.HasIndex("FoundedAnimalId");

                    b.ToTable("AnimalSuggestions");
                });

            modelBuilder.Entity("GwizdWebAPI.Entities.DisappearedAnimalEntity", b =>
                {
                    b.Property<int>("DisappearedAnimalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("DisappearedAnimalId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("integer");

                    b.Property<string>("SpeciesName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("DisappearedAnimalId");

                    b.HasIndex("OwnerId");

                    b.ToTable("DisappearedAnimals");
                });

            modelBuilder.Entity("GwizdWebAPI.Entities.FoundedAnimalEntity", b =>
                {
                    b.Property<int>("FoundedAnimalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("FoundedAnimalId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<int?>("ReporterId")
                        .HasColumnType("integer");

                    b.Property<string>("SpeciesName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("FoundedAnimalId");

                    b.HasIndex("ReporterId");

                    b.ToTable("FoundedAnimals");
                });

            modelBuilder.Entity("GwizdWebAPI.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GwizdWebAPI.Entities.AnimalImageEntity", b =>
                {
                    b.HasOne("GwizdWebAPI.Entities.DisappearedAnimalEntity", "DisappearedAnimal")
                        .WithMany("Images")
                        .HasForeignKey("DisappearedAnimalEntityDisappearedAnimalId");

                    b.HasOne("GwizdWebAPI.Entities.FoundedAnimalEntity", "FoundedAnimal")
                        .WithMany("Images")
                        .HasForeignKey("FoundedAnimalEntityFoundedAnimalId");

                    b.Navigation("DisappearedAnimal");

                    b.Navigation("FoundedAnimal");
                });

            modelBuilder.Entity("GwizdWebAPI.Entities.AnimalSuggestionEntity", b =>
                {
                    b.HasOne("GwizdWebAPI.Entities.DisappearedAnimalEntity", "DisappearedAnimal")
                        .WithMany()
                        .HasForeignKey("DisappearedAnimalId");

                    b.HasOne("GwizdWebAPI.Entities.FoundedAnimalEntity", "FoundedAnimal")
                        .WithMany()
                        .HasForeignKey("FoundedAnimalId");

                    b.Navigation("DisappearedAnimal");

                    b.Navigation("FoundedAnimal");
                });

            modelBuilder.Entity("GwizdWebAPI.Entities.DisappearedAnimalEntity", b =>
                {
                    b.HasOne("GwizdWebAPI.Entities.UserEntity", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("GwizdWebAPI.Entities.FoundedAnimalEntity", b =>
                {
                    b.HasOne("GwizdWebAPI.Entities.UserEntity", "Reporter")
                        .WithMany()
                        .HasForeignKey("ReporterId");

                    b.Navigation("Reporter");
                });

            modelBuilder.Entity("GwizdWebAPI.Entities.DisappearedAnimalEntity", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("GwizdWebAPI.Entities.FoundedAnimalEntity", b =>
                {
                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
