﻿// <auto-generated />
using System;
using Hastane.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Hastane.Migrations
{
    [DbContext(typeof(HospitalDbContext))]
    [Migration("20240925035418_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Hastane.Model.Brans", b =>
                {
                    b.Property<int>("BransId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BransId"));

                    b.Property<string>("BransAd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BransId");

                    b.ToTable("Branslar");
                });

            modelBuilder.Entity("Hastane.Model.Doktor", b =>
                {
                    b.Property<int>("DoktorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DoktorId"));

                    b.Property<int>("BransId")
                        .HasColumnType("int");

                    b.Property<string>("DoktorAd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DoktorSoyad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DoktorTC")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sifre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DoktorId");

                    b.HasIndex("BransId");

                    b.ToTable("Doktorlar");
                });

            modelBuilder.Entity("Hastane.Model.Hasta", b =>
                {
                    b.Property<int>("HastaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HastaId"));

                    b.Property<string>("Adres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DogumTarihi")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HastaAd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HastaSoyad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sifre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TcKimlikNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HastaId");

                    b.ToTable("Hastalar");
                });

            modelBuilder.Entity("Hastane.Model.Randevu", b =>
                {
                    b.Property<int>("RandevuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RandevuId"));

                    b.Property<int>("DoktorId")
                        .HasColumnType("int");

                    b.Property<int>("HastaId")
                        .HasColumnType("int");

                    b.Property<bool>("OnayDurumu")
                        .HasColumnType("bit");

                    b.Property<TimeSpan>("RandevuSaati")
                        .HasColumnType("time");

                    b.Property<DateTime>("RandevuTarihi")
                        .HasColumnType("datetime2");

                    b.HasKey("RandevuId");

                    b.HasIndex("DoktorId");

                    b.HasIndex("HastaId");

                    b.ToTable("Randevular");
                });

            modelBuilder.Entity("Hastane.Model.RandevuMusait", b =>
                {
                    b.Property<int>("RandevuMusaitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RandevuMusaitId"));

                    b.Property<int>("DoktorId")
                        .HasColumnType("int");

                    b.Property<bool>("Durum")
                        .HasColumnType("bit");

                    b.Property<TimeSpan>("MusaitSaat")
                        .HasColumnType("time");

                    b.Property<DateTime>("MusaitTarih")
                        .HasColumnType("datetime2");

                    b.HasKey("RandevuMusaitId");

                    b.HasIndex("DoktorId");

                    b.ToTable("RandevuMusait");
                });

            modelBuilder.Entity("Hastane.Model.Sekreter", b =>
                {
                    b.Property<int>("Sekreterid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Sekreterid"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SekreterTC")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sekreterad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sekretersoyad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sifre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Sekreterid");

                    b.ToTable("Sekreterler");
                });

            modelBuilder.Entity("Hastane.Model.Doktor", b =>
                {
                    b.HasOne("Hastane.Model.Brans", "Brans")
                        .WithMany("Doktorlar")
                        .HasForeignKey("BransId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brans");
                });

            modelBuilder.Entity("Hastane.Model.Randevu", b =>
                {
                    b.HasOne("Hastane.Model.Doktor", "Doktor")
                        .WithMany("Randevular")
                        .HasForeignKey("DoktorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hastane.Model.Hasta", "Hasta")
                        .WithMany("Randevular")
                        .HasForeignKey("HastaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doktor");

                    b.Navigation("Hasta");
                });

            modelBuilder.Entity("Hastane.Model.RandevuMusait", b =>
                {
                    b.HasOne("Hastane.Model.Doktor", "Doktor")
                        .WithMany("RandevuMusaits")
                        .HasForeignKey("DoktorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doktor");
                });

            modelBuilder.Entity("Hastane.Model.Brans", b =>
                {
                    b.Navigation("Doktorlar");
                });

            modelBuilder.Entity("Hastane.Model.Doktor", b =>
                {
                    b.Navigation("RandevuMusaits");

                    b.Navigation("Randevular");
                });

            modelBuilder.Entity("Hastane.Model.Hasta", b =>
                {
                    b.Navigation("Randevular");
                });
#pragma warning restore 612, 618
        }
    }
}
