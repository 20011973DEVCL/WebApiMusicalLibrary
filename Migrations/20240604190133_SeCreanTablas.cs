using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiMusicalLibrary.Migrations
{
    /// <inheritdoc />
    public partial class SeCreanTablas : Migration
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
                name: "MusicGenre",
                columns: table => new
                {
                    IdMusicGenre = table.Column<int>(type: "int", nullable: false),
                    GenreName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicGenre", x => x.IdMusicGenre);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "Singer",
                columns: table => new
                {
                    IdSinger = table.Column<int>(type: "int", nullable: false),
                    SingerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Members = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCountry = table.Column<string>(type: "nvarchar(3)", nullable: false),
                    StarDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Singer", x => x.IdSinger);
                    table.ForeignKey(
                        name: "FK_Singer_Country_IdCountry",
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
                    IdSinger = table.Column<int>(type: "int", nullable: false),
                    IdMusicGenre = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albun", x => x.IdAlbun);
                    table.ForeignKey(
                        name: "FK_Albun_MusicGenre_IdMusicGenre",
                        column: x => x.IdMusicGenre,
                        principalTable: "MusicGenre",
                        principalColumn: "IdMusicGenre",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Albun_Singer_IdSinger",
                        column: x => x.IdSinger,
                        principalTable: "Singer",
                        principalColumn: "IdSinger",
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
                name: "IX_Albun_IdMusicGenre",
                table: "Albun",
                column: "IdMusicGenre");

            migrationBuilder.CreateIndex(
                name: "IX_Albun_IdSinger",
                table: "Albun",
                column: "IdSinger");

            migrationBuilder.CreateIndex(
                name: "IX_Singer_IdCountry",
                table: "Singer",
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
                name: "User");

            migrationBuilder.DropTable(
                name: "Albun");

            migrationBuilder.DropTable(
                name: "MusicGenre");

            migrationBuilder.DropTable(
                name: "Singer");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
