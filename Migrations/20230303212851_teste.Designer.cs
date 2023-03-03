﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api_flutter.Data;

#nullable disable

namespace api_flutter_azure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230303212851_teste")]
    partial class teste
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("api_flutter.Models.Journal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Journals");
                });

            modelBuilder.Entity("api_flutter.Models.Tarefa", b =>
                {
                    b.Property<string>("Titulo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Dificuldade")
                        .HasColumnType("int");

                    b.Property<string>("UrlFoto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Titulo");

                    b.ToTable("Tarefas");
                });
#pragma warning restore 612, 618
        }
    }
}
