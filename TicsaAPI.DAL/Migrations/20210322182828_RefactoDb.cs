using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TicsaAPI.DAL.Migrations
{
    public partial class RefactoDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
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
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GammeType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GammeType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Producer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    FirstName = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    CompagnieName = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    address = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    phoneNumber = table.Column<int>(nullable: true),
                    Email = table.Column<int>(nullable: true),
                    PostalCode = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Commentary",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdClient = table.Column<int>(nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CommentaryDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__Commentar__IdCli__3A81B327",
                        column: x => x.IdClient,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "date", nullable: false),
                    IdClient = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Order__IdClient__34C8D9D1",
                        column: x => x.IdClient,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Gamme",
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
                    StockHisto = table.Column<string>(type: "text", nullable: true),
                    IdProducer = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamme", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Gamme__IdProduce__300424B4",
                        column: x => x.IdProducer,
                        principalTable: "Producer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Gamme__IdType__2F10007B",
                        column: x => x.IdType,
                        principalTable: "GammeType",
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
                        name: "FK__OrderCont__IdGam__38996AB5",
                        column: x => x.IdGamme,
                        principalTable: "Gamme",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__OrderCont__IdOrd__37A5467C",
                        column: x => x.IdOrder,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commentary_IdClient",
                table: "Commentary",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_Gamme_IdProducer",
                table: "Gamme",
                column: "IdProducer");

            migrationBuilder.CreateIndex(
                name: "IX_Gamme_IdType",
                table: "Gamme",
                column: "IdType");

            migrationBuilder.CreateIndex(
                name: "IX_Order_IdClient",
                table: "Order",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_OrderContent_IdGamme",
                table: "OrderContent",
                column: "IdGamme");

            migrationBuilder.CreateIndex(
                name: "IX_OrderContent_IdOrder",
                table: "OrderContent",
                column: "IdOrder");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commentary");

            migrationBuilder.DropTable(
                name: "OrderContent");

            migrationBuilder.DropTable(
                name: "Gamme");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Producer");

            migrationBuilder.DropTable(
                name: "GammeType");

            migrationBuilder.DropTable(
                name: "Client");
        }
    }
}
