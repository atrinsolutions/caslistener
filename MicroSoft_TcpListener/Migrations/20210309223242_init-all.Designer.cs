﻿// <auto-generated />
using System;
using MicroSoft_TcpListener;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MicroSoft_TcpListener.Migrations
{
    [DbContext(typeof(MainContext))]
    [Migration("20210309223242_init-all")]
    partial class initall
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CasListener.CasScalePackInfo", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Add")
                        .HasColumnType("bit");

                    b.Property<string>("Barcode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CasProtocolIdentifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("CurrentDate_day")
                        .HasColumnType("bigint");

                    b.Property<long>("CurrentDate_month")
                        .HasColumnType("bigint");

                    b.Property<long>("CurrentDate_year")
                        .HasColumnType("bigint");

                    b.Property<byte>("CurrentTicketSaleorder")
                        .HasColumnType("tinyint");

                    b.Property<long>("CurrentTime_hour")
                        .HasColumnType("bigint");

                    b.Property<long>("CurrentTime_min")
                        .HasColumnType("bigint");

                    b.Property<long>("CurrentTime_second")
                        .HasColumnType("bigint");

                    b.Property<byte>("DeparmentNumber")
                        .HasColumnType("tinyint");

                    b.Property<long>("DiscountPrice")
                        .HasColumnType("bigint");

                    b.Property<byte>("FunctionCode")
                        .HasColumnType("tinyint");

                    b.Property<long>("Itemcode")
                        .HasColumnType("bigint");

                    b.Property<bool>("Label")
                        .HasColumnType("bit");

                    b.Property<bool>("NegativeSale")
                        .HasColumnType("bit");

                    b.Property<bool>("NoVoid")
                        .HasColumnType("bit");

                    b.Property<bool>("Normal")
                        .HasColumnType("bit");

                    b.Property<bool>("Override")
                        .HasColumnType("bit");

                    b.Property<long>("PLUNumber")
                        .HasColumnType("bigint");

                    b.Property<long>("PackLenght")
                        .HasColumnType("bigint");

                    b.Property<short>("Pcs")
                        .HasColumnType("smallint");

                    b.Property<bool>("PluData")
                        .HasColumnType("bit");

                    b.Property<byte[]>("PluName")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte>("PluType")
                        .HasColumnType("tinyint");

                    b.Property<bool>("Prepack")
                        .HasColumnType("bit");

                    b.Property<bool>("PrepackData")
                        .HasColumnType("bit");

                    b.Property<short>("Qty")
                        .HasColumnType("smallint");

                    b.Property<bool>("Return")
                        .HasColumnType("bit");

                    b.Property<long>("SaleDate_day")
                        .HasColumnType("bigint");

                    b.Property<long>("SaleDate_month")
                        .HasColumnType("bigint");

                    b.Property<long>("SaleDate_year")
                        .HasColumnType("bigint");

                    b.Property<byte>("ScaleID")
                        .HasColumnType("tinyint");

                    b.Property<string>("ScaleIP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("ScaleLocked")
                        .HasColumnType("tinyint");

                    b.Property<long>("ScalePort")
                        .HasColumnType("bigint");

                    b.Property<byte>("ScaleServiceType")
                        .HasColumnType("tinyint");

                    b.Property<long>("ScaleTail")
                        .HasColumnType("bigint");

                    b.Property<long>("ScaleTransactioncounter")
                        .HasColumnType("bigint");

                    b.Property<bool>("SelfService")
                        .HasColumnType("bit");

                    b.Property<bool>("TicketData")
                        .HasColumnType("bit");

                    b.Property<short>("TicketNumber")
                        .HasColumnType("smallint");

                    b.Property<long>("TotalPrice")
                        .HasColumnType("bigint");

                    b.Property<string>("TraceCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UnitPrice")
                        .HasColumnType("bigint");

                    b.Property<bool>("Void")
                        .HasColumnType("bit");

                    b.Property<long>("Weight")
                        .HasColumnType("bigint");

                    b.Property<byte>("reserved")
                        .HasColumnType("tinyint");

                    b.HasKey("id");

                    b.ToTable("CasSalesData");
                });
#pragma warning restore 612, 618
        }
    }
}
