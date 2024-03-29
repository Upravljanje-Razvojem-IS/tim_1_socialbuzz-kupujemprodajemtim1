﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserService.Entities;

namespace UserService.Migrations
{
    [DbContext(typeof(UserDbContext))]
    [Migration("20210909002902_UserDbMigration")]
    partial class UserDbMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("UserService.Entities.CorporationUser", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserId");

                    b.Property<string>("CorporationAddress")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)")
                        .HasColumnName("CorporationAddress");

                    b.Property<string>("CorporationCity")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)")
                        .HasColumnName("CorporationCity");

                    b.Property<string>("CorporationName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("CorporationName");

                    b.Property<Guid>("CountryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Email");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("IsActive");

                    b.Property<string>("Pib")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("Pib");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("Telephone");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)")
                        .HasColumnName("Username");

                    b.HasKey("UserId");

                    b.HasIndex("CountryId");

                    b.HasIndex("RoleId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("CorporationUser");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("9171f23e-adf2-4698-b73f-05c6fd7ad1be"),
                            CorporationAddress = "Kisacka 25",
                            CorporationCity = "Novi Sad",
                            CorporationName = "Stark",
                            CountryId = new Guid("8c349e7b-1c97-486d-aa2e-e58205d11577"),
                            Email = "stark@gmail.com",
                            IsActive = true,
                            Pib = "515731",
                            RoleId = new Guid("33253633-10e4-45c8-9b8e-84020a5c8c58"),
                            Telephone = "+38160111222",
                            Username = "Stark"
                        },
                        new
                        {
                            UserId = new Guid("9346b8c4-1b3b-435f-9c35-35de3a76fcf9"),
                            CorporationAddress = "unknown",
                            CorporationCity = "Washington DC",
                            CorporationName = "National Bank",
                            CountryId = new Guid("ff0c9396-7c4c-4bf5-a12e-6aa79c272413"),
                            Email = "nat_bank@gmail.com",
                            IsActive = true,
                            Pib = "51516",
                            RoleId = new Guid("33253633-10e4-45c8-9b8e-84020a5c8c58"),
                            Telephone = "+38165555113",
                            Username = "NationalBank"
                        });
                });

            modelBuilder.Entity("UserService.Entities.Country", b =>
                {
                    b.Property<Guid>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CountryId");

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("CountryName");

                    b.HasKey("CountryId");

                    b.ToTable("Country");

                    b.HasData(
                        new
                        {
                            CountryId = new Guid("8c349e7b-1c97-486d-aa2e-e58205d11577"),
                            CountryName = "Serbia"
                        },
                        new
                        {
                            CountryId = new Guid("ff0c9396-7c4c-4bf5-a12e-6aa79c272413"),
                            CountryName = "US"
                        });
                });

            modelBuilder.Entity("UserService.Entities.PersonalUser", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserId");

                    b.Property<Guid>("CountryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("FirstName");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("IsActive");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("LastName");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("Telephone");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)")
                        .HasColumnName("Username");

                    b.HasKey("UserId");

                    b.HasIndex("CountryId");

                    b.HasIndex("RoleId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("PersonalUser");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("ce593d02-c615-4af6-a794-c450b79e9b4d"),
                            CountryId = new Guid("8c349e7b-1c97-486d-aa2e-e58205d11577"),
                            Email = "djolex3211@gmail.com",
                            FirstName = "Djordje",
                            IsActive = true,
                            LastName = "Stefanovic",
                            RoleId = new Guid("987268e5-f880-4f81-b1bf-5b9704604e26"),
                            Telephone = "+381628192354",
                            Username = "djolex"
                        },
                        new
                        {
                            UserId = new Guid("728569aa-7a1f-45c9-b9d4-94bcc176bd0c"),
                            CountryId = new Guid("8c349e7b-1c97-486d-aa2e-e58205d11577"),
                            Email = "marko@gmail.com",
                            FirstName = "Marko",
                            IsActive = true,
                            LastName = "Markovic",
                            RoleId = new Guid("33253633-10e4-45c8-9b8e-84020a5c8c58"),
                            Telephone = "+3816965555555",
                            Username = "markoMarkovic"
                        },
                        new
                        {
                            UserId = new Guid("194df880-d4ce-4997-96c9-878102eb6e0e"),
                            CountryId = new Guid("ff0c9396-7c4c-4bf5-a12e-6aa79c272413"),
                            Email = "nevena@gmail.com",
                            FirstName = "Nevena",
                            IsActive = true,
                            LastName = "Nikolic",
                            RoleId = new Guid("33253633-10e4-45c8-9b8e-84020a5c8c58"),
                            Telephone = "+381691234567",
                            Username = "nikolicNN"
                        });
                });

            modelBuilder.Entity("UserService.Entities.Role", b =>
                {
                    b.Property<Guid>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("RoleId");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("RoleName");

                    b.HasKey("RoleId");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            RoleId = new Guid("987268e5-f880-4f81-b1bf-5b9704604e26"),
                            RoleName = "Admin"
                        },
                        new
                        {
                            RoleId = new Guid("33253633-10e4-45c8-9b8e-84020a5c8c58"),
                            RoleName = "Regular user"
                        });
                });

            modelBuilder.Entity("UserService.Entities.CorporationUser", b =>
                {
                    b.HasOne("UserService.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UserService.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("UserService.Entities.PersonalUser", b =>
                {
                    b.HasOne("UserService.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UserService.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}
