using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JHT.Logistics.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditoriaModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_AAUDITORIA",
                columns: table => new
                {
                    AUD_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AUD_TABLA = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    AUD_REGISTRO_ID = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    AUD_ACCION = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    AUD_USUARIO = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    AUD_FECHA = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AUD_VALORES_ANTERIORES = table.Column<string>(type: "jsonb", nullable: true),
                    AUD_VALORES_NUEVOS = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_AAUDITORIA", x => x.AUD_ID);
                });

            migrationBuilder.UpdateData(
                table: "TBL_CROL",
                keyColumn: "ROL_ID",
                keyValue: 1,
                column: "ROL_FECHA_CREACION",
                value: new DateTime(2026, 6, 10, 6, 22, 30, 488, DateTimeKind.Utc).AddTicks(7898));

            migrationBuilder.UpdateData(
                table: "TBL_CROL",
                keyColumn: "ROL_ID",
                keyValue: 2,
                column: "ROL_FECHA_CREACION",
                value: new DateTime(2026, 6, 10, 6, 22, 30, 488, DateTimeKind.Utc).AddTicks(7904));

            migrationBuilder.UpdateData(
                table: "TBL_CROL",
                keyColumn: "ROL_ID",
                keyValue: 3,
                column: "ROL_FECHA_CREACION",
                value: new DateTime(2026, 6, 10, 6, 22, 30, 488, DateTimeKind.Utc).AddTicks(7906));

            migrationBuilder.CreateIndex(
                name: "IX_AAUDITORIA_REGISTROID",
                table: "TBL_AAUDITORIA",
                column: "AUD_REGISTRO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AAUDITORIA_TABLA",
                table: "TBL_AAUDITORIA",
                column: "AUD_TABLA");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_AAUDITORIA");

            migrationBuilder.UpdateData(
                table: "TBL_CROL",
                keyColumn: "ROL_ID",
                keyValue: 1,
                column: "ROL_FECHA_CREACION",
                value: new DateTime(2026, 6, 10, 6, 12, 3, 309, DateTimeKind.Utc).AddTicks(9469));

            migrationBuilder.UpdateData(
                table: "TBL_CROL",
                keyColumn: "ROL_ID",
                keyValue: 2,
                column: "ROL_FECHA_CREACION",
                value: new DateTime(2026, 6, 10, 6, 12, 3, 309, DateTimeKind.Utc).AddTicks(9474));

            migrationBuilder.UpdateData(
                table: "TBL_CROL",
                keyColumn: "ROL_ID",
                keyValue: 3,
                column: "ROL_FECHA_CREACION",
                value: new DateTime(2026, 6, 10, 6, 12, 3, 309, DateTimeKind.Utc).AddTicks(9477));
        }
    }
}
