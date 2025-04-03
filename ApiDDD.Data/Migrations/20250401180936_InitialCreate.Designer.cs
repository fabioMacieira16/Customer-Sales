﻿// <auto-generated />
using System;
using ApiDDD.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace SalesAPI.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250401180936_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.27");

            modelBuilder.Entity("SalesAPI.Domain.Entities.Sale", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsCancelled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("SaleDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("SaleNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TotalAmount")
                        .HasPrecision(10, 2)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("SalesAPI.Domain.Entities.SaleItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Discount")
                        .HasPrecision(4, 2)
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("SaleId")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TotalAmount")
                        .HasPrecision(10, 2)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("UnitPrice")
                        .HasPrecision(10, 2)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SaleId");

                    b.ToTable("SaleItems");
                });

            modelBuilder.Entity("SalesAPI.Domain.Entities.Sale", b =>
                {
                    b.OwnsOne("SalesAPI.Domain.ValueObjects.BranchInfo", "Branch", b1 =>
                        {
                            b1.Property<Guid>("SaleId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Address")
                                .HasMaxLength(200)
                                .HasColumnType("TEXT");

                            b1.Property<string>("City")
                                .HasMaxLength(50)
                                .HasColumnType("TEXT");

                            b1.Property<Guid>("Id")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("TEXT");

                            b1.Property<string>("State")
                                .HasMaxLength(50)
                                .HasColumnType("TEXT");

                            b1.HasKey("SaleId");

                            b1.ToTable("BranchInfos");

                            b1.WithOwner()
                                .HasForeignKey("SaleId");
                        });

                    b.OwnsOne("SalesAPI.Domain.ValueObjects.CustomerInfo", "Customer", b1 =>
                        {
                            b1.Property<Guid>("SaleId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Email")
                                .HasMaxLength(100)
                                .HasColumnType("TEXT");

                            b1.Property<Guid>("Id")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("TEXT");

                            b1.Property<string>("Phone")
                                .HasMaxLength(20)
                                .HasColumnType("TEXT");

                            b1.HasKey("SaleId");

                            b1.ToTable("CustomerInfos");

                            b1.WithOwner()
                                .HasForeignKey("SaleId");
                        });

                    b.Navigation("Branch");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("SalesAPI.Domain.Entities.SaleItem", b =>
                {
                    b.HasOne("SalesAPI.Domain.Entities.Sale", "Sales")
                        .WithMany("Items")
                        .HasForeignKey("SaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("SalesAPI.Domain.ValueObjects.ProductInfo", "Product", b1 =>
                        {
                            b1.Property<Guid>("SaleItemId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Brand")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Category")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Description")
                                .HasColumnType("TEXT");

                            b1.Property<Guid>("Id")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("TEXT");

                            b1.HasKey("SaleItemId");

                            b1.ToTable("ProductInfos");

                            b1.WithOwner()
                                .HasForeignKey("SaleItemId");
                        });

                    b.Navigation("Product");

                    b.Navigation("Sales");
                });

            modelBuilder.Entity("SalesAPI.Domain.Entities.Sale", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
