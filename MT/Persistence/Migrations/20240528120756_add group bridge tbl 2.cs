using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addgroupbridgetbl2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CampaignGroup_Campaigns_CampaignId",
                table: "CampaignGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerGroup_Customers_CustomerId",
                table: "CustomerGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerGroup",
                table: "CustomerGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CampaignGroup",
                table: "CampaignGroup");

            migrationBuilder.RenameTable(
                name: "CustomerGroup",
                newName: "CustomerGroups");

            migrationBuilder.RenameTable(
                name: "CampaignGroup",
                newName: "CampaignGroups");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerGroup_CustomerId",
                table: "CustomerGroups",
                newName: "IX_CustomerGroups_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_CampaignGroup_CampaignId",
                table: "CampaignGroups",
                newName: "IX_CampaignGroups_CampaignId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerGroups",
                table: "CustomerGroups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CampaignGroups",
                table: "CampaignGroups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignGroups_Campaigns_CampaignId",
                table: "CampaignGroups",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerGroups_Customers_CustomerId",
                table: "CustomerGroups",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CampaignGroups_Campaigns_CampaignId",
                table: "CampaignGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerGroups_Customers_CustomerId",
                table: "CustomerGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerGroups",
                table: "CustomerGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CampaignGroups",
                table: "CampaignGroups");

            migrationBuilder.RenameTable(
                name: "CustomerGroups",
                newName: "CustomerGroup");

            migrationBuilder.RenameTable(
                name: "CampaignGroups",
                newName: "CampaignGroup");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerGroups_CustomerId",
                table: "CustomerGroup",
                newName: "IX_CustomerGroup_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_CampaignGroups_CampaignId",
                table: "CampaignGroup",
                newName: "IX_CampaignGroup_CampaignId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerGroup",
                table: "CustomerGroup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CampaignGroup",
                table: "CampaignGroup",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignGroup_Campaigns_CampaignId",
                table: "CampaignGroup",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerGroup_Customers_CustomerId",
                table: "CustomerGroup",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
