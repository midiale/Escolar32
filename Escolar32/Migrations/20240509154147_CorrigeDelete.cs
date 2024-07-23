using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Escolar32.Migrations
{
    /// <inheritdoc />
    public partial class CorrigeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_Escolas_EscolaId",
                table: "Alunos");

            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_Receitas_ReceitaId",
                table: "Alunos");

            migrationBuilder.DropIndex(
                name: "IX_Alunos_ReceitaId",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "ReceitaId",
                table: "Alunos");

            migrationBuilder.AlterColumn<int>(
                name: "EscolaId",
                table: "Alunos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Alunos_Escolas_EscolaId",
                table: "Alunos",
                column: "EscolaId",
                principalTable: "Escolas",
                principalColumn: "EscolaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_Escolas_EscolaId",
                table: "Alunos");

            migrationBuilder.AlterColumn<int>(
                name: "EscolaId",
                table: "Alunos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReceitaId",
                table: "Alunos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_ReceitaId",
                table: "Alunos",
                column: "ReceitaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alunos_Escolas_EscolaId",
                table: "Alunos",
                column: "EscolaId",
                principalTable: "Escolas",
                principalColumn: "EscolaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Alunos_Receitas_ReceitaId",
                table: "Alunos",
                column: "ReceitaId",
                principalTable: "Receitas",
                principalColumn: "ReceitaId");
        }
    }
}
