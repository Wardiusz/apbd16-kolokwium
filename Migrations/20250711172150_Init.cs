using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Kolokwium_APBD.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Nursery",
                columns: table => new
                {
                    NurseryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EstablishedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nursery", x => x.NurseryId);
                });

            migrationBuilder.CreateTable(
                name: "TreeSpecies",
                columns: table => new
                {
                    SpeciesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LatinName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GrowthTimeInYears = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreeSpecies", x => x.SpeciesId);
                });

            migrationBuilder.CreateTable(
                name: "SeedlingBatch",
                columns: table => new
                {
                    BatchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NurseryId = table.Column<int>(type: "int", nullable: false),
                    SpeciesId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SownDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReadyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeedlingBatch", x => x.BatchId);
                    table.ForeignKey(
                        name: "FK_SeedlingBatch_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                    table.ForeignKey(
                        name: "FK_SeedlingBatch_Nursery_NurseryId",
                        column: x => x.NurseryId,
                        principalTable: "Nursery",
                        principalColumn: "NurseryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeedlingBatch_TreeSpecies_SpeciesId",
                        column: x => x.SpeciesId,
                        principalTable: "TreeSpecies",
                        principalColumn: "SpeciesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Responsible",
                columns: table => new
                {
                    BatchId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsible", x => new { x.BatchId, x.EmployeeId });
                    table.ForeignKey(
                        name: "FK_Responsible_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Responsible_SeedlingBatch_BatchId",
                        column: x => x.BatchId,
                        principalTable: "SeedlingBatch",
                        principalColumn: "BatchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "FirstName", "HireDate", "LastName" },
                values: new object[,]
                {
                    { 1, "John", new DateTime(2023, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kowalski" },
                    { 2, "Jane", new DateTime(2022, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lis" },
                    { 3, "Julie", new DateTime(2021, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mas" }
                });

            migrationBuilder.InsertData(
                table: "Nursery",
                columns: new[] { "NurseryId", "EstablishedDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Green Forest Nursery" },
                    { 2, new DateTime(2013, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Another Forest Whatever Nursery" },
                    { 3, new DateTime(2009, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dementia Forest Nursery" }
                });

            migrationBuilder.InsertData(
                table: "TreeSpecies",
                columns: new[] { "SpeciesId", "GrowthTimeInYears", "LatinName" },
                values: new object[,]
                {
                    { 1, 7, "Juglans regia" },
                    { 2, 9, "Betula pendula" },
                    { 3, 5, "Robinia fertilis" }
                });

            migrationBuilder.InsertData(
                table: "SeedlingBatch",
                columns: new[] { "BatchId", "EmployeeId", "NurseryId", "Quantity", "ReadyDate", "SownDate", "SpeciesId" },
                values: new object[,]
                {
                    { 1, null, 1, 500, new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 2, null, 1, 5000, null, new DateTime(2020, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 3, null, 2, 100, null, new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 4, null, 3, 1000, new DateTime(2025, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.InsertData(
                table: "Responsible",
                columns: new[] { "BatchId", "EmployeeId", "Role" },
                values: new object[,]
                {
                    { 1, 1, "Supervisor" },
                    { 2, 3, "Tree climber" },
                    { 3, 2, "Planter" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Responsible_EmployeeId",
                table: "Responsible",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SeedlingBatch_EmployeeId",
                table: "SeedlingBatch",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SeedlingBatch_NurseryId",
                table: "SeedlingBatch",
                column: "NurseryId");

            migrationBuilder.CreateIndex(
                name: "IX_SeedlingBatch_SpeciesId",
                table: "SeedlingBatch",
                column: "SpeciesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Responsible");

            migrationBuilder.DropTable(
                name: "SeedlingBatch");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Nursery");

            migrationBuilder.DropTable(
                name: "TreeSpecies");
        }
    }
}
