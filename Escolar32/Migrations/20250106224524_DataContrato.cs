using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Escolar32.Migrations
{
    /// <inheritdoc />
    public partial class DataContrato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataContrato",
                table: "Alunos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataContrato",
                table: "Alunos");
        }
    }
}
