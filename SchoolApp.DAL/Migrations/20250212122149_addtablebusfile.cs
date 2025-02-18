using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addtablebusfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileBuses",
                columns: table => new
                {
                    BusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusCode = table.Column<int>(type: "int", nullable: false),
                    BusPlate = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    BusGo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BusReturn = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BusAll = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MobilPhone1 = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    MobilPhone2 = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    BusDrive = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    BusRoute = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    BusSup = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    BusState = table.Column<int>(type: "int", nullable: false),
                    BusNumber = table.Column<int>(type: "int", nullable: true),
                    BusStud = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileBuses", x => x.BusId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileBuses_BusCode",
                table: "FileBuses",
                column: "BusCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileBuses");
        }
    }
}
