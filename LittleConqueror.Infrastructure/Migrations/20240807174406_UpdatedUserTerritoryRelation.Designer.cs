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
    [Migration("20240807174406_UpdatedUserTerritoryRelation")]
    partial class UpdatedUserTerritoryRelation
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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

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
                            Id = -1,
                            Hash = "$2a$13$Rp36yy.PiWho4KPs24Kc6.CONcgB8fB2DY.6j3defNtJVdQKw6rSq",
                            Role = "Admin",
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("LittleConqueror.AppService.Domain.Models.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

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

                    b.Property<int?>("TerritoryId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TerritoryId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("LittleConqueror.AppService.Domain.Models.Entities.Territory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("OwnerId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId")
                        .IsUnique();

                    b.ToTable("Territories");
                });

            modelBuilder.Entity("LittleConqueror.AppService.Domain.Models.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

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

                    b.Navigation("Territory");
                });
#pragma warning restore 612, 618
        }
    }
}
