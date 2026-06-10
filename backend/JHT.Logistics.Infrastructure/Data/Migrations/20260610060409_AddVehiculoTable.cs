using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JHT.Logistics.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddVehiculoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_TVEHICULO",
                columns: table => new
                {
                    VEH_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VEH_PLACA = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    VEH_MARCA = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    VEH_MODELO = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    VEH_TIPO = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    VEH_CAPACIDAD = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    VEH_FECHA_CREACION = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    VEH_USUARIO_CREACION = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    VEH_FECHA_MODIFICACION = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    VEH_USUARIO_MODIFICACION = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    VEH_ESTADO = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_TVEHICULO", x => x.VEH_ID);
                });

            migrationBuilder.UpdateData(
                table: "TBL_CROL",
                keyColumn: "ROL_ID",
                keyValue: 1,
                column: "ROL_FECHA_CREACION",
                value: new DateTime(2026, 6, 10, 6, 4, 8, 757, DateTimeKind.Utc).AddTicks(3075));

            migrationBuilder.UpdateData(
                table: "TBL_CROL",
                keyColumn: "ROL_ID",
                keyValue: 2,
                column: "ROL_FECHA_CREACION",
                value: new DateTime(2026, 6, 10, 6, 4, 8, 757, DateTimeKind.Utc).AddTicks(3082));

            migrationBuilder.UpdateData(
                table: "TBL_CROL",
                keyColumn: "ROL_ID",
                keyValue: 3,
                column: "ROL_FECHA_CREACION",
                value: new DateTime(2026, 6, 10, 6, 4, 8, 757, DateTimeKind.Utc).AddTicks(3084));

            migrationBuilder.CreateIndex(
                name: "IX_TVEHICULO_PLACA",
                table: "TBL_TVEHICULO",
                column: "VEH_PLACA",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_TVEHICULO");

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
        }
    }
}
