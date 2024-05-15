﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApiMusicalLibrary.Data;

#nullable disable

namespace WebApiMusicalLibrary.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240515202551_SeCreaNvoCampoOrdenEnMenuOptions")]
    partial class SeCreaNvoCampoOrdenEnMenuOptions
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApiMusicalLibrary.Models.Albun", b =>
                {
                    b.Property<int>("IdAlbun")
                        .HasColumnType("int");

                    b.Property<string>("AlbunName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("AlbunYear")
                        .HasColumnType("int");

                    b.Property<byte[]>("Cover")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("IdBandSinger")
                        .HasColumnType("int");

                    b.Property<int?>("IdGenre")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("IdAlbun");

                    b.HasIndex("IdBandSinger");

                    b.HasIndex("IdGenre");

                    b.ToTable("Albun");
                });

            modelBuilder.Entity("WebApiMusicalLibrary.Models.BandSinger", b =>
                {
                    b.Property<int>("IdBandSinger")
                        .HasColumnType("int");

                    b.Property<string>("BandSingerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IdCountry")
                        .IsRequired()
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("Members")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("StarDate")
                        .HasColumnType("datetime2");

                    b.HasKey("IdBandSinger");

                    b.HasIndex("IdCountry");

                    b.ToTable("BandSinger");
                });

            modelBuilder.Entity("WebApiMusicalLibrary.Models.Country", b =>
                {
                    b.Property<string>("IdCountry")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("IdCountry");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("WebApiMusicalLibrary.Models.Genre", b =>
                {
                    b.Property<int>("IdGenre")
                        .HasColumnType("int");

                    b.Property<string>("GenreName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("IdGenre");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("WebApiMusicalLibrary.Models.Login.MenuOptions", b =>
                {
                    b.Property<string>("IdOption")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<int>("OptionOrder")
                        .HasColumnType("int");

                    b.HasKey("IdOption");

                    b.ToTable("MenuOptions");
                });

            modelBuilder.Entity("WebApiMusicalLibrary.Models.Songs", b =>
                {
                    b.Property<int>("IdSong")
                        .HasColumnType("int");

                    b.Property<int?>("Disc")
                        .HasColumnType("int");

                    b.Property<int>("IdAlbun")
                        .HasColumnType("int");

                    b.Property<string>("SongName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("Track")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("IdSong");

                    b.HasIndex("IdAlbun");

                    b.ToTable("Songs");
                });

            modelBuilder.Entity("WebApiMusicalLibrary.Models.Albun", b =>
                {
                    b.HasOne("WebApiMusicalLibrary.Models.BandSinger", "BandSinger")
                        .WithMany()
                        .HasForeignKey("IdBandSinger")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiMusicalLibrary.Models.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("IdGenre")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BandSinger");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("WebApiMusicalLibrary.Models.BandSinger", b =>
                {
                    b.HasOne("WebApiMusicalLibrary.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("IdCountry")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("WebApiMusicalLibrary.Models.Songs", b =>
                {
                    b.HasOne("WebApiMusicalLibrary.Models.Albun", "Albun")
                        .WithMany()
                        .HasForeignKey("IdAlbun")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Albun");
                });
#pragma warning restore 612, 618
        }
    }
}
