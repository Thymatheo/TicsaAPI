using Microsoft.EntityFrameworkCore.Migrations;

namespace TicsaAPI.DAL.Migrations
{
    public partial class RemoveCrossAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Gammes__IdType__4BAC3F29",
                table: "Gammes");

            migrationBuilder.DropForeignKey(
                name: "FK__OrderCont__IdGam__5441852A",
                table: "OrderContent");

            migrationBuilder.DropForeignKey(
                name: "FK__OrderCont__IdOrd__534D60F1",
                table: "OrderContent");

            migrationBuilder.DropForeignKey(
                name: "FK__Orders__IdClient__5070F446",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_IdClient",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_OrderContent_IdGamme",
                table: "OrderContent");

            migrationBuilder.DropIndex(
                name: "IX_OrderContent_IdOrder",
                table: "OrderContent");

            migrationBuilder.DropIndex(
                name: "IX_Gammes_IdType",
                table: "Gammes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Orders_IdClient",
                table: "Orders",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_OrderContent_IdGamme",
                table: "OrderContent",
                column: "IdGamme");

            migrationBuilder.CreateIndex(
                name: "IX_OrderContent_IdOrder",
                table: "OrderContent",
                column: "IdOrder");

            migrationBuilder.CreateIndex(
                name: "IX_Gammes_IdType",
                table: "Gammes",
                column: "IdType");

            migrationBuilder.AddForeignKey(
                name: "FK__Gammes__IdType__4BAC3F29",
                table: "Gammes",
                column: "IdType",
                principalTable: "GammeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__OrderCont__IdGam__5441852A",
                table: "OrderContent",
                column: "IdGamme",
                principalTable: "Gammes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__OrderCont__IdOrd__534D60F1",
                table: "OrderContent",
                column: "IdOrder",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__Orders__IdClient__5070F446",
                table: "Orders",
                column: "IdClient",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
