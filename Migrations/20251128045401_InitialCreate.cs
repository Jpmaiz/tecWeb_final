using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace final.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Celdas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Numero = table.Column<int>(type: "integer", nullable: false),
                    Pabellon = table.Column<string>(type: "text", nullable: false),
                    Capacidad = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Celdas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Guardias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    CI = table.Column<string>(type: "text", nullable: false),
                    Turno = table.Column<string>(type: "text", nullable: false),
                    Rango = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guardias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    CI = table.Column<string>(type: "text", nullable: false),
                    Correo = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reclusos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    CI = table.Column<string>(type: "text", nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CondenaAnios = table.Column<int>(type: "integer", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    CeldaId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reclusos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reclusos_Celdas_CeldaId",
                        column: x => x.CeldaId,
                        principalTable: "Celdas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reclusos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expedientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    DelitoPrincipal = table.Column<string>(type: "text", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReclusoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expedientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expedientes_Reclusos_ReclusoId",
                        column: x => x.ReclusoId,
                        principalTable: "Reclusos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expedientes_ReclusoId",
                table: "Expedientes",
                column: "ReclusoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reclusos_CeldaId",
                table: "Reclusos",
                column: "CeldaId");

            migrationBuilder.CreateIndex(
                name: "IX_Reclusos_UsuarioId",
                table: "Reclusos",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expedientes");

            migrationBuilder.DropTable(
                name: "Guardias");

            migrationBuilder.DropTable(
                name: "Reclusos");

            migrationBuilder.DropTable(
                name: "Celdas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
