using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TicsaAPI.DAL.Migrations
{
    public partial class AddPrimaryKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    FirstName = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    CompagnieName = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    Email = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    Address = table.Column<string>(type: "text", nullable: false),
                    PostalCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GammeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GammeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "date", nullable: false),
                    IdClient = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Orders__IdClient__5070F446",
                        column: x => x.IdClient,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Gammes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CostHisto = table.Column<string>(type: "text", nullable: true),
                    Cost = table.Column<double>(nullable: false),
                    IdType = table.Column<int>(nullable: false),
                    Stock = table.Column<int>(nullable: false),
                    StockHisto = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gammes", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Gammes__IdType__4BAC3F29",
                        column: x => x.IdType,
                        principalTable: "GammeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderContent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOrder = table.Column<int>(nullable: false),
                    IdGamme = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK__OrderCont__IdGam__5441852A",
                        column: x => x.IdGamme,
                        principalTable: "Gammes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__OrderCont__IdOrd__534D60F1",
                        column: x => x.IdOrder,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gammes_IdType",
                table: "Gammes",
                column: "IdType");

            migrationBuilder.CreateIndex(
                name: "IX_OrderContent_IdGamme",
                table: "OrderContent",
                column: "IdGamme");

            migrationBuilder.CreateIndex(
                name: "IX_OrderContent_IdOrder",
                table: "OrderContent",
                column: "IdOrder");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_IdClient",
                table: "Orders",
                column: "IdClient");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderContent");

            migrationBuilder.DropTable(
                name: "Gammes");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "GammeTypes");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
