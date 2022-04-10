﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecommendCoffee.Catalog.Infrastructure.Persistence;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220226070147_CreateSchema")]
    partial class CreateSchema
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RecommendCoffee.Catalog.Domain.Aggregates.ProductAggregate.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Discontinued")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("RecommendCoffee.Catalog.Domain.Aggregates.ProductAggregate.Product", b =>
                {
                    b.OwnsMany("RecommendCoffee.Catalog.Domain.Aggregates.ProductAggregate.ProductVariant", "Variants", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"), 1L, 1);

                            b1.Property<decimal>("UnitPrice")
                                .HasPrecision(5, 2)
                                .HasColumnType("decimal(5,2)");

                            b1.Property<int>("Weight")
                                .HasColumnType("int");

                            b1.HasKey("ProductId", "Id");

                            b1.ToTable("ProductVariant");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.Navigation("Variants");
                });
#pragma warning restore 612, 618
        }
    }
}
