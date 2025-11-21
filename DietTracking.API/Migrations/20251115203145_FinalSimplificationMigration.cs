using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DietTracking.API.Migrations
{
    /// <inheritdoc />
    public partial class FinalSimplificationMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Experts_ExpertId",
                schema: "public",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_ClientGroups_ClientGroupId",
                schema: "public",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_ClientTypes_ClientTypeId",
                schema: "public",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Experts_ExpertId",
                schema: "public",
                table: "Clients");

            migrationBuilder.DropTable(
                name: "ClientEmergencies",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ClientGroups",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ClientTypes",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ClosedDates",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Payments",
                schema: "public");

            migrationBuilder.DropTable(
                name: "UserProfiles",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Experts",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Institutions",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_Clients_ClientGroupId",
                schema: "public",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_ClientTypeId",
                schema: "public",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_ExpertId",
                schema: "public",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ExpertId",
                schema: "public",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "public",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ClientGroupId",
                schema: "public",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ClientTypeId",
                schema: "public",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ExpertId",
                schema: "public",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ExpertId",
                schema: "public",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                schema: "public",
                table: "Appointments");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "public",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                schema: "public",
                table: "Users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "public",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                schema: "public",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "public",
                table: "Users",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientGroupId",
                schema: "public",
                table: "Clients",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientTypeId",
                schema: "public",
                table: "Clients",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExpertId",
                schema: "public",
                table: "Clients",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExpertId",
                schema: "public",
                table: "Appointments",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentId",
                schema: "public",
                table: "Appointments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClientEmergencies",
                schema: "public",
                columns: table => new
                {
                    ClientEmergencyId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<int>(type: "integer", nullable: true),
                    ContactName = table.Column<string>(type: "text", nullable: true),
                    ContactPhone = table.Column<string>(type: "text", nullable: true),
                    Relationship = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientEmergencies", x => x.ClientEmergencyId);
                    table.ForeignKey(
                        name: "FK_ClientEmergencies_Clients_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "public",
                        principalTable: "Clients",
                        principalColumn: "ClientId");
                });

            migrationBuilder.CreateTable(
                name: "ClientGroups",
                schema: "public",
                columns: table => new
                {
                    ClientGroupId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientGroups", x => x.ClientGroupId);
                });

            migrationBuilder.CreateTable(
                name: "ClientTypes",
                schema: "public",
                columns: table => new
                {
                    ClientTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientTypes", x => x.ClientTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Institutions",
                schema: "public",
                columns: table => new
                {
                    InstitutionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institutions", x => x.InstitutionId);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                schema: "public",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppointmentId = table.Column<int>(type: "integer", nullable: true),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PaymentMethod = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalSchema: "public",
                        principalTable: "Appointments",
                        principalColumn: "AppointmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                schema: "public",
                columns: table => new
                {
                    UserProfileId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    BirthPlace = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<string>(type: "text", nullable: true),
                    IdNumber = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Nationality = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.UserProfileId);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Experts",
                schema: "public",
                columns: table => new
                {
                    ExpertId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InstitutionId = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    DefaultPaymentAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experts", x => x.ExpertId);
                    table.ForeignKey(
                        name: "FK_Experts_Institutions_InstitutionId",
                        column: x => x.InstitutionId,
                        principalSchema: "public",
                        principalTable: "Institutions",
                        principalColumn: "InstitutionId");
                    table.ForeignKey(
                        name: "FK_Experts_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "ClosedDates",
                schema: "public",
                columns: table => new
                {
                    ClosedDateId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExpertId = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClosedDates", x => x.ClosedDateId);
                    table.ForeignKey(
                        name: "FK_ClosedDates_Experts_ExpertId",
                        column: x => x.ExpertId,
                        principalSchema: "public",
                        principalTable: "Experts",
                        principalColumn: "ExpertId");
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "ClientGroups",
                columns: new[] { "ClientGroupId", "Description" },
                values: new object[,]
                {
                    { 1, "Group A" },
                    { 2, "Group B" }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "ClientTypes",
                columns: new[] { "ClientTypeId", "Description" },
                values: new object[,]
                {
                    { 1, "Regular" },
                    { 2, "Premium" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ClientGroupId",
                schema: "public",
                table: "Clients",
                column: "ClientGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ClientTypeId",
                schema: "public",
                table: "Clients",
                column: "ClientTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ExpertId",
                schema: "public",
                table: "Clients",
                column: "ExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ExpertId",
                schema: "public",
                table: "Appointments",
                column: "ExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientEmergencies_ClientId",
                schema: "public",
                table: "ClientEmergencies",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClosedDates_ExpertId",
                schema: "public",
                table: "ClosedDates",
                column: "ExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_Experts_InstitutionId",
                schema: "public",
                table: "Experts",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Experts_UserId",
                schema: "public",
                table: "Experts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_AppointmentId",
                schema: "public",
                table: "Payments",
                column: "AppointmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserId",
                schema: "public",
                table: "UserProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Experts_ExpertId",
                schema: "public",
                table: "Appointments",
                column: "ExpertId",
                principalSchema: "public",
                principalTable: "Experts",
                principalColumn: "ExpertId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_ClientGroups_ClientGroupId",
                schema: "public",
                table: "Clients",
                column: "ClientGroupId",
                principalSchema: "public",
                principalTable: "ClientGroups",
                principalColumn: "ClientGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_ClientTypes_ClientTypeId",
                schema: "public",
                table: "Clients",
                column: "ClientTypeId",
                principalSchema: "public",
                principalTable: "ClientTypes",
                principalColumn: "ClientTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Experts_ExpertId",
                schema: "public",
                table: "Clients",
                column: "ExpertId",
                principalSchema: "public",
                principalTable: "Experts",
                principalColumn: "ExpertId");
        }
    }
}
