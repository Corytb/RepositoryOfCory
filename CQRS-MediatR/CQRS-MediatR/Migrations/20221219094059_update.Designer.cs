﻿// <auto-generated />
using System;
using CQRS_MediatR.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CQRSMediatR.Migrations
{
    [DbContext(typeof(ProductContext))]
    [Migration("20221219094059_update")]
    partial class update
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CQRS_MediatR.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BusinessName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customer");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BusinessName = "Great Company 1",
                            PhoneNumber = "612-867-5309"
                        },
                        new
                        {
                            Id = 2,
                            BusinessName = "Wonderful Wonder-Bread",
                            PhoneNumber = "612-407-9242"
                        },
                        new
                        {
                            Id = 3,
                            BusinessName = "Wow!",
                            PhoneNumber = "612-866-6537"
                        });
                });

            modelBuilder.Entity("CQRS_MediatR.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProductId");

                    b.ToTable("Order");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CustomerId = 1,
                            OrderDate = new DateTime(2021, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductId = 1
                        },
                        new
                        {
                            Id = 2,
                            CustomerId = 1,
                            OrderDate = new DateTime(2021, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductId = 2
                        },
                        new
                        {
                            Id = 3,
                            CustomerId = 2,
                            OrderDate = new DateTime(2021, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductId = 1
                        },
                        new
                        {
                            Id = 4,
                            CustomerId = 2,
                            OrderDate = new DateTime(2021, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductId = 3
                        },
                        new
                        {
                            Id = 5,
                            CustomerId = 3,
                            OrderDate = new DateTime(2021, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductId = 1
                        });
                });

            modelBuilder.Entity("CQRS_MediatR.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Product");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Shirt",
                            Price = 49.99m
                        },
                        new
                        {
                            Id = 2,
                            Name = "Pants",
                            Price = 29.99m
                        },
                        new
                        {
                            Id = 3,
                            Name = "Socks",
                            Price = 39.99m
                        },
                        new
                        {
                            Id = 4,
                            Name = "Shoes",
                            Price = 19.99m
                        });
                });

            modelBuilder.Entity("CQRS_MediatR.Models.Order", b =>
                {
                    b.HasOne("CQRS_MediatR.Models.Customer", "Customer")
                        .WithMany("Order")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CQRS_MediatR.Models.Product", "Product")
                        .WithMany("Order")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CQRS_MediatR.Models.Customer", b =>
                {
                    b.Navigation("Order");
                });

            modelBuilder.Entity("CQRS_MediatR.Models.Product", b =>
                {
                    b.Navigation("Order");
                });
#pragma warning restore 612, 618
        }
    }
}
