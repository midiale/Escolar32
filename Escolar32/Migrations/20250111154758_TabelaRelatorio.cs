using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Escolar32.Migrations
{
    /// <inheritdoc />
    public partial class TabelaRelatorio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Relatorio",
                columns: table => new
                {
                    RelatorioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataRelatorio = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relatorio", x => x.RelatorioId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Relatorio");
        }
    }
}
