using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataInfrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "char(60)", unicode: false, fixedLength: true, maxLength: 60, nullable: false),
                    Cidade = table.Column<string>(type: "char(60)", unicode: false, fixedLength: true, maxLength: 60, nullable: false),
                    Rua = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "char(100)", unicode: false, fixedLength: true, maxLength: 100, nullable: false),
                    Profissao = table.Column<string>(type: "char(100)", unicode: false, fixedLength: true, maxLength: 100, nullable: false),
                    UF = table.Column<int>(type: "int", nullable: false),
                    CEP = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    CPF = table.Column<string>(type: "char(15)", unicode: false, fixedLength: true, maxLength: 15, nullable: false),
                    Telefone = table.Column<string>(type: "char(15)", unicode: false, fixedLength: true, maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "char(100)", unicode: false, fixedLength: true, maxLength: 100, nullable: false),
                    Senha = table.Column<string>(type: "char(68)", unicode: false, fixedLength: true, maxLength: 68, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Imagem = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Anuncios",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataAnuncio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Nome = table.Column<string>(type: "char(60)", unicode: false, fixedLength: true, maxLength: 60, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    EhDeficiente = table.Column<bool>(type: "bit", nullable: false),
                    EhCastrado = table.Column<bool>(type: "bit", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PessoaID = table.Column<int>(type: "int", nullable: false),
                    Sexo = table.Column<int>(type: "int", nullable: false),
                    Porte = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anuncios", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Anuncios_Pessoas_PessoaID",
                        column: x => x.PessoaID,
                        principalTable: "Pessoas",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "AnuncioInteresses",
                columns: table => new
                {
                    AdotanteID = table.Column<int>(type: "int", nullable: false),
                    AnuncioID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnuncioInteresses", x => new { x.AdotanteID, x.AnuncioID });
                    table.ForeignKey(
                        name: "FK_AnuncioInteresses_AdotanteID",
                        column: x => x.AdotanteID,
                        principalTable: "Pessoas",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AnuncioInteresses_AnuncioID",
                        column: x => x.AnuncioID,
                        principalTable: "Anuncios",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Mensagens",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RemetenteID = table.Column<int>(type: "int", nullable: false),
                    DestinatarioID = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Corpo = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    AnuncioID = table.Column<int>(type: "int", nullable: true),
                    AnuncioID1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mensagens", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Mensagens_Anuncios_AnuncioID",
                        column: x => x.AnuncioID,
                        principalTable: "Anuncios",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Mensagens_Anuncios_AnuncioID1",
                        column: x => x.AnuncioID1,
                        principalTable: "Anuncios",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Mensagens_Pessoas_DestinatarioID",
                        column: x => x.DestinatarioID,
                        principalTable: "Pessoas",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Mensagens_Pessoas_RemetenteID",
                        column: x => x.RemetenteID,
                        principalTable: "Pessoas",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnuncioInteresses_AnuncioID",
                table: "AnuncioInteresses",
                column: "AnuncioID");

            migrationBuilder.CreateIndex(
                name: "IX_Anuncios_PessoaID",
                table: "Anuncios",
                column: "PessoaID");

            migrationBuilder.CreateIndex(
                name: "IX_Mensagens_AnuncioID",
                table: "Mensagens",
                column: "AnuncioID");

            migrationBuilder.CreateIndex(
                name: "IX_Mensagens_AnuncioID1",
                table: "Mensagens",
                column: "AnuncioID1");

            migrationBuilder.CreateIndex(
                name: "IX_Mensagens_DestinatarioID",
                table: "Mensagens",
                column: "DestinatarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Mensagens_RemetenteID",
                table: "Mensagens",
                column: "RemetenteID");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_CPF",
                table: "Pessoas",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_Email",
                table: "Pessoas",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnuncioInteresses");

            migrationBuilder.DropTable(
                name: "Mensagens");

            migrationBuilder.DropTable(
                name: "Anuncios");

            migrationBuilder.DropTable(
                name: "Pessoas");
        }
    }
}
