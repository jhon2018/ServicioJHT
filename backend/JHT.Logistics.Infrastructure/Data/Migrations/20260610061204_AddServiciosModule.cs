using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JHT.Logistics.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddServiciosModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_CESTADO_SERVICIO",
                columns: table => new
                {
                    EST_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EST_CODIGO = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    EST_NOMBRE = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_CESTADO_SERVICIO", x => x.EST_ID);
                });

            migrationBuilder.CreateTable(
                name: "TBL_CTIPO_DOCUMENTO",
                columns: table => new
                {
                    TIPDOC_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TIPDOC_CODIGO = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TIPDOC_NOMBRE = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_CTIPO_DOCUMENTO", x => x.TIPDOC_ID);
                });

            migrationBuilder.CreateTable(
                name: "TBL_TSERVICIO",
                columns: table => new
                {
                    SER_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SER_CODIGO = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CLI_ID = table.Column<int>(type: "integer", nullable: false),
                    SER_TIPO_SERVICIO = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SER_DESCRIPCION = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    SER_OBSERVACION = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    SER_PRIORIDAD = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    SER_FECHA_PROGRAMADA = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SER_FECHA_INICIO_REAL = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SER_FECHA_FIN_REAL = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EST_ID = table.Column<int>(type: "integer", nullable: false),
                    SER_FECHA_CREACION = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SER_USUARIO_CREACION = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SER_FECHA_MODIFICACION = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SER_USUARIO_MODIFICACION = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    SER_ESTADO_LOGICO = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_TSERVICIO", x => x.SER_ID);
                    table.ForeignKey(
                        name: "FK_TBL_TSERVICIO_TBL_CESTADO_SERVICIO_EST_ID",
                        column: x => x.EST_ID,
                        principalTable: "TBL_CESTADO_SERVICIO",
                        principalColumn: "EST_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBL_TSERVICIO_TBL_TCLIENTE_CLI_ID",
                        column: x => x.CLI_ID,
                        principalTable: "TBL_TCLIENTE",
                        principalColumn: "CLI_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TBL_DSERVICIO_DESTINO",
                columns: table => new
                {
                    SERDES_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SER_ID = table.Column<int>(type: "integer", nullable: false),
                    SERDES_SECUENCIA = table.Column<int>(type: "integer", nullable: false),
                    SERDES_DESTINATARIO = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    SERDES_RUC = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    SERDES_DIRECCION = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    SERDES_REFERENCIA = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    SERDES_CONTACTO = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    SERDES_TELEFONO = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    SERDES_ESTADO = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SERDES_FECHA_CREACION = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SERDES_USUARIO_CREACION = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SERDES_FECHA_MODIFICACION = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SERDES_USUARIO_MODIFICACION = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    SERDES_ESTADO_LOGICO = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_DSERVICIO_DESTINO", x => x.SERDES_ID);
                    table.ForeignKey(
                        name: "FK_TBL_DSERVICIO_DESTINO_TBL_TSERVICIO_SER_ID",
                        column: x => x.SER_ID,
                        principalTable: "TBL_TSERVICIO",
                        principalColumn: "SER_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_DSERVICIO_DOCUMENTO",
                columns: table => new
                {
                    SERDOC_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SER_ID = table.Column<int>(type: "integer", nullable: false),
                    TIPDOC_ID = table.Column<int>(type: "integer", nullable: false),
                    SERDOC_NUMERO = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    SERDOC_ARCHIVO_URL = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    SERDOC_NOMBRE_ORIGINAL = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    SERDOC_FECHA_CARGA = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    USUARIO_CARGA = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SERDOC_FECHA_CREACION = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SERDOC_USUARIO_CREACION = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SERDOC_FECHA_MODIFICACION = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SERDOC_USUARIO_MODIFICACION = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    SERDOC_ESTADO_LOGICO = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_DSERVICIO_DOCUMENTO", x => x.SERDOC_ID);
                    table.ForeignKey(
                        name: "FK_TBL_DSERVICIO_DOCUMENTO_TBL_CTIPO_DOCUMENTO_TIPDOC_ID",
                        column: x => x.TIPDOC_ID,
                        principalTable: "TBL_CTIPO_DOCUMENTO",
                        principalColumn: "TIPDOC_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBL_DSERVICIO_DOCUMENTO_TBL_TSERVICIO_SER_ID",
                        column: x => x.SER_ID,
                        principalTable: "TBL_TSERVICIO",
                        principalColumn: "SER_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_HSERVICIO_ESTADO",
                columns: table => new
                {
                    HSE_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SER_ID = table.Column<int>(type: "integer", nullable: false),
                    EST_ID = table.Column<int>(type: "integer", nullable: false),
                    HSE_FECHA_HORA = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    HSE_OBSERVACION = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    USUARIO_REGISTRO = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_HSERVICIO_ESTADO", x => x.HSE_ID);
                    table.ForeignKey(
                        name: "FK_TBL_HSERVICIO_ESTADO_TBL_CESTADO_SERVICIO_EST_ID",
                        column: x => x.EST_ID,
                        principalTable: "TBL_CESTADO_SERVICIO",
                        principalColumn: "EST_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBL_HSERVICIO_ESTADO_TBL_TSERVICIO_SER_ID",
                        column: x => x.SER_ID,
                        principalTable: "TBL_TSERVICIO",
                        principalColumn: "SER_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_RSERVICIO_CONDUCTOR",
                columns: table => new
                {
                    SER_ID = table.Column<int>(type: "integer", nullable: false),
                    CON_ID = table.Column<int>(type: "integer", nullable: false),
                    FECHA_ASIGNACION = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    USUARIO_ASIGNADOR = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FECHA_INICIO = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FECHA_FIN = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ASIGNACION_ACTIVA = table.Column<bool>(type: "boolean", nullable: false),
                    MOTIVO_CAMBIO = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_RSERVICIO_CONDUCTOR", x => new { x.SER_ID, x.CON_ID });
                    table.ForeignKey(
                        name: "FK_TBL_RSERVICIO_CONDUCTOR_TBL_TCONDUCTOR_CON_ID",
                        column: x => x.CON_ID,
                        principalTable: "TBL_TCONDUCTOR",
                        principalColumn: "CON_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBL_RSERVICIO_CONDUCTOR_TBL_TSERVICIO_SER_ID",
                        column: x => x.SER_ID,
                        principalTable: "TBL_TSERVICIO",
                        principalColumn: "SER_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_RSERVICIO_VEHICULO",
                columns: table => new
                {
                    SER_ID = table.Column<int>(type: "integer", nullable: false),
                    VEH_ID = table.Column<int>(type: "integer", nullable: false),
                    FECHA_ASIGNACION = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    USUARIO_ASIGNADOR = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FECHA_INICIO = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FECHA_FIN = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ASIGNACION_ACTIVA = table.Column<bool>(type: "boolean", nullable: false),
                    MOTIVO_CAMBIO = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_RSERVICIO_VEHICULO", x => new { x.SER_ID, x.VEH_ID });
                    table.ForeignKey(
                        name: "FK_TBL_RSERVICIO_VEHICULO_TBL_TSERVICIO_SER_ID",
                        column: x => x.SER_ID,
                        principalTable: "TBL_TSERVICIO",
                        principalColumn: "SER_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_RSERVICIO_VEHICULO_TBL_TVEHICULO_VEH_ID",
                        column: x => x.VEH_ID,
                        principalTable: "TBL_TVEHICULO",
                        principalColumn: "VEH_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TBL_TTRACKING_PUBLICO",
                columns: table => new
                {
                    TRK_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SER_ID = table.Column<int>(type: "integer", nullable: false),
                    TRK_TOKEN = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TRK_FECHA_CREACION = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TRK_ESTADO = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_TTRACKING_PUBLICO", x => x.TRK_ID);
                    table.ForeignKey(
                        name: "FK_TBL_TTRACKING_PUBLICO_TBL_TSERVICIO_SER_ID",
                        column: x => x.SER_ID,
                        principalTable: "TBL_TSERVICIO",
                        principalColumn: "SER_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TBL_CESTADO_SERVICIO",
                columns: new[] { "EST_ID", "EST_CODIGO", "EST_NOMBRE" },
                values: new object[,]
                {
                    { 1, "RECEPCIONADO", "Recepcionado" },
                    { 2, "PROGRAMADO", "Programado" },
                    { 3, "UNIDAD_ASIGNADA", "Unidad Asignada" },
                    { 4, "EN_RUTA", "En Ruta" },
                    { 5, "MUY_CERCA", "Muy Cerca" },
                    { 6, "ENTREGADO", "Entregado" },
                    { 7, "CANCELADO", "Cancelado" }
                });

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

            migrationBuilder.InsertData(
                table: "TBL_CTIPO_DOCUMENTO",
                columns: new[] { "TIPDOC_ID", "TIPDOC_CODIGO", "TIPDOC_NOMBRE" },
                values: new object[,]
                {
                    { 1, "GRE", "Guía de Remisión" },
                    { 2, "FACTURA", "Factura" },
                    { 3, "BOLETA", "Boleta" },
                    { 4, "ORDEN_SERVICIO", "Orden de Servicio" },
                    { 5, "ORDEN_COMPRA", "Orden de Compra" },
                    { 6, "CARGO", "Cargo de Entrega" },
                    { 7, "EVIDENCIA", "Evidencia Fotográfica" },
                    { 8, "OTRO", "Otro Documento" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CESTADO_CODIGO",
                table: "TBL_CESTADO_SERVICIO",
                column: "EST_CODIGO",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CTIPDOC_CODIGO",
                table: "TBL_CTIPO_DOCUMENTO",
                column: "TIPDOC_CODIGO",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBL_DSERVICIO_DESTINO_SER_ID",
                table: "TBL_DSERVICIO_DESTINO",
                column: "SER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_DSERVICIO_DOCUMENTO_SER_ID",
                table: "TBL_DSERVICIO_DOCUMENTO",
                column: "SER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_DSERVICIO_DOCUMENTO_TIPDOC_ID",
                table: "TBL_DSERVICIO_DOCUMENTO",
                column: "TIPDOC_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_HSERVICIO_ESTADO_EST_ID",
                table: "TBL_HSERVICIO_ESTADO",
                column: "EST_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_HSERVICIO_ESTADO_SER_ID",
                table: "TBL_HSERVICIO_ESTADO",
                column: "SER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_RSERVICIO_CONDUCTOR_CON_ID",
                table: "TBL_RSERVICIO_CONDUCTOR",
                column: "CON_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_RSERVICIO_VEHICULO_VEH_ID",
                table: "TBL_RSERVICIO_VEHICULO",
                column: "VEH_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_TSERVICIO_CLI_ID",
                table: "TBL_TSERVICIO",
                column: "CLI_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_TSERVICIO_EST_ID",
                table: "TBL_TSERVICIO",
                column: "EST_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TSERVICIO_CODIGO",
                table: "TBL_TSERVICIO",
                column: "SER_CODIGO",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TTRACKING_SERVICIO",
                table: "TBL_TTRACKING_PUBLICO",
                column: "SER_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TTRACKING_TOKEN",
                table: "TBL_TTRACKING_PUBLICO",
                column: "TRK_TOKEN",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_DSERVICIO_DESTINO");

            migrationBuilder.DropTable(
                name: "TBL_DSERVICIO_DOCUMENTO");

            migrationBuilder.DropTable(
                name: "TBL_HSERVICIO_ESTADO");

            migrationBuilder.DropTable(
                name: "TBL_RSERVICIO_CONDUCTOR");

            migrationBuilder.DropTable(
                name: "TBL_RSERVICIO_VEHICULO");

            migrationBuilder.DropTable(
                name: "TBL_TTRACKING_PUBLICO");

            migrationBuilder.DropTable(
                name: "TBL_CTIPO_DOCUMENTO");

            migrationBuilder.DropTable(
                name: "TBL_TSERVICIO");

            migrationBuilder.DropTable(
                name: "TBL_CESTADO_SERVICIO");

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
        }
    }
}
