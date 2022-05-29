﻿// <auto-generated />
using System;
using Benchmark;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Benchmark.Migrations
{
    [DbContext(typeof(BenchmarkDbContext))]
    [Migration("20220529171816_MetricCompanyName")]
    partial class MetricCompanyName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DataStructures.Benchmark.Benchmark", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Benchmark");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Benchmark");
                });

            modelBuilder.Entity("DataStructures.Benchmark.CompanyLatestFiling", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BenchmarkId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LatestFilingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Ticker")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BenchmarkId");

                    b.ToTable("CompanyLatestFiling");
                });

            modelBuilder.Entity("DataStructures.Benchmark.Metric", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal?>("Average")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("BenchmarkID")
                        .HasColumnType("int");

                    b.Property<decimal?>("MaxValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Median")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("MetricName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("MinValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Percentile01")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Percentile05")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Percentile10")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Percentile25")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Percentile75")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Percentile90")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Percentile95")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Percentile99")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TopCompanies")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorstCompanies")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BenchmarkID");

                    b.ToTable("_Metrics");
                });

            modelBuilder.Entity("DataStructures.Benchmark.MetricDataPoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MetricId")
                        .HasColumnType("int");

                    b.Property<string>("Ticker")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("MetricId");

                    b.ToTable("MetricDataPoint");
                });

            modelBuilder.Entity("DataStructures.Benchmark.IndustryBenchmark", b =>
                {
                    b.HasBaseType("DataStructures.Benchmark.Benchmark");

                    b.Property<string>("IndustryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MarketName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SectorName")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("IndustryBenchmark");
                });

            modelBuilder.Entity("DataStructures.Benchmark.MarketBenchmark", b =>
                {
                    b.HasBaseType("DataStructures.Benchmark.Benchmark");

                    b.Property<string>("MarketName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("MarketBenchmark_MarketName");

                    b.HasDiscriminator().HasValue("MarketBenchmark");
                });

            modelBuilder.Entity("DataStructures.Benchmark.SectorBenchmark", b =>
                {
                    b.HasBaseType("DataStructures.Benchmark.Benchmark");

                    b.Property<string>("MarketName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("SectorBenchmark_MarketName");

                    b.Property<string>("SectorName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("SectorBenchmark_SectorName");

                    b.HasDiscriminator().HasValue("SectorBenchmark");
                });

            modelBuilder.Entity("DataStructures.Benchmark.CompanyLatestFiling", b =>
                {
                    b.HasOne("DataStructures.Benchmark.Benchmark", null)
                        .WithMany("CompanyDates")
                        .HasForeignKey("BenchmarkId");
                });

            modelBuilder.Entity("DataStructures.Benchmark.Metric", b =>
                {
                    b.HasOne("DataStructures.Benchmark.Benchmark", "Benchmark")
                        .WithMany("Metrics")
                        .HasForeignKey("BenchmarkID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Benchmark");
                });

            modelBuilder.Entity("DataStructures.Benchmark.MetricDataPoint", b =>
                {
                    b.HasOne("DataStructures.Benchmark.Metric", null)
                        .WithMany("RawDataPoints")
                        .HasForeignKey("MetricId");
                });

            modelBuilder.Entity("DataStructures.Benchmark.Benchmark", b =>
                {
                    b.Navigation("CompanyDates");

                    b.Navigation("Metrics");
                });

            modelBuilder.Entity("DataStructures.Benchmark.Metric", b =>
                {
                    b.Navigation("RawDataPoints");
                });
#pragma warning restore 612, 618
        }
    }
}