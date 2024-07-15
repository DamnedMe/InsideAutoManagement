using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsideAutoManagement.Migrations
{
    /// <inheritdoc />
    public partial class initial_create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarDealers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PIVA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarDealers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<long>(type: "bigint", nullable: false),
                    FuelId = table.Column<int>(type: "int", nullable: false),
                    Plate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Km = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    GearShiftId = table.Column<int>(type: "int", nullable: false),
                    SaleStatusId = table.Column<int>(type: "int", nullable: false),
                    StartPrice = table.Column<double>(type: "float", nullable: false),
                    CarDealerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_CarDealers_CarDealerId",
                        column: x => x.CarDealerId,
                        principalTable: "CarDealers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Configurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarDealerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValueType = table.Column<int>(type: "int", nullable: false),
                    StringValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberValue = table.Column<double>(type: "float", nullable: true),
                    BoolValue = table.Column<bool>(type: "bit", nullable: true),
                    DateTimeValue = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Configurations_CarDealers_CarDealerId",
                        column: x => x.CarDealerId,
                        principalTable: "CarDealers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FolderCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FolderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarDealerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolderCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FolderCategories_CarDealers_CarDealerId",
                        column: x => x.CarDealerId,
                        principalTable: "CarDealers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpeningHoursShifts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CarDealerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpeningHoursShifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpeningHoursShifts_CarDealers_CarDealerId",
                        column: x => x.CarDealerId,
                        principalTable: "CarDealers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FolderCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ranking = table.Column<int>(type: "int", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CarDealerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_CarDealers_CarDealerId",
                        column: x => x.CarDealerId,
                        principalTable: "CarDealers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documents_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Documents_FolderCategories_FolderCategoryId",
                        column: x => x.FolderCategoryId,
                        principalTable: "FolderCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarDealerId",
                table: "Cars",
                column: "CarDealerId");

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_CarDealerId",
                table: "Configurations",
                column: "CarDealerId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_CarDealerId",
                table: "Documents",
                column: "CarDealerId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_CarId",
                table: "Documents",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_FolderCategoryId",
                table: "Documents",
                column: "FolderCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FolderCategories_CarDealerId",
                table: "FolderCategories",
                column: "CarDealerId");

            migrationBuilder.CreateIndex(
                name: "IX_OpeningHoursShifts_CarDealerId",
                table: "OpeningHoursShifts",
                column: "CarDealerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configurations");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "OpeningHoursShifts");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "FolderCategories");

            migrationBuilder.DropTable(
                name: "CarDealers");
        }
    }
}
