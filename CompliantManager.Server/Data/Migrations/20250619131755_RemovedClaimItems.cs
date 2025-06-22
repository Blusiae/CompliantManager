using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompliantManager.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedClaimItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClaimItem");

            migrationBuilder.AddColumn<int>(
                name: "FaultyQuantity",
                table: "OrderItem",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FaultyQuantity",
                table: "OrderItem");

            migrationBuilder.CreateTable(
                name: "ClaimItem",
                columns: table => new
                {
                    ClaimItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimId = table.Column<int>(type: "int", nullable: false),
                    OrderItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimItem", x => x.ClaimItemId);
                    table.ForeignKey(
                        name: "FK_ClaimItem_Claim_ClaimId",
                        column: x => x.ClaimId,
                        principalTable: "Claim",
                        principalColumn: "ClaimId");
                    table.ForeignKey(
                        name: "FK_ClaimItem_OrderItem_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "OrderItem",
                        principalColumn: "OrderItemId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClaimItem_ClaimId",
                table: "ClaimItem",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimItem_OrderItemId",
                table: "ClaimItem",
                column: "OrderItemId");
        }
    }
}
