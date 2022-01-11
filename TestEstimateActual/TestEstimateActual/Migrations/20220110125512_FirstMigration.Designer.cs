﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestEstimateActual.Models;

namespace TestEstimateActual.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220110125512_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TestEstimateActual.Models.Actual", b =>
                {
                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<int>("ActualHouseholds")
                        .HasColumnType("int");

                    b.Property<int>("ActualPopulation")
                        .HasColumnType("int");

                    b.HasKey("State");

                    b.ToTable("Actuals");
                });

            modelBuilder.Entity("TestEstimateActual.Models.Estimate", b =>
                {
                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<int>("District")
                        .HasColumnType("int");

                    b.Property<int>("EstimateHouseholds")
                        .HasColumnType("int");

                    b.Property<int>("EstimatePopulation")
                        .HasColumnType("int");

                    b.HasKey("State", "District")
                        .HasName("estimate_pk");

                    b.ToTable("Estimates");
                });
#pragma warning restore 612, 618
        }
    }
}
