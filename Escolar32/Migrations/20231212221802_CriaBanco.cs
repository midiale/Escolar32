using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Escolar32.Migrations
{
    /// <inheritdoc />
    public partial class CriaBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bairros",
                columns: table => new
                {
                    BairroId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BairroNome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bairros", x => x.BairroId);
                });

            migrationBuilder.CreateTable(
                name: "Escolas",
                columns: table => new
                {
                    EscolaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EscolaNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escolas", x => x.EscolaId);
                });

            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    AlunoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NomeUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataNasc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Mae = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Pai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Cep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroCasa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Complemento = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefone1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefone3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VanAnterior = table.Column<bool>(type: "bit", nullable: false),
                    QualEscolar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EscolaId = table.Column<int>(type: "int", nullable: false),
                    Serie = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Periodo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RespFinan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Rg = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Profissao = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FirmaRec = table.Column<bool>(type: "bit", nullable: false),
                    Cartorio = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    InicioPgto = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FimPgto = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorParcela = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    QtdeParcelas = table.Column<int>(type: "int", nullable: false),
                    TotalContrato = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExAluno = table.Column<bool>(type: "bit", nullable: false),
                    Jan = table.Column<bool>(type: "bit", nullable: false),
                    Fev = table.Column<bool>(type: "bit", nullable: false),
                    Mar = table.Column<bool>(type: "bit", nullable: false),
                    Abr = table.Column<bool>(type: "bit", nullable: false),
                    Mai = table.Column<bool>(type: "bit", nullable: false),
                    Jun = table.Column<bool>(type: "bit", nullable: false),
                    Jul = table.Column<bool>(type: "bit", nullable: false),
                    Ago = table.Column<bool>(type: "bit", nullable: false),
                    Set = table.Column<bool>(type: "bit", nullable: false),
                    Out = table.Column<bool>(type: "bit", nullable: false),
                    Nov = table.Column<bool>(type: "bit", nullable: false),
                    Dez = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.AlunoId);
                    table.ForeignKey(
                        name: "FK_Alunos_Escolas_EscolaId",
                        column: x => x.EscolaId,
                        principalTable: "Escolas",
                        principalColumn: "EscolaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_EscolaId",
                table: "Alunos",
                column: "EscolaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Bairros");

            migrationBuilder.DropTable(
                name: "Escolas");
        }
    }
}
