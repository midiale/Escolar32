using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Escolar32.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaFinanceiro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Financeiros",
                columns: table => new
                {
                    FinanceiroId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SomaAluno = table.Column<int>(type: "int", nullable: false),
                    SomaMes = table.Column<int>(type: "int", nullable: false),
                    SomaTotal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Financeiros", x => x.FinanceiroId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Financeiros");
        }
    }
}
