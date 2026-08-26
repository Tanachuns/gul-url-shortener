using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShortener.Migrations
{
    /// <inheritdoc />
    public partial class updatebuilder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LongUrl",
                table: "Links",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Links_Code",
                table: "Links",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Links_Code",
                table: "Links");

            migrationBuilder.AlterColumn<string>(
                name: "LongUrl",
                table: "Links",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
