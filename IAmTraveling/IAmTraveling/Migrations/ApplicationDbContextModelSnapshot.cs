﻿// <auto-generated />
using System;
using IAmTraveling.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IAmTraveling.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("IAmTraveling.Data.ApplicationDbContext+Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentId"));

                    b.Property<DateTime>("CommentDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ThingId")
                        .HasColumnType("int");

                    b.HasKey("CommentId");

                    b.HasIndex("ThingId");

                    b.ToTable("Comments", t =>
                        {
                            t.HasComment("Comments that can be made by other authenticated users");
                        });
                });

            modelBuilder.Entity("IAmTraveling.Data.ApplicationDbContext+MediaFile", b =>
                {
                    b.Property<int>("MediaFileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MediaFileId"));

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ThingId")
                        .HasColumnType("int");

                    b.HasKey("MediaFileId");

                    b.HasIndex("ThingId");

                    b.ToTable("MediaFiles");
                });

            modelBuilder.Entity("IAmTraveling.Data.ApplicationDbContext+Thing", b =>
                {
                    b.Property<int>("ThingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ThingId"));

                    b.Property<DateTime>("AddedToDbDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasComment("System-provided: DateTime added to database");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Description of the event, that user can add for their memories");

                    b.Property<DateTime?>("ThingDate")
                        .HasColumnType("datetime2")
                        .HasComment("User-provided: date of the thing or event");

                    b.Property<int?>("ThingLocationId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasComment("System-provided: DateTime of most recent updates to the particular entry");

                    b.Property<string>("UserIdId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ThingId");

                    b.HasIndex("ThingLocationId");

                    b.HasIndex("UserIdId");

                    b.ToTable("Things");
                });

            modelBuilder.Entity("IAmTraveling.Data.ApplicationDbContext+ThingLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FormattedAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("LatitudeCoord")
                        .HasColumnType("float");

                    b.Property<double?>("LongitudeCoord")
                        .HasColumnType("float");

                    b.Property<string>("PlaceId")
                        .HasColumnType("nvarchar(max)")
                        .HasComment("PlaceId and subsequent fields are provided by GoogleMaps (or other online map API)");

                    b.Property<string>("PlaceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProvinceOrState")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeOfPlace")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ThingLocations");
                });

            modelBuilder.Entity("IAmTraveling.Data.ApplicationDbContext+TravelCompanion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CompanionId1Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CompanionId2Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CompanionId1Id");

                    b.HasIndex("CompanionId2Id");

                    b.ToTable("TravelCompanions", t =>
                        {
                            t.HasComment("TravelCompanions authorizes whether one person can view anothers' content");
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "1",
                            ConcurrencyStamp = "1",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "2",
                            ConcurrencyStamp = "1",
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = "3",
                            ConcurrencyStamp = "1",
                            Name = "Disabled",
                            NormalizedName = "DISABLED"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "b3ae9005-977c-44b7-b070-cf18882acdbd",
                            Email = "admin@temples.com",
                            EmailConfirmed = true,
                            LockoutEnabled = true,
                            NormalizedEmail = "ADMIN@TEMPLES.COM",
                            NormalizedUserName = "ADMIN@TEMPLES.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEAjdBNymIo8fqbMAmdezxVlzVyCX2VzWcEgeVOH/u+LBKAEAWqeOuYyneH4+X8U5Kw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "f352d1d3-4e22-4a6c-83b9-0a202a73cb9d",
                            TwoFactorEnabled = false,
                            UserName = "admin@temples.com"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                            RoleId = "1"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("IAmTraveling.Data.ApplicationDbContext+Comment", b =>
                {
                    b.HasOne("IAmTraveling.Data.ApplicationDbContext+Thing", "Thing")
                        .WithMany("Comments")
                        .HasForeignKey("ThingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Thing");
                });

            modelBuilder.Entity("IAmTraveling.Data.ApplicationDbContext+MediaFile", b =>
                {
                    b.HasOne("IAmTraveling.Data.ApplicationDbContext+Thing", "Thing")
                        .WithMany("MediaFiles")
                        .HasForeignKey("ThingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Thing");
                });

            modelBuilder.Entity("IAmTraveling.Data.ApplicationDbContext+Thing", b =>
                {
                    b.HasOne("IAmTraveling.Data.ApplicationDbContext+ThingLocation", "ThingLocation")
                        .WithMany("Things")
                        .HasForeignKey("ThingLocationId");

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "UserId")
                        .WithMany()
                        .HasForeignKey("UserIdId");

                    b.Navigation("ThingLocation");

                    b.Navigation("UserId");
                });

            modelBuilder.Entity("IAmTraveling.Data.ApplicationDbContext+TravelCompanion", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "CompanionId1")
                        .WithMany()
                        .HasForeignKey("CompanionId1Id");

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "CompanionId2")
                        .WithMany()
                        .HasForeignKey("CompanionId2Id");

                    b.Navigation("CompanionId1");

                    b.Navigation("CompanionId2");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IAmTraveling.Data.ApplicationDbContext+Thing", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("MediaFiles");
                });

            modelBuilder.Entity("IAmTraveling.Data.ApplicationDbContext+ThingLocation", b =>
                {
                    b.Navigation("Things");
                });
#pragma warning restore 612, 618
        }
    }
}
