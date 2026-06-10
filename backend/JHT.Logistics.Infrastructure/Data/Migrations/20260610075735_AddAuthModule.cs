using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JHT.Logistics.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TBL_CROL",
                keyColumn: "ROL_ID",
                keyValue: 1,
                column: "ROL_FECHA_CREACION",
                value: new DateTime(2026, 6, 10, 7, 57, 35, 12, DateTimeKind.Utc).AddTicks(8181));

            migrationBuilder.UpdateData(
                table: "TBL_CROL",
                keyColumn: "ROL_ID",
                keyValue: 2,
                column: "ROL_FECHA_CREACION",
                value: new DateTime(2026, 6, 10, 7, 57, 35, 12, DateTimeKind.Utc).AddTicks(8186));

            migrationBuilder.UpdateData(
                table: "TBL_CROL",
                keyColumn: "ROL_ID",
                keyValue: 3,
                column: "ROL_FECHA_CREACION",
                value: new DateTime(2026, 6, 10, 7, 57, 35, 12, DateTimeKind.Utc).AddTicks(8188));

            migrationBuilder.InsertData(
                table: "TBL_UUSUARIO",
                columns: new[] { "USU_ID", "USU_ESTADO", "USU_FECHA_CREACION", "USU_FECHA_MODIFICACION", "USU_EMAIL", "USU_LOGIN", "USU_NOMBRE", "USU_PASSWORD_HASH", "USU_USUARIO_CREACION", "USU_USUARIO_MODIFICACION" },
                values: new object[] { 1, true, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "admin@jhtlogistics.com", "admin", "Administrador del Sistema", "$2a$11$V.ZsAZaVSCnCTeiRY9u26OhFGxsL3/.ffZmNWC4VI5yjDo.X30dHa", "SYSTEM", null });

            migrationBuilder.InsertData(
                table: "TBL_RUSUARIO_ROL",
                columns: new[] { "ROL_ID", "USR_ID" },
                values: new object[] { 1, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TBL_RUSUARIO_ROL",
                keyColumns: new[] { "ROL_ID", "USR_ID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "TBL_UUSUARIO",
                keyColumn: "USU_ID",
                keyValue: 1);

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
        }
    }
}
