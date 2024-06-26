﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NNTracker.Data;

#nullable disable

namespace NNTracker.Migrations
{
    [DbContext(typeof(ApplicationDBcontext))]
    partial class ApplicationDBcontextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NNTracker.Models.Account", b =>
                {
                    b.Property<int>("Account_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Account_Id"));

                    b.Property<string>("Account_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Account_Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Initial_Budget")
                        .HasColumnType("float");

                    b.HasKey("Account_Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("NNTracker.Models.Category", b =>
                {
                    b.Property<int>("Category_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Category_Id"));

                    b.Property<string>("Category_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("budget")
                        .HasColumnType("float");

                    b.HasKey("Category_Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("NNTracker.Models.Transaction", b =>
                {
                    b.Property<int>("Transaction_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Transaction_Id"));

                    b.Property<int>("Account_Id")
                        .HasColumnType("int");

                    b.Property<int>("Category_Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Transaction_Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Transaction_Value")
                        .HasColumnType("int");

                    b.HasKey("Transaction_Id");

                    b.HasIndex("Account_Id");

                    b.HasIndex("Category_Id");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("NNTracker.Models.Transaction", b =>
                {
                    b.HasOne("NNTracker.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("Account_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NNTracker.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("Category_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
