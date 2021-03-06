﻿// <auto-generated />
using System;
using CAPMessageBusWithRabbitMq.Core;
using CAPMessageBusWithRabbitMq.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CAPMessageBusWithRabbitMq.Web.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20200619012349_ConstrainColumnToXyPoints")]
    partial class ConstrainColumnToXyPoints
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:PostgresExtension:postgis", ",,")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("CAPMessageBusWithRabbitMq.Web.Trip", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<Point>("Destination")
                        .HasColumnType("geometry (point)");

                    b.Property<string>("DriverId")
                        .HasColumnType("character varying(30)")
                        .HasMaxLength(30);

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Point>("StartingPoint")
                        .HasColumnType("geometry (point)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("character varying(10)")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("Trips");
                });
#pragma warning restore 612, 618
        }
    }
}
