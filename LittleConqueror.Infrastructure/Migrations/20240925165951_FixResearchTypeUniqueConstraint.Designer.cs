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
    [Migration("20240925165951_FixResearchTypeUniqueConstraint")]
    partial class FixResearchTypeUniqueConstraint
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Action", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Actions");

                    b.UseTptMappingStrategy();
                });

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
                            Hash = "$2a$13$LTSgQKByHPhK7KG2WZgBIeelE.kuddR4uguTJ9u7keWZ3gfJVu1DK",
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

                    b.Property<string>("AddressType")
                        .HasColumnType("text");

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

            modelBuilder.Entity("LittleConqueror.AppService.Domain.Models.Entities.Configs", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Configs");
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

            modelBuilder.Entity("LittleConqueror.AppService.Domain.Models.Entities.TechResearch", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("ResearchCategory")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ResearchDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("ResearchStatus")
                        .HasColumnType("integer");

                    b.Property<int>("ResearchType")
                        .HasColumnType("integer");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId", "ResearchType")
                        .IsUnique();

                    b.ToTable("TechResearches");
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

            modelBuilder.Entity("LittleConqueror.Infrastructure.DatabaseAdapters.DbDto.BackgroundJobIdentifierDbDto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("JobIdentifier")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("BackgroundJobIdentifiers");
                });

            modelBuilder.Entity("LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Agricole", b =>
                {
                    b.HasBaseType("LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Action");

                    b.ToTable("ActionsAgricoles", (string)null);
                });

            modelBuilder.Entity("LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Diplomatique", b =>
                {
                    b.HasBaseType("LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Action");

                    b.ToTable("ActionsDiplomatiques", (string)null);
                });

            modelBuilder.Entity("LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Espionnage", b =>
                {
                    b.HasBaseType("LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Action");

                    b.ToTable("ActionsEspionnages", (string)null);
                });

            modelBuilder.Entity("LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Militaire", b =>
                {
                    b.HasBaseType("LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Action");

                    b.ToTable("ActionsMilitaires", (string)null);
                });

            modelBuilder.Entity("LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Miniere", b =>
                {
                    b.HasBaseType("LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Action");

                    b.Property<int>("ResourceType")
                        .HasColumnType("integer");

                    b.ToTable("ActionsMiniere", (string)null);
                });

            modelBuilder.Entity("LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Technologique", b =>
                {
                    b.HasBaseType("LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Action");

                    b.Property<int>("TechResearchCategory")
                        .HasColumnType("integer");

                    b.ToTable("ActionsTechnologiques", (string)null);
                });

            modelBuilder.Entity("LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Action", b =>
                {
                    b.HasOne("LittleConqueror.AppService.Domain.Models.Entities.City", "City")
                        .WithOne("Action")
                        .HasForeignKey("LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Action", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
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

            modelBuilder.Entity("LittleConqueror.AppService.Domain.Models.Entities.Configs", b =>
                {
                    b.OwnsMany("LittleConqueror.AppService.Domain.Models.TechResearches.Configs.TechConfig", "TechResearchConfigs", b1 =>
                        {
                            b1.Property<long>("ConfigsId")
                                .HasColumnType("bigint");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            b1.Property<int>("Category")
                                .HasColumnType("integer");

                            b1.Property<int>("Cost")
                                .HasColumnType("integer");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<int[]>("PreReqs")
                                .IsRequired()
                                .HasColumnType("integer[]");

                            b1.Property<TimeSpan>("ResearchTime")
                                .HasColumnType("interval");

                            b1.Property<int>("Type")
                                .HasColumnType("integer");

                            b1.HasKey("ConfigsId", "Id");

                            b1.ToTable("Configs");

                            b1.ToJson("TechResearchConfigs");

                            b1.WithOwner()
                                .HasForeignKey("ConfigsId");
                        });

                    b.Navigation("TechResearchConfigs");
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

            modelBuilder.Entity("LittleConqueror.AppService.Domain.Models.Entities.TechResearch", b =>
                {
                    b.HasOne("LittleConqueror.AppService.Domain.Models.Entities.User", "User")
                        .WithMany("TechResearches")
                        .HasForeignKey("UserId")
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

            modelBuilder.Entity("LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Agricole", b =>
                {
                    b.HasOne("LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Action", null)
                        .WithOne()
                        .HasForeignKey("LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Agricole", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Diplomatique", b =>
                {
                    b.HasOne("LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Action", null)
                        .WithOne()
                        .HasForeignKey("LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Diplomatique", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Espionnage", b =>
                {
                    b.HasOne("LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Action", null)
                        .WithOne()
                        .HasForeignKey("LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Espionnage", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Militaire", b =>
                {
                    b.HasOne("LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Action", null)
                        .WithOne()
                        .HasForeignKey("LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Militaire", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Miniere", b =>
                {
                    b.HasOne("LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Action", null)
                        .WithOne()
                        .HasForeignKey("LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Miniere", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Technologique", b =>
                {
                    b.HasOne("LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Action", null)
                        .WithOne()
                        .HasForeignKey("LittleConqueror.AppService.Domain.Models.Entities.ActionEntities.Technologique", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LittleConqueror.AppService.Domain.Models.Entities.City", b =>
                {
                    b.Navigation("Action");
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

                    b.Navigation("TechResearches");

                    b.Navigation("Territory");
                });
#pragma warning restore 612, 618
        }
    }
}
