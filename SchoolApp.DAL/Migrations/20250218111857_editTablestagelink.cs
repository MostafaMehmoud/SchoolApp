using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class editTablestagelink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_classTypes_stages_StageId",
                table: "classTypes");

            migrationBuilder.DropIndex(
                name: "IX_classTypes_StageId",
                table: "classTypes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_classTypes_StageId",
                table: "classTypes",
                column: "StageId");

            migrationBuilder.AddForeignKey(
                name: "FK_classTypes_stages_StageId",
                table: "classTypes",
                column: "StageId",
                principalTable: "stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
