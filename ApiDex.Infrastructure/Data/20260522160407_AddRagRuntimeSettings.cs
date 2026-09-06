using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiDex.Infrastructure.Data
{
    /// <inheritdoc />
    public partial class AddRagRuntimeSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContextMode",
                schema: "ApiDex",
                table: "RagSettings",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "Compact");

            migrationBuilder.AddColumn<int>(
                name: "TopKChunks",
                schema: "ApiDex",
                table: "RagSettings",
                type: "integer",
                nullable: false,
                defaultValue: 5);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContextMode",
                schema: "ApiDex",
                table: "RagSettings");

            migrationBuilder.DropColumn(
                name: "TopKChunks",
                schema: "ApiDex",
                table: "RagSettings");
        }
    }
}
