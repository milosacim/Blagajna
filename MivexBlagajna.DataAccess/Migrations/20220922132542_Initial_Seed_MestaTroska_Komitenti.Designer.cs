﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MivexBlagajna.DataAccess;

#nullable disable

namespace MivexBlagajna.DataAccess.Migrations
{
    [DbContext(typeof(MivexBlagajnaDbContext))]
    [Migration("20220922132542_Initial_Seed_MestaTroska_Komitenti")]
    partial class Initial_Seed_MestaTroska_Komitenti
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MivexBlagajna.Data.Models.Komitent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Adresa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("FizickoLice")
                        .HasColumnType("bit");

                    b.Property<string>("Ime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Jmbg")
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("KontaktOsoba")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaticniBroj")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mesto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MestoTroska_id")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Naziv2")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Pib")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostanskiBroj")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<bool>("PravnoLice")
                        .HasColumnType("bit");

                    b.Property<string>("Prezime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sifra")
                        .HasColumnType("int");

                    b.Property<string>("Telefon")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MestoTroska_id");

                    b.ToTable("Komitent", (string)null);
                });

            modelBuilder.Entity("MivexBlagajna.Data.Models.Konto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Konto", (string)null);
                });

            modelBuilder.Entity("MivexBlagajna.Data.Models.MestoTroska", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("NadredjenoMesto_Id")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("Nivo")
                        .HasColumnType("int");

                    b.Property<string>("Sifra")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MestaTroska", (string)null);
                });

            modelBuilder.Entity("MivexBlagajna.Data.Models.Transakcija", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Isplata")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,5)")
                        .HasDefaultValue(0.0000m);

                    b.Property<int>("Komitent_Id")
                        .HasColumnType("int");

                    b.Property<int>("Konto_Id")
                        .HasColumnType("int");

                    b.Property<int>("MestoTroska_Id")
                        .HasColumnType("int");

                    b.Property<string>("Nalog")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Neopravndan")
                        .HasColumnType("bit");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Opravdan")
                        .HasColumnType("bit");

                    b.Property<decimal>("Uplata")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,5)")
                        .HasDefaultValue(0.0000m);

                    b.HasKey("Id");

                    b.HasIndex("Komitent_Id");

                    b.HasIndex("Konto_Id");

                    b.HasIndex("MestoTroska_Id");

                    b.ToTable("Transakcije", (string)null);
                });

            modelBuilder.Entity("MivexBlagajna.Data.Models.Komitent", b =>
                {
                    b.HasOne("MivexBlagajna.Data.Models.MestoTroska", "MestoTroska")
                        .WithMany("Komitenti")
                        .HasForeignKey("MestoTroska_id")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("MestoTroska");
                });

            modelBuilder.Entity("MivexBlagajna.Data.Models.Transakcija", b =>
                {
                    b.HasOne("MivexBlagajna.Data.Models.Komitent", "Komitent")
                        .WithMany("Transakcije")
                        .HasForeignKey("Komitent_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MivexBlagajna.Data.Models.Konto", "Konta")
                        .WithMany("Transakcije")
                        .HasForeignKey("Konto_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MivexBlagajna.Data.Models.MestoTroska", "MestoTroska")
                        .WithMany("Transakcije")
                        .HasForeignKey("MestoTroska_Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Komitent");

                    b.Navigation("Konta");

                    b.Navigation("MestoTroska");
                });

            modelBuilder.Entity("MivexBlagajna.Data.Models.Komitent", b =>
                {
                    b.Navigation("Transakcije");
                });

            modelBuilder.Entity("MivexBlagajna.Data.Models.Konto", b =>
                {
                    b.Navigation("Transakcije");
                });

            modelBuilder.Entity("MivexBlagajna.Data.Models.MestoTroska", b =>
                {
                    b.Navigation("Komitenti");

                    b.Navigation("Transakcije");
                });
#pragma warning restore 612, 618
        }
    }
}
