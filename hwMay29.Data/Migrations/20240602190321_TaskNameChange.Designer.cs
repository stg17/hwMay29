﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using hwMay29.Data;

#nullable disable

namespace hwMay29.Data.Migrations
{
    [DbContext(typeof(TaskDataContext))]
    [Migration("20240602190321_TaskNameChange")]
    partial class TaskNameChange
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("hwMay29.Data.TaskItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("TaskName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserDoingTaskId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserDoingTaskId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("hwMay29.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("hwMay29.Data.TaskItem", b =>
                {
                    b.HasOne("hwMay29.Data.User", "UserDoingTask")
                        .WithMany()
                        .HasForeignKey("UserDoingTaskId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("UserDoingTask");
                });
#pragma warning restore 612, 618
        }
    }
}