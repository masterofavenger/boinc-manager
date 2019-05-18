﻿// <auto-generated />
using BoincManagerWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BoincManagerWeb.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190518222558_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0-preview5.19227.1");

            modelBuilder.Entity("BoincManager.Models.Host", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("IpAddress");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<int>("Port");

                    b.HasKey("Id");

                    b.ToTable("Host");
                });
#pragma warning restore 612, 618
        }
    }
}
