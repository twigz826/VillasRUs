using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VillasRUs.Infrastructure.Migrations;

/// <inheritdoc />
public partial class CreateDatabase : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "users",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                first_name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                last_name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                email = table.Column<string>(type: "character varying(350)", maxLength: 350, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_users", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "villas",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                description = table.Column<string>(type: "character varying(1500)", maxLength: 1500, nullable: false),
                address_country = table.Column<string>(type: "text", nullable: false),
                address_county = table.Column<string>(type: "text", nullable: false),
                address_postcode = table.Column<string>(type: "text", nullable: false),
                address_city = table.Column<string>(type: "text", nullable: false),
                address_street = table.Column<string>(type: "text", nullable: false),
                price_amount = table.Column<decimal>(type: "numeric", nullable: false),
                price_currency = table.Column<string>(type: "text", nullable: false),
                cleaning_fee_amount = table.Column<decimal>(type: "numeric", nullable: false),
                cleaning_fee_currency = table.Column<string>(type: "text", nullable: false),
                last_booked_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                amenities = table.Column<int[]>(type: "integer[]", nullable: false),
                xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_villas", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "bookings",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                villa_id = table.Column<Guid>(type: "uuid", nullable: false),
                user_id = table.Column<Guid>(type: "uuid", nullable: false),
                duration_start = table.Column<DateOnly>(type: "date", nullable: false),
                duration_end = table.Column<DateOnly>(type: "date", nullable: false),
                price_for_period_amount = table.Column<decimal>(type: "numeric", nullable: false),
                price_for_period_currency = table.Column<string>(type: "text", nullable: false),
                cleaning_fee_amount = table.Column<decimal>(type: "numeric", nullable: false),
                cleaning_fee_currency = table.Column<string>(type: "text", nullable: false),
                amenities_fee_amount = table.Column<decimal>(type: "numeric", nullable: false),
                amenities_fee_currency = table.Column<string>(type: "text", nullable: false),
                total_price_amount = table.Column<decimal>(type: "numeric", nullable: false),
                total_price_currency = table.Column<string>(type: "text", nullable: false),
                status = table.Column<int>(type: "integer", nullable: false),
                created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                confirmed_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                rejected_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                completed_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                cancelled_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_bookings", x => x.id);
                table.ForeignKey(
                    name: "fk_bookings_user_user_id",
                    column: x => x.user_id,
                    principalTable: "users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_bookings_villa_villa_id",
                    column: x => x.villa_id,
                    principalTable: "villas",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "reviews",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                villa_id = table.Column<Guid>(type: "uuid", nullable: false),
                booking_id = table.Column<Guid>(type: "uuid", nullable: false),
                user_id = table.Column<Guid>(type: "uuid", nullable: false),
                rating = table.Column<int>(type: "integer", nullable: false),
                comment = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_reviews", x => x.id);
                table.ForeignKey(
                    name: "fk_reviews_bookings_booking_id",
                    column: x => x.booking_id,
                    principalTable: "bookings",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_reviews_user_user_id",
                    column: x => x.user_id,
                    principalTable: "users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_reviews_villa_villa_id",
                    column: x => x.villa_id,
                    principalTable: "villas",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_bookings_user_id",
            table: "bookings",
            column: "user_id");

        migrationBuilder.CreateIndex(
            name: "ix_bookings_villa_id",
            table: "bookings",
            column: "villa_id");

        migrationBuilder.CreateIndex(
            name: "ix_reviews_booking_id",
            table: "reviews",
            column: "booking_id");

        migrationBuilder.CreateIndex(
            name: "ix_reviews_user_id",
            table: "reviews",
            column: "user_id");

        migrationBuilder.CreateIndex(
            name: "ix_reviews_villa_id",
            table: "reviews",
            column: "villa_id");

        migrationBuilder.CreateIndex(
            name: "ix_users_email",
            table: "users",
            column: "email",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "reviews");

        migrationBuilder.DropTable(
            name: "bookings");

        migrationBuilder.DropTable(
            name: "users");

        migrationBuilder.DropTable(
            name: "villas");
    }
}
