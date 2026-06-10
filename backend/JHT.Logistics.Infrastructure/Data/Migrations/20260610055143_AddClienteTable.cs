using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JHT.Logistics.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddClienteTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_TCLIENTE",
                columns: table => new
                {
                    CLI_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CLI_TIPO_DOCUMENTO = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CLI_NUMERO_DOCUMENTO = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CLI_RAZON_SOCIAL = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CLI_NOMBRE_COMERCIAL = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CLI_DIRECCION = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CLI_TELEFONO = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CLI_EMAIL = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    CLI_FECHA_CREACION = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CLI_USUARIO_CREACION = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CLI_FECHA_MODIFICACION = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CLI_USUARIO_MODIFICACION = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CLI_ESTADO = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_TCLIENTE", x => x.CLI_ID);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_TCLIENTE_NUMERO_DOCUMENTO",
                table: "TBL_TCLIENTE",
                column: "CLI_NUMERO_DOCUMENTO",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_TCLIENTE");

            migrationBuilder.UpdateData(
                table: "TBL_CROL",
                keyColumn: "ROL_ID",
                keyValue: 1,
                column: "ROL_FECHA_CREACION",
                value: new DateTime(2026, 6, 10, 5, 46, 43, 458, DateTimeKind.Utc).AddTicks(3374));

            migrationBuilder.UpdateData(
                table: "TBL_CROL",
                keyColumn: "ROL_ID",
                keyValue: 2,
                column: "ROL_FECHA_CREACION",
                value: new DateTime(2026, 6, 10, 5, 46, 43, 458, DateTimeKind.Utc).AddTicks(3380));

            migrationBuilder.UpdateData(
                table: "TBL_CROL",
                keyColumn: "ROL_ID",
                keyValue: 3,
                column: "ROL_FECHA_CREACION",
                value: new DateTime(2026, 6, 10, 5, 46, 43, 458, DateTimeKind.Utc).AddTicks(3381));
        }
    }
}
