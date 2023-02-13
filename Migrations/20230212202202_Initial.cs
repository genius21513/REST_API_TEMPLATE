using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace REST_API_TEMPLATE.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlbumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Albums",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("6ebc3dbe-2e7b-4132-8c33-e089d47694cd"), "Walter Isaacson" });

            migrationBuilder.InsertData(
                table: "Albums",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("90d10994-3bdd-4ca2-a178-6a35fd653c59"), "J.K. Rowling" });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "AlbumId", "Caption", "Description", "Url" },
                values: new object[] { new Guid("150c81c6-2458-466e-907a-2df11325e2b8"), new Guid("6ebc3dbe-2e7b-4132-8c33-e089d47694cd"), "Simon & Schuster; 1st edition (October 24, 2011)", "Walter Isaacson’s “enthralling” (The New Yorker) worldwide bestselling biography of Apple cofounder Steve Jobs.", "Steve Jobs" });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "AlbumId", "Caption", "Description", "Url" },
                values: new object[] { new Guid("98474b8e-d713-401e-8aee-acb7353f97bb"), new Guid("90d10994-3bdd-4ca2-a178-6a35fd653c59"), "Scholastic; 1st Scholastic Td Ppbk Print., Sept.1999 edition (September 1, 1998)", "Harry Potter's life is miserable. His parents are dead and he's stuck with his heartless relatives, who force him to live in a tiny closet under the stairs.", "Harry Potter and the Sorcerer's Stone" });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "AlbumId", "Caption", "Description", "Url" },
                values: new object[] { new Guid("bfe902af-3cf0-4a1c-8a83-66be60b028ba"), new Guid("90d10994-3bdd-4ca2-a178-6a35fd653c59"), "Scholastic Paperbacks; Reprint edition (September 1, 2000)", "Ever since Harry Potter had come home for the summer, the Dursleys had been so mean and hideous that all Harry wanted was to get back to the Hogwarts School for Witchcraft and Wizardry. ", "Harry Potter and the Chamber of Secrets" });

            migrationBuilder.CreateIndex(
                name: "IX_Images_AlbumId",
                table: "Images",
                column: "AlbumId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Albums");
        }
    }
}
