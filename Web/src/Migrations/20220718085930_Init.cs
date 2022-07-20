using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeRabbits.KaoList.Web.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Log = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classfications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classfications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceCodes",
                columns: table => new
                {
                    UserCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeviceCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCodes", x => x.UserCode);
                });

            migrationBuilder.CreateTable(
                name: "Heads",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    NomalizedDisplayName = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "I18ns",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_I18ns", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "KaoListPlaylistPrivacyState",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KaoListPlaylistPrivacyState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KaoListPlaylistShareRole",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KaoListPlaylistShareRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KaoListRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KaoListRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KaoListUserDeleteReasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KaoListUserDeleteReasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KaoListUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NickNameEditedDatetime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProfileIcon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KaoListUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Keys",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Use = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Algorithm = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsX509Certificate = table.Column<bool>(type: "bit", nullable: false),
                    DataProtected = table.Column<bool>(type: "bit", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConsumedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrants", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "PostChartVoteRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostChartVoteRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    Contents = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RemoveRequestTime = table.Column<int>(type: "int", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Blinded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sounds",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sounds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "YouTubePlaylistShared",
                columns: table => new
                {
                    YouTubePlaylistId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YouTubePlaylistShared", x => new { x.YouTubePlaylistId, x.UserId });
                    table.UniqueConstraint("AK_YouTubePlaylistShared_YouTubePlaylistId", x => x.YouTubePlaylistId);
                });

            migrationBuilder.CreateTable(
                name: "ClassficationLocalizeds",
                columns: table => new
                {
                    ClassficationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    I18nName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassficationLocalizeds", x => new { x.I18nName, x.ClassficationId });
                    table.ForeignKey(
                        name: "FK_ClassficationLocalizeds_Classfications_ClassficationId",
                        column: x => x.ClassficationId,
                        principalTable: "Classfications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassficationLocalizeds_I18ns_I18nName",
                        column: x => x.I18nName,
                        principalTable: "I18ns",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HeadLocalizeds",
                columns: table => new
                {
                    HeadId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    I18nName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Displayname = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeadLocalizeds", x => new { x.HeadId, x.I18nName });
                    table.ForeignKey(
                        name: "FK_HeadLocalizeds_Heads_HeadId",
                        column: x => x.HeadId,
                        principalTable: "Heads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeadLocalizeds_I18ns_I18nName",
                        column: x => x.I18nName,
                        principalTable: "I18ns",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KaoListClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KaoListClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KaoListClaims_KaoListRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "KaoListRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KaoListPlaylist",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    PrivacyStatus = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KaoListPlaylist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KaoListPlaylist_KaoListPlaylistPrivacyState_PrivacyStatus",
                        column: x => x.PrivacyStatus,
                        principalTable: "KaoListPlaylistPrivacyState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KaoListPlaylist_KaoListUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "KaoListUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KaoListUserBlinds",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BlinedUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KaoListUserBlinds", x => new { x.UserId, x.BlinedUserId });
                    table.ForeignKey(
                        name: "FK_KaoListUserBlinds_KaoListUsers_BlinedUserId",
                        column: x => x.BlinedUserId,
                        principalTable: "KaoListUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KaoListUserBlinds_KaoListUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "KaoListUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KaoListUserChannels",
                columns: table => new
                {
                    ChannelProvider = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KaoListUserChannels", x => new { x.ChannelProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_KaoListUserChannels_KaoListUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "KaoListUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KaoListUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KaoListUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KaoListUserClaims_KaoListUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "KaoListUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KaoListUserColors",
                columns: table => new
                {
                    Color = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KaoListUserColors", x => new { x.Color, x.UserId });
                    table.ForeignKey(
                        name: "FK_KaoListUserColors_KaoListUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "KaoListUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KaoListUserFollowers",
                columns: table => new
                {
                    FollwerUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FollowUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KaoListUserFollowers", x => new { x.FollowUserId, x.FollwerUserId });
                    table.ForeignKey(
                        name: "FK_KaoListUserFollowers_KaoListUsers_FollowUserId",
                        column: x => x.FollowUserId,
                        principalTable: "KaoListUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KaoListUserFollowers_KaoListUsers_FollwerUserId",
                        column: x => x.FollwerUserId,
                        principalTable: "KaoListUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KaoListUserLanguages",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    I18nName = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KaoListUserLanguages", x => new { x.I18nName, x.UserId });
                    table.ForeignKey(
                        name: "FK_KaoListUserLanguages_I18ns_I18nName",
                        column: x => x.I18nName,
                        principalTable: "I18ns",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KaoListUserLanguages_KaoListUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "KaoListUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KaoListUserLocalizeds",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    i18nName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditedDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KaoListUserLocalizeds", x => new { x.i18nName, x.UserId });
                    table.ForeignKey(
                        name: "FK_KaoListUserLocalizeds_I18ns_i18nName",
                        column: x => x.i18nName,
                        principalTable: "I18ns",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KaoListUserLocalizeds_KaoListUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "KaoListUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KaoListUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KaoListUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_KaoListUserLogins_KaoListUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "KaoListUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KaoListUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KaoListUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_KaoListUserRoles_KaoListRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "KaoListRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KaoListUserRoles_KaoListUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "KaoListUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KaoListUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KaoListUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_KaoListUserTokens_KaoListUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "KaoListUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SignInAttempts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IpAddress = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Successed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignInAttempts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SignInAttempts_KaoListUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "KaoListUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SongSearchLogs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Query = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdentityToken = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongSearchLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SongSearchLogs_KaoListUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "KaoListUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OriginalPosts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OriginalPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OriginalPosts_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostCharts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    VoteNumber = table.Column<byte>(type: "tinyint", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VoteRole = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCharts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostCharts_PostChartVoteRoles_VoteRole",
                        column: x => x.VoteRole,
                        principalTable: "PostChartVoteRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostCharts_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    CommentParent = table.Column<int>(type: "int", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RemoveRequestTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Blinded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostComments_PostComments_CommentParent",
                        column: x => x.CommentParent,
                        principalTable: "PostComments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PostComments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostHeads",
                columns: table => new
                {
                    HeadId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostHeads", x => x.HeadId);
                    table.ForeignKey(
                        name: "FK_PostHeads_Heads_HeadId",
                        column: x => x.HeadId,
                        principalTable: "Heads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostHeads_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostHitLogs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IdentityToken = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostHitLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostHitLogs_KaoListUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "KaoListUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_PostHitLogs_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostLikes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IdentityToken = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostLikes_KaoListUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "KaoListUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_PostLikes_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostReports",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IdentityToken = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostReports_KaoListUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "KaoListUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_PostReports_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostUnlikes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IdentityToken = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostUnlikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostUnlikes_KaoListUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "KaoListUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_PostUnlikes_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostUsers",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostUsers", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_PostUsers_KaoListUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "KaoListUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostUsers_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instrumentals",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    SoundId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instrumentals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instrumentals_Sounds_SoundId",
                        column: x => x.SoundId,
                        principalTable: "Sounds",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SoundPlayLogs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SoundId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdentityToken = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoundPlayLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoundPlayLogs_KaoListUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "KaoListUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SoundPlayLogs_Sounds_SoundId",
                        column: x => x.SoundId,
                        principalTable: "Sounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KaoListPlaylistLocalized",
                columns: table => new
                {
                    I18nName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    PlaylistId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KaoListPlaylistLocalized", x => new { x.PlaylistId, x.I18nName });
                    table.ForeignKey(
                        name: "FK_KaoListPlaylistLocalized_I18ns_I18nName",
                        column: x => x.I18nName,
                        principalTable: "I18ns",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KaoListPlaylistLocalized_KaoListPlaylist_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "KaoListPlaylist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KaoListPlaylistPlayLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityToken = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PlaylistId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KaoListPlaylistPlayLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KaoListPlaylistPlayLog_KaoListPlaylist_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "KaoListPlaylist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KaoListPlaylistPlayLog_KaoListUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "KaoListUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KaoListPlaylistShare",
                columns: table => new
                {
                    PlaylistId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ShareRole = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KaoListPlaylistShare", x => new { x.PlaylistId, x.UserId });
                    table.ForeignKey(
                        name: "FK_KaoListPlaylistShare_KaoListPlaylist_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "KaoListPlaylist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KaoListPlaylistShare_KaoListPlaylistShareRole_ShareRole",
                        column: x => x.ShareRole,
                        principalTable: "KaoListPlaylistShareRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KaoListPlaylistShare_KaoListUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "KaoListUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "YouTubePlaylistSync",
                columns: table => new
                {
                    PlaylistId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    YouTubePlaylistId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YouTubePlaylistSync", x => new { x.YouTubePlaylistId, x.PlaylistId });
                    table.ForeignKey(
                        name: "FK_YouTubePlaylistSync_KaoListPlaylist_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "KaoListPlaylist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_YouTubePlaylistSync_YouTubePlaylistShared_YouTubePlaylistId",
                        column: x => x.YouTubePlaylistId,
                        principalTable: "YouTubePlaylistShared",
                        principalColumn: "YouTubePlaylistId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostChartItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PostChartId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostChartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostChartItems_PostCharts_PostChartId",
                        column: x => x.PostChartId,
                        principalTable: "PostCharts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostChartVotes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PostChartItemId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdentityToken = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostChartVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostChartVotes_PostCharts_PostChartItemId",
                        column: x => x.PostChartItemId,
                        principalTable: "PostCharts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommentReports",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CommentId = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IdentityToken = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentReports_KaoListUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "KaoListUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CommentReports_PostComments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "PostComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OriginalPostComments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PostCommentId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OriginalPostComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OriginalPostComments_PostComments_PostCommentId",
                        column: x => x.PostCommentId,
                        principalTable: "PostComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostCommentUsers",
                columns: table => new
                {
                    PostCommantId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCommentUsers", x => x.PostCommantId);
                    table.ForeignKey(
                        name: "FK_PostCommentUsers_KaoListUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "KaoListUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostCommentUsers_PostComments_PostCommantId",
                        column: x => x.PostCommantId,
                        principalTable: "PostComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstrumentalBlinds",
                columns: table => new
                {
                    InstrumentalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentalBlinds", x => new { x.InstrumentalId, x.UserId });
                    table.ForeignKey(
                        name: "FK_InstrumentalBlinds_Instrumentals_InstrumentalId",
                        column: x => x.InstrumentalId,
                        principalTable: "Instrumentals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstrumentalBlinds_KaoListUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "KaoListUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstrumentalClassifications",
                columns: table => new
                {
                    InstrumentalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClassficationId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentalClassifications", x => new { x.ClassficationId, x.InstrumentalId });
                    table.ForeignKey(
                        name: "FK_InstrumentalClassifications_Classfications_ClassficationId",
                        column: x => x.ClassficationId,
                        principalTable: "Classfications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstrumentalClassifications_Instrumentals_InstrumentalId",
                        column: x => x.InstrumentalId,
                        principalTable: "Instrumentals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstrumentalFollowers",
                columns: table => new
                {
                    InstrumentalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentalFollowers", x => new { x.InstrumentalId, x.UserId });
                    table.ForeignKey(
                        name: "FK_InstrumentalFollowers_Instrumentals_InstrumentalId",
                        column: x => x.InstrumentalId,
                        principalTable: "Instrumentals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstrumentalFollowers_KaoListUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "KaoListUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstrumentalLocalizeds",
                columns: table => new
                {
                    InstrumentalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    I18nName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentalLocalizeds", x => new { x.InstrumentalId, x.I18nName });
                    table.ForeignKey(
                        name: "FK_InstrumentalLocalizeds_I18ns_I18nName",
                        column: x => x.I18nName,
                        principalTable: "I18ns",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstrumentalLocalizeds_Instrumentals_InstrumentalId",
                        column: x => x.InstrumentalId,
                        principalTable: "Instrumentals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lyrics",
                columns: table => new
                {
                    InstrumentalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    Offset = table.Column<TimeSpan>(type: "time", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lyrics", x => new { x.Sequence, x.InstrumentalId });
                    table.ForeignKey(
                        name: "FK_Lyrics_Instrumentals_InstrumentalId",
                        column: x => x.InstrumentalId,
                        principalTable: "Instrumentals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InstrumentalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    SoundId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sings_I18ns_Language",
                        column: x => x.Language,
                        principalTable: "I18ns",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sings_Instrumentals_InstrumentalId",
                        column: x => x.InstrumentalId,
                        principalTable: "Instrumentals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sings_Sounds_SoundId",
                        column: x => x.SoundId,
                        principalTable: "Sounds",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SoundPlaylogDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SoundPlayLogId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CurrentTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoundPlaylogDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoundPlaylogDetails_SoundPlayLogs_SoundPlayLogId",
                        column: x => x.SoundPlayLogId,
                        principalTable: "SoundPlayLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KaoListPlaylistSingItem",
                columns: table => new
                {
                    PlaylistId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SingId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KaoListPlaylistSingItem", x => new { x.PlaylistId, x.SingId });
                    table.ForeignKey(
                        name: "FK_KaoListPlaylistSingItem_KaoListPlaylist_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "KaoListPlaylist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KaoListPlaylistSingItem_Sings_SingId",
                        column: x => x.SingId,
                        principalTable: "Sings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Karaokes",
                columns: table => new
                {
                    Provider = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    No = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    SingId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Karaokes", x => new { x.Provider, x.No });
                    table.ForeignKey(
                        name: "FK_Karaokes_Sings_SingId",
                        column: x => x.SingId,
                        principalTable: "Sings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PopularSings",
                columns: table => new
                {
                    SingId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Score = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PopularSings", x => new { x.Created, x.SingId });
                    table.ForeignKey(
                        name: "FK_PopularSings_Sings_SingId",
                        column: x => x.SingId,
                        principalTable: "Sings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SingBlinds",
                columns: table => new
                {
                    SingId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingBlinds", x => new { x.SingId, x.UserId });
                    table.ForeignKey(
                        name: "FK_SingBlinds_KaoListUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "KaoListUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SingBlinds_Sings_SingId",
                        column: x => x.SingId,
                        principalTable: "Sings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SingFollowers",
                columns: table => new
                {
                    SingId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingFollowers", x => new { x.SingId, x.UserId });
                    table.ForeignKey(
                        name: "FK_SingFollowers_KaoListUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "KaoListUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SingFollowers_Sings_SingId",
                        column: x => x.SingId,
                        principalTable: "Sings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SingUsers",
                columns: table => new
                {
                    SingId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingUsers", x => new { x.SingId, x.UserId });
                    table.ForeignKey(
                        name: "FK_SingUsers_KaoListUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "KaoListUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SingUsers_Sings_SingId",
                        column: x => x.SingId,
                        principalTable: "Sings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TitleSings",
                columns: table => new
                {
                    InstrumentalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SingId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitleSings", x => new { x.UserId, x.InstrumentalId });
                    table.ForeignKey(
                        name: "FK_TitleSings_Instrumentals_InstrumentalId",
                        column: x => x.InstrumentalId,
                        principalTable: "Instrumentals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TitleSings_KaoListUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "KaoListUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TitleSings_Sings_SingId",
                        column: x => x.SingId,
                        principalTable: "Sings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ClassficationLocalizedNameIndex",
                table: "ClassficationLocalizeds",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_ClassficationLocalizeds_ClassficationId",
                table: "ClassficationLocalizeds",
                column: "ClassficationId");

            migrationBuilder.CreateIndex(
                name: "ClassficationNameIndex",
                table: "Classfications",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_CommentReports_CommentId",
                table: "CommentReports",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentReports_UserId",
                table: "CommentReports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_DeviceCode",
                table: "DeviceCodes",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_Expiration",
                table: "DeviceCodes",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "HeadLocalizedDisplayNameIndex",
                table: "HeadLocalizeds",
                column: "Displayname");

            migrationBuilder.CreateIndex(
                name: "IX_HeadLocalizeds_I18nName",
                table: "HeadLocalizeds",
                column: "I18nName");

            migrationBuilder.CreateIndex(
                name: "HeadDisplayNameIndex",
                table: "Heads",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_Heads_NomalizedDisplayName",
                table: "Heads",
                column: "NomalizedDisplayName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "I18nNormalizedNameIndex",
                table: "I18ns",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentalBlinds_UserId",
                table: "InstrumentalBlinds",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentalClassifications_InstrumentalId",
                table: "InstrumentalClassifications",
                column: "InstrumentalId");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentalFollowers_UserId",
                table: "InstrumentalFollowers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "InstrumentalLocalizedTitleIndex",
                table: "InstrumentalLocalizeds",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentalLocalizeds_I18nName",
                table: "InstrumentalLocalizeds",
                column: "I18nName");

            migrationBuilder.CreateIndex(
                name: "InstrumentalTitleIndex",
                table: "Instrumentals",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Instrumentals_SoundId",
                table: "Instrumentals",
                column: "SoundId");

            migrationBuilder.CreateIndex(
                name: "IX_KaoListClaims_RoleId",
                table: "KaoListClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_KaoListPlaylist_PrivacyStatus",
                table: "KaoListPlaylist",
                column: "PrivacyStatus");

            migrationBuilder.CreateIndex(
                name: "IX_KaoListPlaylist_UserId",
                table: "KaoListPlaylist",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_KaoListPlaylistLocalized_I18nName",
                table: "KaoListPlaylistLocalized",
                column: "I18nName");

            migrationBuilder.CreateIndex(
                name: "IX_KaoListPlaylistPlayLog_PlaylistId",
                table: "KaoListPlaylistPlayLog",
                column: "PlaylistId");

            migrationBuilder.CreateIndex(
                name: "IX_KaoListPlaylistPlayLog_UserId",
                table: "KaoListPlaylistPlayLog",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_KaoListPlaylistPrivacyState_NormalizedName",
                table: "KaoListPlaylistPrivacyState",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_KaoListPlaylistShare_ShareRole",
                table: "KaoListPlaylistShare",
                column: "ShareRole");

            migrationBuilder.CreateIndex(
                name: "IX_KaoListPlaylistShare_UserId",
                table: "KaoListPlaylistShare",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_KaoListPlaylistShareRole_NormalizedName",
                table: "KaoListPlaylistShareRole",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_KaoListPlaylistSingItem_SingId",
                table: "KaoListPlaylistSingItem",
                column: "SingId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "KaoListRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_KaoListUserBlinds_BlinedUserId",
                table: "KaoListUserBlinds",
                column: "BlinedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_KaoListUserChannels_UserId",
                table: "KaoListUserChannels",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_KaoListUserClaims_UserId",
                table: "KaoListUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_KaoListUserColors_UserId",
                table: "KaoListUserColors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_KaoListUserFollowers_FollwerUserId",
                table: "KaoListUserFollowers",
                column: "FollwerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_KaoListUserLanguages_UserId",
                table: "KaoListUserLanguages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_KaoListUserLocalizeds_UserId",
                table: "KaoListUserLocalizeds",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_KaoListUserLogins_UserId",
                table: "KaoListUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_KaoListUserRoles_RoleId",
                table: "KaoListUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "KaoListUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "KaoListUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNickNameIndex",
                table: "KaoListUsers",
                column: "NickName");

            migrationBuilder.CreateIndex(
                name: "IX_Karaokes_SingId",
                table: "Karaokes",
                column: "SingId");

            migrationBuilder.CreateIndex(
                name: "KaraokeNameIndex",
                table: "Karaokes",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_Keys_Use",
                table: "Keys",
                column: "Use");

            migrationBuilder.CreateIndex(
                name: "IX_Lyrics_InstrumentalId",
                table: "Lyrics",
                column: "InstrumentalId");

            migrationBuilder.CreateIndex(
                name: "IX_OriginalPostComments_PostCommentId",
                table: "OriginalPostComments",
                column: "PostCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_OriginalPosts_PostId",
                table: "OriginalPosts",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_ConsumedTime",
                table: "PersistedGrants",
                column: "ConsumedTime");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_Expiration",
                table: "PersistedGrants",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_ClientId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "ClientId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_SessionId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "SessionId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_PopularSings_SingId",
                table: "PopularSings",
                column: "SingId");

            migrationBuilder.CreateIndex(
                name: "IX_PostChartItems_PostChartId",
                table: "PostChartItems",
                column: "PostChartId");

            migrationBuilder.CreateIndex(
                name: "PostChartItemTitleIndex",
                table: "PostChartItems",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_PostCharts_PostId",
                table: "PostCharts",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostCharts_VoteRole",
                table: "PostCharts",
                column: "VoteRole");

            migrationBuilder.CreateIndex(
                name: "PostChartTitleIndex",
                table: "PostCharts",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_PostChartVoteRoles_NormalizedName",
                table: "PostChartVoteRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PostChartVotes_PostChartItemId",
                table: "PostChartVotes",
                column: "PostChartItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_CommentParent",
                table: "PostComments",
                column: "CommentParent");

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_PostId",
                table: "PostComments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostCommentUsers_UserId",
                table: "PostCommentUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostHeads_PostId",
                table: "PostHeads",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostHitLogs_PostId",
                table: "PostHitLogs",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostHitLogs_UserId",
                table: "PostHitLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostLikes_PostId",
                table: "PostLikes",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostLikes_UserId",
                table: "PostLikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostReports_PostId",
                table: "PostReports",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostReports_UserId",
                table: "PostReports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostUnlikes_PostId",
                table: "PostUnlikes",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostUnlikes_UserId",
                table: "PostUnlikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostUsers_UserId",
                table: "PostUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SignInAttempts_UserId",
                table: "SignInAttempts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SingBlinds_UserId",
                table: "SingBlinds",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SingFollowers_UserId",
                table: "SingFollowers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Sings_InstrumentalId",
                table: "Sings",
                column: "InstrumentalId");

            migrationBuilder.CreateIndex(
                name: "IX_Sings_Language",
                table: "Sings",
                column: "Language");

            migrationBuilder.CreateIndex(
                name: "IX_Sings_SoundId",
                table: "Sings",
                column: "SoundId");

            migrationBuilder.CreateIndex(
                name: "IX_SingUsers_UserId",
                table: "SingUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SongSearchLogs_UserId",
                table: "SongSearchLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SoundPlaylogDetails_SoundPlayLogId",
                table: "SoundPlaylogDetails",
                column: "SoundPlayLogId");

            migrationBuilder.CreateIndex(
                name: "IX_SoundPlayLogs_SoundId",
                table: "SoundPlayLogs",
                column: "SoundId");

            migrationBuilder.CreateIndex(
                name: "IX_SoundPlayLogs_UserId",
                table: "SoundPlayLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TitleSings_InstrumentalId",
                table: "TitleSings",
                column: "InstrumentalId");

            migrationBuilder.CreateIndex(
                name: "IX_TitleSings_SingId",
                table: "TitleSings",
                column: "SingId");

            migrationBuilder.CreateIndex(
                name: "IX_YouTubePlaylistSync_PlaylistId",
                table: "YouTubePlaylistSync",
                column: "PlaylistId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppLogs");

            migrationBuilder.DropTable(
                name: "ClassficationLocalizeds");

            migrationBuilder.DropTable(
                name: "CommentReports");

            migrationBuilder.DropTable(
                name: "DeviceCodes");

            migrationBuilder.DropTable(
                name: "HeadLocalizeds");

            migrationBuilder.DropTable(
                name: "InstrumentalBlinds");

            migrationBuilder.DropTable(
                name: "InstrumentalClassifications");

            migrationBuilder.DropTable(
                name: "InstrumentalFollowers");

            migrationBuilder.DropTable(
                name: "InstrumentalLocalizeds");

            migrationBuilder.DropTable(
                name: "KaoListClaims");

            migrationBuilder.DropTable(
                name: "KaoListPlaylistLocalized");

            migrationBuilder.DropTable(
                name: "KaoListPlaylistPlayLog");

            migrationBuilder.DropTable(
                name: "KaoListPlaylistShare");

            migrationBuilder.DropTable(
                name: "KaoListPlaylistSingItem");

            migrationBuilder.DropTable(
                name: "KaoListUserBlinds");

            migrationBuilder.DropTable(
                name: "KaoListUserChannels");

            migrationBuilder.DropTable(
                name: "KaoListUserClaims");

            migrationBuilder.DropTable(
                name: "KaoListUserColors");

            migrationBuilder.DropTable(
                name: "KaoListUserDeleteReasons");

            migrationBuilder.DropTable(
                name: "KaoListUserFollowers");

            migrationBuilder.DropTable(
                name: "KaoListUserLanguages");

            migrationBuilder.DropTable(
                name: "KaoListUserLocalizeds");

            migrationBuilder.DropTable(
                name: "KaoListUserLogins");

            migrationBuilder.DropTable(
                name: "KaoListUserRoles");

            migrationBuilder.DropTable(
                name: "KaoListUserTokens");

            migrationBuilder.DropTable(
                name: "Karaokes");

            migrationBuilder.DropTable(
                name: "Keys");

            migrationBuilder.DropTable(
                name: "Lyrics");

            migrationBuilder.DropTable(
                name: "OriginalPostComments");

            migrationBuilder.DropTable(
                name: "OriginalPosts");

            migrationBuilder.DropTable(
                name: "PersistedGrants");

            migrationBuilder.DropTable(
                name: "PopularSings");

            migrationBuilder.DropTable(
                name: "PostChartItems");

            migrationBuilder.DropTable(
                name: "PostChartVotes");

            migrationBuilder.DropTable(
                name: "PostCommentUsers");

            migrationBuilder.DropTable(
                name: "PostHeads");

            migrationBuilder.DropTable(
                name: "PostHitLogs");

            migrationBuilder.DropTable(
                name: "PostLikes");

            migrationBuilder.DropTable(
                name: "PostReports");

            migrationBuilder.DropTable(
                name: "PostUnlikes");

            migrationBuilder.DropTable(
                name: "PostUsers");

            migrationBuilder.DropTable(
                name: "SignInAttempts");

            migrationBuilder.DropTable(
                name: "SingBlinds");

            migrationBuilder.DropTable(
                name: "SingFollowers");

            migrationBuilder.DropTable(
                name: "SingUsers");

            migrationBuilder.DropTable(
                name: "SongSearchLogs");

            migrationBuilder.DropTable(
                name: "SoundPlaylogDetails");

            migrationBuilder.DropTable(
                name: "TitleSings");

            migrationBuilder.DropTable(
                name: "YouTubePlaylistSync");

            migrationBuilder.DropTable(
                name: "Classfications");

            migrationBuilder.DropTable(
                name: "KaoListPlaylistShareRole");

            migrationBuilder.DropTable(
                name: "KaoListRoles");

            migrationBuilder.DropTable(
                name: "PostCharts");

            migrationBuilder.DropTable(
                name: "PostComments");

            migrationBuilder.DropTable(
                name: "Heads");

            migrationBuilder.DropTable(
                name: "SoundPlayLogs");

            migrationBuilder.DropTable(
                name: "Sings");

            migrationBuilder.DropTable(
                name: "KaoListPlaylist");

            migrationBuilder.DropTable(
                name: "YouTubePlaylistShared");

            migrationBuilder.DropTable(
                name: "PostChartVoteRoles");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "I18ns");

            migrationBuilder.DropTable(
                name: "Instrumentals");

            migrationBuilder.DropTable(
                name: "KaoListPlaylistPrivacyState");

            migrationBuilder.DropTable(
                name: "KaoListUsers");

            migrationBuilder.DropTable(
                name: "Sounds");
        }
    }
}
