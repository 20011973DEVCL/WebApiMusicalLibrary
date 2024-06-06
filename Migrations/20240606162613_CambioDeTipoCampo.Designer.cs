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
    [Migration("20240606162613_CambioDeTipoCampo")]
    partial class CambioDeTipoCampo
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

                    b.Property<int?>("IdMusicGenre")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("IdSinger")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("PublishYear")
                        .HasColumnType("int");

                    b.HasKey("IdAlbun");

                    b.HasIndex("IdMusicGenre");

                    b.HasIndex("IdSinger");

                    b.ToTable("Albun");
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

            modelBuilder.Entity("WebApiMusicalLibrary.Models.MusicGenre", b =>
                {
                    b.Property<int>("IdMusicGenre")
                        .HasColumnType("int");

                    b.Property<string>("GenreName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("IdMusicGenre");

                    b.ToTable("MusicGenre");
                });

            modelBuilder.Entity("WebApiMusicalLibrary.Models.Sales.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("OrderId");

                    b.HasIndex("Username");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("WebApiMusicalLibrary.Models.Sales.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderItemId"));

                    b.Property<int>("IdAlbun")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderItemId");

                    b.HasIndex("IdAlbun");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("WebApiMusicalLibrary.Models.Singer", b =>
                {
                    b.Property<int>("IdSinger")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IdCountry")
                        .IsRequired()
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("Members")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SingerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("StarDate")
                        .HasColumnType("datetime2");

                    b.HasKey("IdSinger");

                    b.HasIndex("IdCountry");

                    b.ToTable("Singer");
                });

            modelBuilder.Entity("WebApiMusicalLibrary.Models.Songs", b =>
                {
                    b.Property<int>("IdSong")
                        .HasColumnType("int");

                    b.Property<int?>("Disc")
                        .HasColumnType("int");

                    b.Property<int>("IdAlbun")
                        .HasColumnType("int");

                    b.Property<int>("PublishYear")
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

            modelBuilder.Entity("WebApiMusicalLibrary.Models.User", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Username");

                    b.ToTable("User");
                });

            modelBuilder.Entity("WebApiMusicalLibrary.Models.Albun", b =>
                {
                    b.HasOne("WebApiMusicalLibrary.Models.MusicGenre", "MusicGenre")
                        .WithMany()
                        .HasForeignKey("IdMusicGenre")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiMusicalLibrary.Models.Singer", "Singer")
                        .WithMany()
                        .HasForeignKey("IdSinger")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MusicGenre");

                    b.Navigation("Singer");
                });

            modelBuilder.Entity("WebApiMusicalLibrary.Models.Sales.Order", b =>
                {
                    b.HasOne("WebApiMusicalLibrary.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("Username")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApiMusicalLibrary.Models.Sales.OrderItem", b =>
                {
                    b.HasOne("WebApiMusicalLibrary.Models.Albun", "Album")
                        .WithMany()
                        .HasForeignKey("IdAlbun")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiMusicalLibrary.Models.Sales.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("WebApiMusicalLibrary.Models.Singer", b =>
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

            modelBuilder.Entity("WebApiMusicalLibrary.Models.Sales.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
