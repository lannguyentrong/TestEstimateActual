// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestEstimateActual.Models;

namespace TestEstimateActual.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.13");

            modelBuilder.Entity("TestEstimateActual.Models.Actual", b =>
                {
                    b.Property<int>("State")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ActualHouseholds")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ActualPopulation")
                        .HasColumnType("INTEGER");

                    b.HasKey("State");

                    b.ToTable("Actuals");
                });

            modelBuilder.Entity("TestEstimateActual.Models.Estimate", b =>
                {
                    b.Property<int>("State")
                        .HasColumnType("INTEGER");

                    b.Property<int>("District")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EstimateHouseholds")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EstimatePopulation")
                        .HasColumnType("INTEGER");

                    b.HasKey("State", "District")
                        .HasName("estimate_pk");

                    b.ToTable("Estimates");
                });
#pragma warning restore 612, 618
        }
    }
}
