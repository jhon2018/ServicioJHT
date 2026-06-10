using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JHT.Logistics.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_CROL",
                columns: table => new
                {
                    ROL_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ROL_NOMBRE = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ROL_DESCRIPCION = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ROL_FECHA_CREACION = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ROL_USUARIO_CREACION = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ROL_FECHA_MODIFICACION = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ROL_USUARIO_MODIFICACION = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ROL_ESTADO = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_CROL", x => x.ROL_ID);
                });

            migrationBuilder.CreateTable(
                name: "TBL_UUSUARIO",
                columns: table => new
                {
                    USU_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    USU_LOGIN = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    USU_NOMBRE = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    USU_EMAIL = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    USU_PASSWORD_HASH = table.Column<string>(type: "text", nullable: false),
                    USU_FECHA_CREACION = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    USU_USUARIO_CREACION = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    USU_FECHA_MODIFICACION = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    USU_USUARIO_MODIFICACION = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    USU_ESTADO = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_UUSUARIO", x => x.USU_ID);
                });

            migrationBuilder.CreateTable(
                name: "TBL_RUSUARIO_ROL",
                columns: table => new
                {
                    USR_ID = table.Column<int>(type: "integer", nullable: false),
                    ROL_ID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_RUSUARIO_ROL", x => new { x.USR_ID, x.ROL_ID });
                    table.ForeignKey(
                        name: "FK_TBL_RUSUARIO_ROL_TBL_CROL_ROL_ID",
                        column: x => x.ROL_ID,
                        principalTable: "TBL_CROL",
                        principalColumn: "ROL_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_RUSUARIO_ROL_TBL_UUSUARIO_USR_ID",
                        column: x => x.USR_ID,
                        principalTable: "TBL_UUSUARIO",
                        principalColumn: "USU_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TBL_CROL",
                columns: new[] { "ROL_ID", "ROL_ESTADO", "ROL_FECHA_CREACION", "ROL_FECHA_MODIFICACION", "ROL_DESCRIPCION", "ROL_NOMBRE", "ROL_USUARIO_CREACION", "ROL_USUARIO_MODIFICACION" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2026, 6, 10, 5, 32, 7, 740, DateTimeKind.Utc).AddTicks(2760), null, "Control total del sistema", "ADMINISTRADOR", "SYSTEM", null },
                    { 2, true, new DateTime(2026, 6, 10, 5, 32, 7, 740, DateTimeKind.Utc).AddTicks(2766), null, "Operación diaria", "OPERADOR", "SYSTEM", null },
                    { 3, true, new DateTime(2026, 6, 10, 5, 32, 7, 740, DateTimeKind.Utc).AddTicks(2768), null, "Uso desde aplicación Flutter", "CONDUCTOR", "SYSTEM", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_RUSUARIO_ROL_ROL_ID",
                table: "TBL_RUSUARIO_ROL",
                column: "ROL_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_RUSUARIO_ROL");

            migrationBuilder.DropTable(
                name: "TBL_CROL");

            migrationBuilder.DropTable(
                name: "TBL_UUSUARIO");
        }
    }
}
