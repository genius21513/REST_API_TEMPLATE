using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace REST_API_TEMPLATE.Migrations
{
    public partial class inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Caption = table.Column<string>(type: "text", nullable: true),
                    Url = table.Column<string>(type: "text", nullable: true),
                    AlbumId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Albums",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("6ebc3dbe-2e7b-4132-8c33-e089d47694cd"), "Gallery 2" },
                    { new Guid("90d10994-3bdd-4ca2-a178-6a35fd653c59"), "Gallery 1" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "AlbumId", "Caption", "Name", "Url" },
                values: new object[,]
                {
                    { new Guid("150c81c6-2458-466e-907a-2df11325e2b8"), new Guid("6ebc3dbe-2e7b-4132-8c33-e089d47694cd"), "incorrect default image", "ccc.jpg", "https://1232131321/ccc.jpg" },
                    { new Guid("98474b8e-d713-401e-8aee-acb7353f97bb"), new Guid("90d10994-3bdd-4ca2-a178-6a35fd653c59"), "incorrect default image", "bbb.jpg", "--------------" },
                    { new Guid("bfe902af-3cf0-4a1c-8a83-66be60b028ba"), new Guid("90d10994-3bdd-4ca2-a178-6a35fd653c59"), "incorrect default image", "aaa.jpg", "--------------" }
                });

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
