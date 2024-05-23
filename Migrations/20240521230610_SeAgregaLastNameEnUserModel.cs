using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiMusicalLibrary.Migrations
{
    /// <inheritdoc />
    public partial class SeAgregaLastNameEnUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "UserModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastName",
                table: "UserModel");
        }
    }
}
