using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebKursach.Migrations
{
    /// <inheritdoc />
    public partial class rteyjhgj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Position",
                table: "Cars",
                newName: "CarPosition");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CarPosition",
                table: "Cars",
                newName: "Position");
        }
    }
}
