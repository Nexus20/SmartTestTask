using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartTestTask.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IndustrialPremises",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Area = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndustrialPremises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalEquipmentTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Area = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalEquipmentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IndustrialPremiseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TechnicalEquipmentTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_IndustrialPremises_IndustrialPremiseId",
                        column: x => x.IndustrialPremiseId,
                        principalTable: "IndustrialPremises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contracts_TechnicalEquipmentTypes_TechnicalEquipmentTypeId",
                        column: x => x.TechnicalEquipmentTypeId,
                        principalTable: "TechnicalEquipmentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_IndustrialPremiseId",
                table: "Contracts",
                column: "IndustrialPremiseId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_TechnicalEquipmentTypeId",
                table: "Contracts",
                column: "TechnicalEquipmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_IndustrialPremises_Code_Name",
                table: "IndustrialPremises",
                columns: new[] { "Code", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalEquipmentTypes_Code_Name",
                table: "TechnicalEquipmentTypes",
                columns: new[] { "Code", "Name" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "IndustrialPremises");

            migrationBuilder.DropTable(
                name: "TechnicalEquipmentTypes");
        }
    }
}
