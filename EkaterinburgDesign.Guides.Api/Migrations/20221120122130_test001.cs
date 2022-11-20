using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EkaterinburgDesign.Guides.Api.Migrations
{
    /// <inheritdoc />
    public partial class test001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "TestEntities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestEntities",
                table: "TestEntities",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TestEntities",
                table: "TestEntities");

            migrationBuilder.RenameTable(
                name: "TestEntities",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");
        }
    }
}
