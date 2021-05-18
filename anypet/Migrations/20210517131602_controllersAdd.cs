using Microsoft.EntityFrameworkCore.Migrations;

namespace AdoptNet.Migrations
{
    public partial class controllersAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionDaysAssociation_Association_associationsAdoptID",
                table: "AdoptionDaysAssociation");

            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Association_associationID",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "IdAssociation",
                table: "Animal");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Association",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "AnimalImage",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "associationID",
                table: "Animal",
                newName: "AssociationId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Animal",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Animal_associationID",
                table: "Animal",
                newName: "IX_Animal_AssociationId");

            migrationBuilder.RenameColumn(
                name: "associationsAdoptID",
                table: "AdoptionDaysAssociation",
                newName: "AssociationsId");

            migrationBuilder.RenameIndex(
                name: "IX_AdoptionDaysAssociation_associationsAdoptID",
                table: "AdoptionDaysAssociation",
                newName: "IX_AdoptionDaysAssociation_AssociationsId");

            migrationBuilder.AlterColumn<int>(
                name: "AssociationId",
                table: "Animal",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionDaysAssociation_Association_AssociationsId",
                table: "AdoptionDaysAssociation",
                column: "AssociationsId",
                principalTable: "Association",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Association_AssociationId",
                table: "Animal",
                column: "AssociationId",
                principalTable: "Association",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionDaysAssociation_Association_AssociationsId",
                table: "AdoptionDaysAssociation");

            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Association_AssociationId",
                table: "Animal");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Association",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AnimalImage",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "AssociationId",
                table: "Animal",
                newName: "associationID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Animal",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Animal_AssociationId",
                table: "Animal",
                newName: "IX_Animal_associationID");

            migrationBuilder.RenameColumn(
                name: "AssociationsId",
                table: "AdoptionDaysAssociation",
                newName: "associationsAdoptID");

            migrationBuilder.RenameIndex(
                name: "IX_AdoptionDaysAssociation_AssociationsId",
                table: "AdoptionDaysAssociation",
                newName: "IX_AdoptionDaysAssociation_associationsAdoptID");

            migrationBuilder.AlterColumn<int>(
                name: "associationID",
                table: "Animal",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "IdAssociation",
                table: "Animal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionDaysAssociation_Association_associationsAdoptID",
                table: "AdoptionDaysAssociation",
                column: "associationsAdoptID",
                principalTable: "Association",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Association_associationID",
                table: "Animal",
                column: "associationID",
                principalTable: "Association",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
