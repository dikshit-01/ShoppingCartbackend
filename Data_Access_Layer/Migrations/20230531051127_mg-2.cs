using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    public partial class mg2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Rating = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Images = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2569645b-3256-49ba-ab5f-f02fc81a9527",
                column: "ConcurrencyStamp",
                value: "7a161610-6f25-4eed-a081-2244da4a438e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f607f7ef-01df-4943-b46b-2726091e7697",
                column: "ConcurrencyStamp",
                value: "039c8d19-acf9-49cb-af71-18271f38c2ec");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e215ed86-87d2-443a-9d10-d8cd761a0a5e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6fde11ba-cb63-4d1f-86a5-32d1c58df925", "AQAAAAEAACcQAAAAEG+lQe96381pJp7jVUsHnBwMuywp8dntL0e1P0hgVnNvQRSlGLbAl8ghQBmy29xzmA==", "95235849-dba9-40da-bfae-f79163291907" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Electronics" },
                    { 2, "Clothings" },
                    { 3, "Grocery" },
                    { 4, "Confectionaries" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2569645b-3256-49ba-ab5f-f02fc81a9527",
                column: "ConcurrencyStamp",
                value: "9548b93a-3dca-49ed-9372-e9e667186420");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f607f7ef-01df-4943-b46b-2726091e7697",
                column: "ConcurrencyStamp",
                value: "89786dea-e6af-4623-8c62-24e20d1dd893");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e215ed86-87d2-443a-9d10-d8cd761a0a5e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dc487018-1819-4c0e-bfc9-72fe602db151", "AQAAAAEAACcQAAAAEIGkROjugmxFJKCsZd6crxmqmdZ4K7Mi2hWDhCuF5DumgNMA9wtgA4Ni8AS0xC4sfw==", "0709bed1-0ee1-4c21-817d-1a2547e77cab" });
        }
    }
}
