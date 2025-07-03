using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adduniquecolumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomNumber_HotelId",
                table: "Rooms",
                columns: new[] { "RoomNumber", "HotelId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rooms_RoomNumber_HotelId",
                table: "Rooms");
        }
    }
}
