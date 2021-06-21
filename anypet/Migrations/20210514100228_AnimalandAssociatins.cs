using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdoptNet.Migrations
{
    public partial class AnimalandAssociatins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdoptionDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdoptionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LocationAdopt = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdoptionDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Association",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<long>(type: "bigint", nullable: false),
                    Location = table.Column<int>(type: "int", nullable: false),
                    EmailOfUser = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Association", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserReg",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", maxLength: 9, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailOfUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrivateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThereIsAnimal = table.Column<bool>(type: "bit", nullable: false),
                    DateOfCreate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReg", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdoptionDaysAssociation",
                columns: table => new
                {
                    AdoptionDaysId = table.Column<int>(type: "int", nullable: false),
                    associationsAdoptID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdoptionDaysAssociation", x => new { x.AdoptionDaysId, x.associationsAdoptID });
                    table.ForeignKey(
                        name: "FK_AdoptionDaysAssociation_AdoptionDays_AdoptionDaysId",
                        column: x => x.AdoptionDaysId,
                        principalTable: "AdoptionDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdoptionDaysAssociation_Association_associationsAdoptID",
                        column: x => x.associationsAdoptID,
                        principalTable: "Association",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Animal",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<int>(type: "int", nullable: false),
                    IdAssociation = table.Column<int>(type: "int", nullable: false),
                    associationID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animal", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Animal_Association_associationID",
                        column: x => x.associationID,
                        principalTable: "Association",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssociationImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssociationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssociationImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssociationImage_Association_AssociationId",
                        column: x => x.AssociationId,
                        principalTable: "Association",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimalImage",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnimalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalImage", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AnimalImage_Animal_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animal",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionDaysAssociation_associationsAdoptID",
                table: "AdoptionDaysAssociation",
                column: "associationsAdoptID");

            migrationBuilder.CreateIndex(
                name: "IX_Animal_associationID",
                table: "Animal",
                column: "associationID");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalImage_AnimalId",
                table: "AnimalImage",
                column: "AnimalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssociationImage_AssociationId",
                table: "AssociationImage",
                column: "AssociationId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdoptionDaysAssociation");

            migrationBuilder.DropTable(
                name: "AnimalImage");

            migrationBuilder.DropTable(
                name: "AssociationImage");

            migrationBuilder.DropTable(
                name: "UserReg");

            migrationBuilder.DropTable(
                name: "AdoptionDays");

            migrationBuilder.DropTable(
                name: "Animal");

            migrationBuilder.DropTable(
                name: "Association");
        }
    }
}
