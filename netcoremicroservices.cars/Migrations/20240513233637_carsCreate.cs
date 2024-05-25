using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NetCoreMicroservices.Cars.Migrations
{
    /// <inheritdoc />
    public partial class carsCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Doors = table.Column<int>(type: "int", nullable: false),
                    HasGraffiti = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxSpeed = table.Column<double>(type: "float", nullable: false),
                    FuelId = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    ModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "BrandId", "Doors", "FuelId", "HasGraffiti", "MaxSpeed", "ModelId", "Name" },
                values: new object[,]
                {
                    { 1, 1, 0, 1, false, 0.0, 1, "Auto 1" },
                    { 2, 1, 0, 2, false, 0.0, 1, "Auto 2" },
                    { 3, 1, 0, 2, false, 0.0, 2, "Auto 3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
