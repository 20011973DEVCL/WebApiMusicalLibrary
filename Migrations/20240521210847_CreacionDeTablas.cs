using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiMusicalLibrary.Migrations
{
    /// <inheritdoc />
    public partial class CreacionDeTablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    IdCountry = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.IdCountry);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    IdGenre = table.Column<int>(type: "int", nullable: false),
                    GenreName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.IdGenre);
                });

            migrationBuilder.CreateTable(
                name: "MenuOptions",
                columns: table => new
                {
                    IdOption = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    OptionOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuOptions", x => x.IdOption);
                });

            migrationBuilder.CreateTable(
                name: "UserModel",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModel", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "BandSinger",
                columns: table => new
                {
                    IdBandSinger = table.Column<int>(type: "int", nullable: false),
                    BandSingerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Members = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCountry = table.Column<string>(type: "nvarchar(3)", nullable: false),
                    StarDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BandSinger", x => x.IdBandSinger);
                    table.ForeignKey(
                        name: "FK_BandSinger_Country_IdCountry",
                        column: x => x.IdCountry,
                        principalTable: "Country",
                        principalColumn: "IdCountry",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Albun",
                columns: table => new
                {
                    IdAlbun = table.Column<int>(type: "int", nullable: false),
                    AlbunName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlbunYear = table.Column<int>(type: "int", nullable: true),
                    IdBandSinger = table.Column<int>(type: "int", nullable: false),
                    IdGenre = table.Column<int>(type: "int", nullable: false),
                    Cover = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albun", x => x.IdAlbun);
                    table.ForeignKey(
                        name: "FK_Albun_BandSinger_IdBandSinger",
                        column: x => x.IdBandSinger,
                        principalTable: "BandSinger",
                        principalColumn: "IdBandSinger",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Albun_Genre_IdGenre",
                        column: x => x.IdGenre,
                        principalTable: "Genre",
                        principalColumn: "IdGenre",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    IdSong = table.Column<int>(type: "int", nullable: false),
                    IdAlbun = table.Column<int>(type: "int", nullable: false),
                    Track = table.Column<int>(type: "int", nullable: false),
                    SongName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Disc = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.IdSong);
                    table.ForeignKey(
                        name: "FK_Songs_Albun_IdAlbun",
                        column: x => x.IdAlbun,
                        principalTable: "Albun",
                        principalColumn: "IdAlbun",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albun_IdBandSinger",
                table: "Albun",
                column: "IdBandSinger");

            migrationBuilder.CreateIndex(
                name: "IX_Albun_IdGenre",
                table: "Albun",
                column: "IdGenre");

            migrationBuilder.CreateIndex(
                name: "IX_BandSinger_IdCountry",
                table: "BandSinger",
                column: "IdCountry");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_IdAlbun",
                table: "Songs",
                column: "IdAlbun");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuOptions");

            migrationBuilder.DropTable(
                name: "Songs");

            migrationBuilder.DropTable(
                name: "UserModel");

            migrationBuilder.DropTable(
                name: "Albun");

            migrationBuilder.DropTable(
                name: "BandSinger");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
