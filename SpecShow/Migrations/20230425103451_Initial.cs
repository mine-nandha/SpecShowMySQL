using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace SpecShow.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Mobile",
                columns: table => new
                {
                    MobileID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    MobileName = table.Column<string>(type: "longtext", nullable: true),
                    Model = table.Column<string>(type: "longtext", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Brand = table.Column<string>(type: "longtext", nullable: true),
                    ImageUrl = table.Column<string>(type: "longtext", nullable: true),
                    AmazonUrl = table.Column<string>(type: "longtext", nullable: true),
                    FlipkartUrl = table.Column<string>(type: "longtext", nullable: true),
                    ScreenSize = table.Column<string>(type: "longtext", nullable: true),
                    FrontCamera = table.Column<string>(type: "longtext", nullable: true),
                    BackCameras = table.Column<string>(type: "longtext", nullable: true),
                    OS = table.Column<string>(type: "longtext", nullable: true),
                    Sim = table.Column<string>(type: "longtext", nullable: true),
                    Sensors = table.Column<string>(type: "longtext", nullable: true),
                    BatteryCapacity = table.Column<string>(type: "longtext", nullable: true),
                    Colors = table.Column<string>(type: "longtext", nullable: true),
                    Variants = table.Column<string>(type: "longtext", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Weight = table.Column<double>(type: "double", nullable: false),
                    Material = table.Column<string>(type: "longtext", nullable: true),
                    ResistanceCertificate = table.Column<string>(type: "longtext", nullable: true),
                    ScreenType = table.Column<string>(type: "longtext", nullable: true),
                    AspectRatio = table.Column<string>(type: "longtext", nullable: true),
                    Resolution = table.Column<string>(type: "longtext", nullable: true),
                    OtherFeatures = table.Column<string>(type: "longtext", nullable: true),
                    Processor = table.Column<string>(type: "longtext", nullable: true),
                    Nanometer = table.Column<string>(type: "longtext", nullable: true),
                    GPU = table.Column<string>(type: "longtext", nullable: true),
                    AntutuScores = table.Column<int>(type: "int", nullable: false),
                    Bluetooth = table.Column<string>(type: "longtext", nullable: true),
                    ChargingCapacity = table.Column<double>(type: "double", nullable: false),
                    Rating = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mobile", x => x.MobileID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(type: "varchar(255)", nullable: true),
                    FullName = table.Column<string>(type: "longtext", nullable: true),
                    UserEmail = table.Column<string>(type: "longtext", nullable: true),
                    Password = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Favourite",
                columns: table => new
                {
                    FavouritesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    MobileID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favourite", x => x.FavouritesID);
                    table.ForeignKey(
                        name: "FK_Favourite_Mobile_MobileID",
                        column: x => x.MobileID,
                        principalTable: "Mobile",
                        principalColumn: "MobileID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favourite_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Favourite_MobileID",
                table: "Favourite",
                column: "MobileID");

            migrationBuilder.CreateIndex(
                name: "IX_Favourite_UserID",
                table: "Favourite",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                table: "User",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favourite");

            migrationBuilder.DropTable(
                name: "Mobile");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
