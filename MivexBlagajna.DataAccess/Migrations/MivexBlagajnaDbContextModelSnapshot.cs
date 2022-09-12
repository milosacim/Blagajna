﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MivexBlagajna.DataAccess;

#nullable disable

namespace MivexBlagajna.DataAccess.Migrations
{
    [DbContext(typeof(MivexBlagajnaDbContext))]
    partial class MivexBlagajnaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MivexBlagajna.Data.Models.Komitent", b =>
                {
                    b.Property<int>("Komitent_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Komitent_Id"), 1L, 1);

                    b.Property<string>("Adresa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("FizickoLice")
                        .HasColumnType("bit");

                    b.Property<string>("Ime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Jmbg")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KontaktOsoba")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaticniBroj")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mesto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naziv")
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("Naziv2")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

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

                    b.HasKey("Komitent_Id");

                    b.ToTable("Komitenti");

                    b.HasData(
                        new
                        {
                            Komitent_Id = 1,
                            Adresa = "Bulevar Oslobodilaca Cacka 105b",
                            FizickoLice = false,
                            KontaktOsoba = "Ivan Čvorović",
                            MaticniBroj = "17123629",
                            Mesto = "Cacak",
                            Naziv = "MIVEX D.O.O. za trgovinu transport i usluge",
                            Naziv2 = "Mivex DOO",
                            Pib = "100898155",
                            PostanskiBroj = "32102",
                            PravnoLice = true,
                            Sifra = 1,
                            Telefon = "032-310-180"
                        },
                        new
                        {
                            Komitent_Id = 2,
                            Adresa = "Bulevar Oslobodilaca Cacka 105b",
                            FizickoLice = false,
                            KontaktOsoba = "Dejan Čvorović",
                            MaticniBroj = "20368578",
                            Mesto = "Cacak",
                            Naziv = "MIVEX LOGISTICS D.O.O. za trgovinu transport i usluge ",
                            Naziv2 = "Mivex DOO",
                            Pib = "105441444",
                            PostanskiBroj = "32102",
                            PravnoLice = true,
                            Sifra = 2,
                            Telefon = "032-310-180"
                        },
                        new
                        {
                            Komitent_Id = 3,
                            Adresa = "Bulevar Oslobodilaca Cacka 105b",
                            FizickoLice = false,
                            KontaktOsoba = "Ivan Čvorović",
                            MaticniBroj = "07167237",
                            Mesto = "Cacak",
                            Naziv = "ČAČANKA D.O.O. za proizvodnju i promet alk. i bezal. pića",
                            Naziv2 = "Mivex DOO",
                            Pib = "104792570",
                            PostanskiBroj = "32102",
                            PravnoLice = true,
                            Sifra = 3,
                            Telefon = "032-310-180"
                        },
                        new
                        {
                            Komitent_Id = 4,
                            Adresa = "Bulevar Oslobodilaca Cacka 105b",
                            FizickoLice = true,
                            Ime = "Ivan",
                            Jmbg = "12345678910123",
                            Mesto = "Cacak",
                            PravnoLice = false,
                            Prezime = "Čvorović",
                            Sifra = 4,
                            Telefon = "064/8281-500"
                        },
                        new
                        {
                            Komitent_Id = 5,
                            Adresa = "Milenka Nikšića 50",
                            FizickoLice = false,
                            KontaktOsoba = "Dragan Mladenović",
                            Mesto = "Cacak",
                            Naziv = "MP 18 - Maloprodaja Ljubić",
                            Naziv2 = "Maloprodaja Ljubić",
                            PostanskiBroj = "32000",
                            PravnoLice = true,
                            Sifra = 5
                        });
                });

            modelBuilder.Entity("MivexBlagajna.Data.Models.MestoTroska", b =>
                {
                    b.Property<int>("MestoTroska_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MestoTroska_Id"), 1L, 1);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Sifra")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MestoTroska_Id");

                    b.ToTable("MestaTroska");

                    b.HasData(
                        new
                        {
                            MestoTroska_Id = 1,
                            Naziv = "Veleprodaja",
                            Sifra = "01"
                        },
                        new
                        {
                            MestoTroska_Id = 2,
                            Naziv = "Maloprodaja",
                            Sifra = "02"
                        },
                        new
                        {
                            MestoTroska_Id = 3,
                            Naziv = "Usluge",
                            Sifra = "03"
                        },
                        new
                        {
                            MestoTroska_Id = 4,
                            Naziv = "Osnivači",
                            Sifra = "04"
                        },
                        new
                        {
                            MestoTroska_Id = 5,
                            Naziv = "Cer",
                            Sifra = "05"
                        },
                        new
                        {
                            MestoTroska_Id = 6,
                            Naziv = "Objekti",
                            Sifra = "06"
                        },
                        new
                        {
                            MestoTroska_Id = 7,
                            Naziv = "Restoran",
                            Sifra = "07"
                        },
                        new
                        {
                            MestoTroska_Id = 8,
                            Naziv = "Čačanka",
                            Sifra = "08"
                        });
                });

            modelBuilder.Entity("MivexBlagajna.Data.Models.NosilacTroska", b =>
                {
                    b.Property<int>("NosilacTroska_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NosilacTroska_id"), 1L, 1);

                    b.Property<int>("MestoTroska_Id")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("Nivo")
                        .HasColumnType("int");

                    b.Property<string>("Sifra")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NosilacTroska_id");

                    b.HasIndex("MestoTroska_Id");

                    b.ToTable("NosiociTroska");

                    b.HasData(
                        new
                        {
                            NosilacTroska_id = 1,
                            MestoTroska_Id = 1,
                            Naziv = "Komercijala",
                            Nivo = 1,
                            Sifra = "01.01"
                        });
                });

            modelBuilder.Entity("MivexBlagajna.Data.Models.NosilacTroska", b =>
                {
                    b.HasOne("MivexBlagajna.Data.Models.MestoTroska", "MestoTroska")
                        .WithMany("NosiociTroska")
                        .HasForeignKey("MestoTroska_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MestoTroska");
                });

            modelBuilder.Entity("MivexBlagajna.Data.Models.MestoTroska", b =>
                {
                    b.Navigation("NosiociTroska");
                });
#pragma warning restore 612, 618
        }
    }
}
