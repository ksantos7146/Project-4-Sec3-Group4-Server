using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_IV.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "gender",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gender", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "state",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_state", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    bio = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gender_id = table.Column<int>(type: "int", nullable: true),
                    state_id = table.Column<int>(type: "int", nullable: false),
                    age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_users_gender_gender_id",
                        column: x => x.gender_id,
                        principalTable: "gender",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_users_state_state_id",
                        column: x => x.state_id,
                        principalTable: "state",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    image_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    image_data = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    uploaded_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.image_id);
                    table.ForeignKey(
                        name: "FK_images_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "likes",
                columns: table => new
                {
                    like_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    liker_id = table.Column<int>(type: "int", nullable: false),
                    liked_id = table.Column<int>(type: "int", nullable: false),
                    liked_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_likes", x => x.like_id);
                    table.ForeignKey(
                        name: "FK_likes_users_liked_id",
                        column: x => x.liked_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_likes_users_liker_id",
                        column: x => x.liker_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "matches",
                columns: table => new
                {
                    match_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user1_id = table.Column<int>(type: "int", nullable: false),
                    user2_id = table.Column<int>(type: "int", nullable: false),
                    matched_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matches", x => x.match_id);
                    table.ForeignKey(
                        name: "FK_matches_users_user1_id",
                        column: x => x.user1_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_matches_users_user2_id",
                        column: x => x.user2_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "preferences",
                columns: table => new
                {
                    preference_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    min_age = table.Column<int>(type: "int", nullable: false, defaultValue: 18),
                    max_age = table.Column<int>(type: "int", nullable: false, defaultValue: 100),
                    gender_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_preferences", x => x.preference_id);
                    table.ForeignKey(
                        name: "FK_preferences_gender_gender_id",
                        column: x => x.gender_id,
                        principalTable: "gender",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_preferences_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_images_user_id",
                table: "images",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_likes_liked_id",
                table: "likes",
                column: "liked_id");

            migrationBuilder.CreateIndex(
                name: "IX_likes_liker_id",
                table: "likes",
                column: "liker_id");

            migrationBuilder.CreateIndex(
                name: "IX_matches_user1_id",
                table: "matches",
                column: "user1_id");

            migrationBuilder.CreateIndex(
                name: "IX_matches_user2_id",
                table: "matches",
                column: "user2_id");

            migrationBuilder.CreateIndex(
                name: "IX_preferences_gender_id",
                table: "preferences",
                column: "gender_id");

            migrationBuilder.CreateIndex(
                name: "IX_preferences_user_id",
                table: "preferences",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_gender_id",
                table: "users",
                column: "gender_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_state_id",
                table: "users",
                column: "state_id");
            // Seed Gender data
            migrationBuilder.InsertData(
                table: "gender",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Male" },
                    { 2, "Female" },
                    { 3, "Other" }
                });

            // Seed State data
            migrationBuilder.InsertData(
                table: "state",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Online" },
                    { 2, "Offline" },
                    { 3, "Paused" }
                });

            // Add test users
            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "user_id", "username", "password", "bio", "gender_id", "state_id", "age" },
                values: new object[,]
                {
                    { 1, "john_doe", "john_doe", "Software developer who loves coding and coffee", 1, 1, 28 },
                    { 2, "jane_smith", "jane_smith", "Adventure seeker and travel enthusiast", 2, 1, 25 },
                    { 3, "alex_wilson", "alex_wilson", "Photographer and nature lover", 1, 2, 32 },
                    { 4, "emily_brown", "emily_brown", "Bookworm and tea addict", 2, 1, 27 },
                    { 5, "sam_taylor", "sam_taylor", "Music producer and guitarist", 3, 3, 30 }
                });

            // Add preferences
            migrationBuilder.InsertData(
                table: "preferences",
                columns: new[] { "preference_id", "user_id", "min_age", "max_age", "gender_id" },
                values: new object[,]
                {
                    { 1, 1, 23, 32, 2 },
                    { 2, 2, 25, 35, 1 },
                    { 3, 3, 25, 35, 2 },
                    { 4, 4, 25, 40, 1 },
                    { 5, 5, 23, 35, null }
                });

            // Add likes with fixed dates
            var baseDate = new DateTime(2024, 3, 22);
            migrationBuilder.InsertData(
                table: "likes",
                columns: new[] { "like_id", "liker_id", "liked_id", "liked_at" },
                values: new object[,]
                {
                    { 1, 1, 2, baseDate.AddDays(-5) },
                    { 2, 2, 1, baseDate.AddDays(-4) },
                    { 3, 3, 4, baseDate.AddDays(-3) },
                    { 4, 4, 1, baseDate.AddDays(-2) },
                    { 5, 5, 2, baseDate.AddDays(-1) }
                });

            // Add matches with fixed dates
            migrationBuilder.InsertData(
                table: "matches",
                columns: new[] { "match_id", "user1_id", "user2_id", "matched_at" },
                values: new object[,]
                {
                    { 1, 1, 2, baseDate.AddDays(-4) } // John and Jane matched
                });

            // Add sample images with fixed dates
            migrationBuilder.InsertData(
                table: "images",
                columns: new[] { "image_id", "image_data", "uploaded_at", "user_id" },
                values: new object[,]
                {
                    { 1, "https://example.com/profile1.jpg", baseDate.AddDays(-10), 1 },
                    { 2, "https://example.com/profile2.jpg", baseDate.AddDays(-9), 1 },
                    { 3, "https://example.com/profile3.jpg", baseDate.AddDays(-8), 2 },
                    { 4, "https://example.com/profile4.jpg", baseDate.AddDays(-7), 3 },
                    { 5, "https://example.com/profile5.jpg", baseDate.AddDays(-6), 4 },
                    { 6, "https://example.com/profile6.jpg", baseDate.AddDays(-5), 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DropTable(
                name: "likes");

            migrationBuilder.DropTable(
                name: "matches");

            migrationBuilder.DropTable(
                name: "preferences");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "gender");

            migrationBuilder.DropTable(
                name: "state");
        }
    }
}
