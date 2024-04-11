using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INTEX_II_Group_4_3.Migrations
{
    /// <inheritdoc />
    public partial class James : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "age",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "birth_date",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "country_of_residence",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "first_name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "last_name",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: false),
                    country_of_residence = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    age = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "ProductRecommendations",
                columns: table => new
                {
                    Product_ID = table.Column<double>(type: "float", nullable: false),
                    Recommendation_1 = table.Column<double>(type: "float", nullable: false),
                    Recommendation_2 = table.Column<double>(type: "float", nullable: false),
                    Recommendation_3 = table.Column<double>(type: "float", nullable: false),
                    Recommendation_4 = table.Column<double>(type: "float", nullable: false),
                    Recommendation_5 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRecommendations", x => x.Product_ID);
                    table.ForeignKey(
                        name: "FK_ProductRecommendations_Products_Product_ID",
                        column: x => x.Product_ID,
                        principalTable: "Products",
                        principalColumn: "product_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductRecommendations_Products_Recommendation_1",
                        column: x => x.Recommendation_1,
                        principalTable: "Products",
                        principalColumn: "product_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductRecommendations_Products_Recommendation_2",
                        column: x => x.Recommendation_2,
                        principalTable: "Products",
                        principalColumn: "product_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductRecommendations_Products_Recommendation_3",
                        column: x => x.Recommendation_3,
                        principalTable: "Products",
                        principalColumn: "product_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductRecommendations_Products_Recommendation_4",
                        column: x => x.Recommendation_4,
                        principalTable: "Products",
                        principalColumn: "product_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductRecommendations_Products_Recommendation_5",
                        column: x => x.Recommendation_5,
                        principalTable: "Products",
                        principalColumn: "product_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductRecommendations_Recommendation_1",
                table: "ProductRecommendations",
                column: "Recommendation_1");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRecommendations_Recommendation_2",
                table: "ProductRecommendations",
                column: "Recommendation_2");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRecommendations_Recommendation_3",
                table: "ProductRecommendations",
                column: "Recommendation_3");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRecommendations_Recommendation_4",
                table: "ProductRecommendations",
                column: "Recommendation_4");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRecommendations_Recommendation_5",
                table: "ProductRecommendations",
                column: "Recommendation_5");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "ProductRecommendations");

            migrationBuilder.AddColumn<double>(
                name: "age",
                table: "AspNetUsers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "birth_date",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "country_of_residence",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "first_name",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "gender",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "last_name",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
