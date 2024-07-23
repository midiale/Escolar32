using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Escolar32.Migrations
{
    /// <inheritdoc />
    public partial class RelacionaTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FinanceiroId",
                table: "Alunos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_FinanceiroId",
                table: "Alunos",
                column: "FinanceiroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alunos_Financeiros_FinanceiroId",
                table: "Alunos",
                column: "FinanceiroId",
                principalTable: "Financeiros",
                principalColumn: "FinanceiroId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_Financeiros_FinanceiroId",
                table: "Alunos");

            migrationBuilder.DropIndex(
                name: "IX_Alunos_FinanceiroId",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "FinanceiroId",
                table: "Alunos");
        }
    }
}
