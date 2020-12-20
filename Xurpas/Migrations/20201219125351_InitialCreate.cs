using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Xurpas.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntryPoint",
                columns: table => new
                {
                    EntryPointID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntryPointName = table.Column<string>(maxLength: 250, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryPoint", x => x.EntryPointID);
                });

            migrationBuilder.CreateTable(
                name: "Parking",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlateNumber = table.Column<string>(nullable: false),
                    ParkingSpaceID = table.Column<int>(nullable: false),
                    ParkingTypeCode = table.Column<string>(nullable: false),
                    EntryPointName = table.Column<string>(nullable: false),
                    TimeIn = table.Column<DateTime>(nullable: false),
                    TimeOut = table.Column<DateTime>(nullable: false),
                    IsReturning = table.Column<bool>(nullable: false),
                    NumberOfHours = table.Column<int>(nullable: false),
                    HourlyRate = table.Column<decimal>(nullable: false),
                    TotalParkingFees = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parking", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ParkingSpacePerEntryPoint",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntryPointName = table.Column<string>(nullable: false),
                    ParkingSpaceID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSpacePerEntryPoint", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ParkingType",
                columns: table => new
                {
                    ParkingTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParkingCode = table.Column<string>(maxLength: 2, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    Remarks = table.Column<string>(maxLength: 250, nullable: true),
                    HourLyRate = table.Column<decimal>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingType", x => x.ParkingTypeID);
                });

            migrationBuilder.CreateTable(
                name: "ParkingSpace",
                columns: table => new
                {
                    ParkingSpaceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParkingTypeCode = table.Column<string>(nullable: false),
                    IsAvailable = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ParkingTypeID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSpace", x => x.ParkingSpaceID);
                    table.ForeignKey(
                        name: "FK_ParkingSpace_ParkingType_ParkingTypeID",
                        column: x => x.ParkingTypeID,
                        principalTable: "ParkingType",
                        principalColumn: "ParkingTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpace_ParkingTypeID",
                table: "ParkingSpace",
                column: "ParkingTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntryPoint");

            migrationBuilder.DropTable(
                name: "Parking");

            migrationBuilder.DropTable(
                name: "ParkingSpace");

            migrationBuilder.DropTable(
                name: "ParkingSpacePerEntryPoint");

            migrationBuilder.DropTable(
                name: "ParkingType");
        }
    }
}
