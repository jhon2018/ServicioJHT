using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JHT.Logistics.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddConductorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_TCONDUCTOR",
                columns: table => new
                {
                    CON_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CON_CODIGO_EXTERNO = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CON_TIPO = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    CON_DNI = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CON_NOMBRE_COMPLETO = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CON_TELEFONO = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CON_EMAIL = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    CON_FECHA_CREACION = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CON_USUARIO_CREACION = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CON_FECHA_MODIFICACION = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CON_USUARIO_MODIFICACION = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CON_ESTADO = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_TCONDUCTOR", x => x.CON_ID);
                });

            migrationBuilder.UpdateData(
                table: "TBL_CROL",
                keyColumn: "ROL_ID",
                keyValue: 1,
                column: "ROL_FECHA_CREACION",
                value: new DateTime(2026, 6, 10, 5, 57, 36, 162, DateTimeKind.Utc).AddTicks(3051));

            migrationBuilder.UpdateData(
                table: "TBL_CROL",
                keyColumn: "ROL_ID",
                keyValue: 2,
                column: "ROL_FECHA_CREACION",
                value: new DateTime(2026, 6, 10, 5, 57, 36, 162, DateTimeKind.Utc).AddTicks(3056));

            migrationBuilder.UpdateData(
                table: "TBL_CROL",
                keyColumn: "ROL_ID",
                keyValue: 3,
                column: "ROL_FECHA_CREACION",
                value: new DateTime(2026, 6, 10, 5, 57, 36, 162, DateTimeKind.Utc).AddTicks(3059));

            migrationBuilder.CreateIndex(
                name: "IX_TCONDUCTOR_DNI",
                table: "TBL_TCONDUCTOR",
                column: "CON_DNI",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_TCONDUCTOR");

            migrationBuilder.UpdateData(
                table: "TBL_CROL",
                keyColumn: "ROL_ID",
                keyValue: 1,
                column: "ROL_FECHA_CREACION",
                value: new DateTime(2026, 6, 10, 5, 51, 42, 632, DateTimeKind.Utc).AddTicks(3485));

            migrationBuilder.UpdateData(
                table: "TBL_CROL",
                keyColumn: "ROL_ID",
                keyValue: 2,
                column: "ROL_FECHA_CREACION",
                value: new DateTime(2026, 6, 10, 5, 51, 42, 632, DateTimeKind.Utc).AddTicks(3490));

            migrationBuilder.UpdateData(
                table: "TBL_CROL",
                keyColumn: "ROL_ID",
                keyValue: 3,
                column: "ROL_FECHA_CREACION",
                value: new DateTime(2026, 6, 10, 5, 51, 42, 632, DateTimeKind.Utc).AddTicks(3493));
        }
    }
}
