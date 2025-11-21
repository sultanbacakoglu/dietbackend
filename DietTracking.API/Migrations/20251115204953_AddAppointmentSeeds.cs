using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DietTracking.API.Migrations
{
    /// <inheritdoc />
    public partial class AddAppointmentSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "public",
                table: "AppointmentStatuses",
                columns: new[] { "AppointmentStatusId", "Description" },
                values: new object[,]
                {
                    { 1, "Waiting" },
                    { 2, "Approved" },
                    { 3, "Canceled" },
                    { 4, "Completed" }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "AppointmentTypes",
                columns: new[] { "AppointmentTypeId", "Description" },
                values: new object[,]
                {
                    { 1, "Online" },
                    { 2, "Phone" },
                    { 3, "F2F" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "public",
                table: "AppointmentStatuses",
                keyColumn: "AppointmentStatusId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "AppointmentStatuses",
                keyColumn: "AppointmentStatusId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "AppointmentStatuses",
                keyColumn: "AppointmentStatusId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "AppointmentStatuses",
                keyColumn: "AppointmentStatusId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "AppointmentTypes",
                keyColumn: "AppointmentTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "AppointmentTypes",
                keyColumn: "AppointmentTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "AppointmentTypes",
                keyColumn: "AppointmentTypeId",
                keyValue: 3);
        }
    }
}
