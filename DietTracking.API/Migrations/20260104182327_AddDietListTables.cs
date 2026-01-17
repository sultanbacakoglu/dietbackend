using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DietTracking.API.Migrations
{
    /// <inheritdoc />
    public partial class AddDietListTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DietLists",
                schema: "public",
                columns: table => new
                {
                    DietListId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ClientId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietLists", x => x.DietListId);
                    table.ForeignKey(
                        name: "FK_DietLists_Clients_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "public",
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DietListDetails",
                schema: "public",
                columns: table => new
                {
                    DetailId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Day = table.Column<string>(type: "text", nullable: false),
                    Meal = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    DietListId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietListDetails", x => x.DetailId);
                    table.ForeignKey(
                        name: "FK_DietListDetails_DietLists_DietListId",
                        column: x => x.DietListId,
                        principalSchema: "public",
                        principalTable: "DietLists",
                        principalColumn: "DietListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DietListDetails_DietListId",
                schema: "public",
                table: "DietListDetails",
                column: "DietListId");

            migrationBuilder.CreateIndex(
                name: "IX_DietLists_ClientId",
                schema: "public",
                table: "DietLists",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DietListDetails",
                schema: "public");

            migrationBuilder.DropTable(
                name: "DietLists",
                schema: "public");
        }
    }
}
