using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class modifyTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Coode",
                table: "regions",
                newName: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Code",
                table: "regions",
                newName: "Coode");
        }
    }
}
