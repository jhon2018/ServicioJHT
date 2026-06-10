using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JHT.Logistics.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_UUSUARIO_EMAIL",
                table: "TBL_UUSUARIO",
                column: "USU_EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UUSUARIO_LOGIN",
                table: "TBL_UUSUARIO",
                column: "USU_LOGIN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CROL_NOMBRE",
                table: "TBL_CROL",
                column: "ROL_NOMBRE",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UUSUARIO_EMAIL",
                table: "TBL_UUSUARIO");

            migrationBuilder.DropIndex(
                name: "IX_UUSUARIO_LOGIN",
                table: "TBL_UUSUARIO");

            migrationBuilder.DropIndex(
                name: "IX_CROL_NOMBRE",
                table: "TBL_CROL");

            migrationBuilder.UpdateData(
                table: "TBL_CROL",
                keyColumn: "ROL_ID",
                keyValue: 1,
                column: "ROL_FECHA_CREACION",
                value: new DateTime(2026, 6, 10, 5, 32, 7, 740, DateTimeKind.Utc).AddTicks(2760));

            migrationBuilder.UpdateData(
                table: "TBL_CROL",
                keyColumn: "ROL_ID",
                keyValue: 2,
                column: "ROL_FECHA_CREACION",
                value: new DateTime(2026, 6, 10, 5, 32, 7, 740, DateTimeKind.Utc).AddTicks(2766));

            migrationBuilder.UpdateData(
                table: "TBL_CROL",
                keyColumn: "ROL_ID",
                keyValue: 3,
                column: "ROL_FECHA_CREACION",
                value: new DateTime(2026, 6, 10, 5, 32, 7, 740, DateTimeKind.Utc).AddTicks(2768));
        }
    }
}
