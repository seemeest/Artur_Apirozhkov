using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Artur_Apirozhkov.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    postsCount = table.Column<int>(type: "int", nullable: true),
                    CountLikes = table.Column<int>(type: "int", nullable: true),
                    AverageLikes = table.Column<int>(type: "int", nullable: true),
                    AvatarPhotoUrl = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Quotes = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Status = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Vkid = table.Column<long>(type: "bigint", nullable: true),
                    About = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Activities = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Interest = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    friendsCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Friend",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vkUserid = table.Column<long>(type: "bigint", nullable: true),
                    vkFriendId = table.Column<long>(type: "bigint", nullable: true),
                    City = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    educationID = table.Column<int>(type: "int", nullable: true),
                    Job = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    UserMidelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friend", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Friend_UserModel",
                        column: x => x.UserMidelId,
                        principalTable: "UserModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vkUserid = table.Column<long>(type: "bigint", nullable: true),
                    vkGroupId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    UserMidelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Group_UserModel",
                        column: x => x.UserMidelId,
                        principalTable: "UserModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vkUserid = table.Column<long>(type: "bigint", nullable: true),
                    vkPhotoId = table.Column<long>(type: "bigint", nullable: true),
                    Url = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Text = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    LikeCount = table.Column<int>(type: "int", nullable: true),
                    RepostCount = table.Column<int>(type: "int", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    UserMidelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPhoto_UserModel",
                        column: x => x.UserMidelId,
                        principalTable: "UserModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WallPost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VkUserid = table.Column<long>(type: "bigint", nullable: true),
                    vkPostID = table.Column<long>(type: "bigint", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    Text = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    author = table.Column<long>(type: "bigint", nullable: true),
                    CountLikes = table.Column<int>(type: "int", nullable: true),
                    CountReposts = table.Column<int>(type: "int", nullable: true),
                    friend = table.Column<bool>(type: "bit", nullable: true),
                    UserMidelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WallPost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WallPost_UserModel",
                        column: x => x.UserMidelId,
                        principalTable: "UserModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Education",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniversityName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    FacultyName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    EducationForm = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    EducationStatus = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    UserMidelId = table.Column<int>(type: "int", nullable: true),
                    FriendId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Education", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Education_Friend",
                        column: x => x.FriendId,
                        principalTable: "Friend",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Education_UserModel",
                        column: x => x.UserMidelId,
                        principalTable: "UserModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Education_FriendId",
                table: "Education",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_Education_UserMidelId",
                table: "Education",
                column: "UserMidelId");

            migrationBuilder.CreateIndex(
                name: "IX_Friend_UserMidelId",
                table: "Friend",
                column: "UserMidelId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_UserMidelId",
                table: "Group",
                column: "UserMidelId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPhoto_UserMidelId",
                table: "UserPhoto",
                column: "UserMidelId");

            migrationBuilder.CreateIndex(
                name: "IX_WallPost_UserMidelId",
                table: "WallPost",
                column: "UserMidelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Education");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "UserPhoto");

            migrationBuilder.DropTable(
                name: "WallPost");

            migrationBuilder.DropTable(
                name: "Friend");

            migrationBuilder.DropTable(
                name: "UserModel");
        }
    }
}
