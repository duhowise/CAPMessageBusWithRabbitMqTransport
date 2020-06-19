using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace CAPMessageBusWithRabbitMq.Web.Migrations
{
    public partial class ConstrainColumnToXyPoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Point>(
                name: "StartingPoint",
                table: "Trips",
                type: "geometry (point)",
                nullable: true,
                oldClrType: typeof(Point),
                oldType: "geometry",
                oldNullable: true);

            migrationBuilder.AlterColumn<Point>(
                name: "Destination",
                table: "Trips",
                type: "geometry (point)",
                nullable: true,
                oldClrType: typeof(Point),
                oldType: "geometry",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Point>(
                name: "StartingPoint",
                table: "Trips",
                type: "geometry",
                nullable: true,
                oldClrType: typeof(Point),
                oldType: "geometry (point)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Point>(
                name: "Destination",
                table: "Trips",
                type: "geometry",
                nullable: true,
                oldClrType: typeof(Point),
                oldType: "geometry (point)",
                oldNullable: true);
        }
    }
}
