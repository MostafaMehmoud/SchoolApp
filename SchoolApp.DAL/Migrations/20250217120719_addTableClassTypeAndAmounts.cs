using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addTableClassTypeAndAmounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_classTypes_Code",
                table: "classTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "classTypes");

            migrationBuilder.AddColumn<decimal>(
                name: "CLSAmountAdvs",
                table: "classTypes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CLSAmountBook",
                table: "classTypes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CLSAmountBus",
                table: "classTypes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CLSAmountInst",
                table: "classTypes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "amounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassTypeId = table.Column<int>(type: "int", nullable: false),
                    AmountPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_amounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_amounts_classTypes_ClassTypeId",
                        column: x => x.ClassTypeId,
                        principalTable: "classTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "classTypeName",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    StageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_classTypeName", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_classTypes_StageId",
                table: "classTypes",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_amounts_ClassTypeId",
                table: "amounts",
                column: "ClassTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_classTypeName_Code",
                table: "classTypeName",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_classTypes_stages_StageId",
                table: "classTypes",
                column: "StageId",
                principalTable: "stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_classTypes_stages_StageId",
                table: "classTypes");

            migrationBuilder.DropTable(
                name: "amounts");

            migrationBuilder.DropTable(
                name: "classTypeName");

            migrationBuilder.DropIndex(
                name: "IX_classTypes_StageId",
                table: "classTypes");

            migrationBuilder.DropColumn(
                name: "CLSAmountAdvs",
                table: "classTypes");

            migrationBuilder.DropColumn(
                name: "CLSAmountBook",
                table: "classTypes");

            migrationBuilder.DropColumn(
                name: "CLSAmountBus",
                table: "classTypes");

            migrationBuilder.DropColumn(
                name: "CLSAmountInst",
                table: "classTypes");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "classTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_classTypes_Code",
                table: "classTypes",
                column: "Code",
                unique: true);
        }
    }
}
