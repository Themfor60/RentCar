using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentCar.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitTablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_formulario_reservaRequests_ReservaRequestId",
                table: "formulario");

            migrationBuilder.DropIndex(
                name: "IX_formulario_ReservaRequestId",
                table: "formulario");

            migrationBuilder.DropColumn(
                name: "Precio",
                table: "reservaRequests");

            migrationBuilder.RenameColumn(
                name: "Vehiculo",
                table: "reservaRequests",
                newName: "Cedula");

            migrationBuilder.RenameColumn(
                name: "ReservaRequestId",
                table: "formulario",
                newName: "VehiculoId");

            migrationBuilder.AlterColumn<string>(
                name: "Transmision",
                table: "vehiculos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Modelo",
                table: "vehiculos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Marca",
                table: "vehiculos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Foto",
                table: "vehiculos",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CapacidadMaletero",
                table: "vehiculos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "RentaFormularioIdRenta",
                table: "reservaRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_reservaRequests_RentaFormularioIdRenta",
                table: "reservaRequests",
                column: "RentaFormularioIdRenta");

            migrationBuilder.CreateIndex(
                name: "IX_formulario_VehiculoId",
                table: "formulario",
                column: "VehiculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_formulario_vehiculos_VehiculoId",
                table: "formulario",
                column: "VehiculoId",
                principalTable: "vehiculos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reservaRequests_formulario_RentaFormularioIdRenta",
                table: "reservaRequests",
                column: "RentaFormularioIdRenta",
                principalTable: "formulario",
                principalColumn: "IdRenta",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_formulario_vehiculos_VehiculoId",
                table: "formulario");

            migrationBuilder.DropForeignKey(
                name: "FK_reservaRequests_formulario_RentaFormularioIdRenta",
                table: "reservaRequests");

            migrationBuilder.DropIndex(
                name: "IX_reservaRequests_RentaFormularioIdRenta",
                table: "reservaRequests");

            migrationBuilder.DropIndex(
                name: "IX_formulario_VehiculoId",
                table: "formulario");

            migrationBuilder.DropColumn(
                name: "RentaFormularioIdRenta",
                table: "reservaRequests");

            migrationBuilder.RenameColumn(
                name: "Cedula",
                table: "reservaRequests",
                newName: "Vehiculo");

            migrationBuilder.RenameColumn(
                name: "VehiculoId",
                table: "formulario",
                newName: "ReservaRequestId");

            migrationBuilder.AlterColumn<string>(
                name: "Transmision",
                table: "vehiculos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Modelo",
                table: "vehiculos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Marca",
                table: "vehiculos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Foto",
                table: "vehiculos",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CapacidadMaletero",
                table: "vehiculos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Precio",
                table: "reservaRequests",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_formulario_ReservaRequestId",
                table: "formulario",
                column: "ReservaRequestId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_formulario_reservaRequests_ReservaRequestId",
                table: "formulario",
                column: "ReservaRequestId",
                principalTable: "reservaRequests",
                principalColumn: "IdReserva",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
