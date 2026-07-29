using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDIITechincalInterview.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTandaTangan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TandaTanganDigital",
                table: "Biodatas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TandaTanganDigital",
                table: "Biodatas",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
