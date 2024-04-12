using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INTEX_II_Group_4_3.Migrations
{
    /// <inheritdoc />
    public partial class YourMigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TopProductRecommendations",
                columns: table => new
                {
                    product_ID = table.Column<double>(type: "float", nullable: false),
                    ratings_count = table.Column<double>(type: "float", nullable: false),
                    ratings_mean = table.Column<double>(type: "float", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopProductRecommendations", x => x.product_ID);
                    table.ForeignKey(
                        name: "FK_TopProductRecommendations_Products_product_ID",
                        column: x => x.product_ID,
                        principalTable: "Products",
                        principalColumn: "product_ID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TopProductRecommendations");
        }
    }
}
