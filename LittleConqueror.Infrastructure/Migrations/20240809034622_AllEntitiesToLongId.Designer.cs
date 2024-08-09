﻿// <auto-generated />
using System;
using LittleConqueror.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LittleConqueror.Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240809034622_AllEntitiesToLongId")]
    partial class AllEntitiesToLongId
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LittleConqueror.AppService.Domain.Models.Entities.AuthUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("AuthUsers");

                    b.HasData(
                        new
                        {
                            Id = -1L,
                            Hash = "$2a$13$FWVeWDfCWuRqInM7ugLTEOBD0Uw7RTzr7dGm2I9M/i98f3r.8lJpy",
                            Role = "Admin",
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("LittleConqueror.AppService.Domain.Models.Entities.City", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Geojson")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<char>("OsmType")
                        .HasColumnType("character(1)");

                    b.Property<int>("Population")
                        .HasColumnType("integer");

                    b.Property<long?>("TerritoryId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TerritoryId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("LittleConqueror.AppService.Domain.Models.Entities.Resources", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("Diamond")
                        .HasColumnType("integer");

                    b.Property<int>("Food")
                        .HasColumnType("integer");

                    b.Property<int>("Gold")
                        .HasColumnType("integer");

                    b.Property<int>("Iron")
                        .HasColumnType("integer");

                    b.Property<int>("Petrol")
                        .HasColumnType("integer");

                    b.Property<int>("Stone")
                        .HasColumnType("integer");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<int>("Wood")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("LittleConqueror.AppService.Domain.Models.Entities.Territory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("OwnerId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId")
                        .IsUnique();

                    b.ToTable("Territories");
                });

            modelBuilder.Entity("LittleConqueror.AppService.Domain.Models.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LittleConqueror.AppService.Domain.Models.Entities.AuthUser", b =>
                {
                    b.HasOne("LittleConqueror.AppService.Domain.Models.Entities.User", "User")
                        .WithOne("AuthUser")
                        .HasForeignKey("LittleConqueror.AppService.Domain.Models.Entities.AuthUser", "UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LittleConqueror.AppService.Domain.Models.Entities.City", b =>
                {
                    b.HasOne("LittleConqueror.AppService.Domain.Models.Entities.Territory", "Territory")
                        .WithMany("Cities")
                        .HasForeignKey("TerritoryId");

                    b.Navigation("Territory");
                });

            modelBuilder.Entity("LittleConqueror.AppService.Domain.Models.Entities.Resources", b =>
                {
                    b.HasOne("LittleConqueror.AppService.Domain.Models.Entities.User", "User")
                        .WithOne("Resources")
                        .HasForeignKey("LittleConqueror.AppService.Domain.Models.Entities.Resources", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LittleConqueror.AppService.Domain.Models.Entities.Territory", b =>
                {
                    b.HasOne("LittleConqueror.AppService.Domain.Models.Entities.User", "Owner")
                        .WithOne("Territory")
                        .HasForeignKey("LittleConqueror.AppService.Domain.Models.Entities.Territory", "OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("LittleConqueror.AppService.Domain.Models.Entities.Territory", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("LittleConqueror.AppService.Domain.Models.Entities.User", b =>
                {
                    b.Navigation("AuthUser")
                        .IsRequired();

                    b.Navigation("Resources");

                    b.Navigation("Territory");
                });
#pragma warning restore 612, 618
        }
    }
}
