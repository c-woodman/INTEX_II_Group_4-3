using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INTEX_II_Group_4_3.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    customer_ID = table.Column<int>(type: "INTEGER", nullable: true),
                    first_name = table.Column<string>(type: "TEXT", nullable: true),
                    last_name = table.Column<string>(type: "TEXT", nullable: true),
                    birth_date = table.Column<string>(type: "TEXT", nullable: true),
                    country_of_residence = table.Column<string>(type: "TEXT", nullable: true),
                    gender = table.Column<string>(type: "TEXT", nullable: true),
                    age = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "LineItems.csv",
                columns: table => new
                {
                    transaction_ID = table.Column<int>(type: "INTEGER", nullable: true),
                    product_ID = table.Column<int>(type: "INTEGER", nullable: true),
                    qty = table.Column<int>(type: "INTEGER", nullable: true),
                    rating = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    transaction_ID = table.Column<double>(type: "REAL", nullable: true),
                    customer_ID = table.Column<double>(type: "REAL", nullable: true),
                    date = table.Column<string>(type: "TEXT", nullable: true),
                    day_of_week = table.Column<string>(type: "TEXT", nullable: true),
                    time = table.Column<double>(type: "REAL", nullable: true),
                    entry_mode = table.Column<string>(type: "TEXT", nullable: true),
                    amount = table.Column<double>(type: "REAL", nullable: true),
                    type_of_transaction = table.Column<string>(type: "TEXT", nullable: true),
                    country_of_transaction = table.Column<string>(type: "TEXT", nullable: true),
                    shipping_address = table.Column<string>(type: "TEXT", nullable: true),
                    bank = table.Column<string>(type: "TEXT", nullable: true),
                    type_of_card = table.Column<string>(type: "TEXT", nullable: true),
                    fraud = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    product_ID = table.Column<double>(type: "REAL", nullable: true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    year = table.Column<double>(type: "REAL", nullable: true),
                    num_parts = table.Column<double>(type: "REAL", nullable: true),
                    price = table.Column<double>(type: "REAL", nullable: true),
                    img_link = table.Column<string>(type: "TEXT", nullable: true),
                    primary_color = table.Column<string>(type: "TEXT", nullable: true),
                    secondary_color = table.Column<string>(type: "TEXT", nullable: true),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    category = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "LineItems.csv");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
