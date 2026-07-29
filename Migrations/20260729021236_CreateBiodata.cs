using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDIITechincalInterview.Migrations
{
    /// <inheritdoc />
    public partial class CreateBiodata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Biodatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PosisiDilamar = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nama = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NomorKtp = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TempatLahir = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TanggalLahir = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    JenisKelamin = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Agama = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GolonganDarah = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AlamatKtp = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AlamatTinggal = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NomorTelepon = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    KontakDarurat = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Skill = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BersediaDitempatkan = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PenghasilanDiharapkan = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TandaTanganDigital = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Biodatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Biodatas_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PendidikanTerakhirs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BiodataId = table.Column<int>(type: "int", nullable: false),
                    JenjangPendidikan = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NamaInstitusi = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Jurusan = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TahunLulus = table.Column<int>(type: "int", nullable: false),
                    IPK = table.Column<decimal>(type: "decimal(65,30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendidikanTerakhirs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PendidikanTerakhirs_Biodatas_BiodataId",
                        column: x => x.BiodataId,
                        principalTable: "Biodatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RiwayatPekerjaans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BiodataId = table.Column<int>(type: "int", nullable: false),
                    NamaPerusahaan = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PosisiTerakhir = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PendapatanTerakhir = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Tahun = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiwayatPekerjaans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiwayatPekerjaans_Biodatas_BiodataId",
                        column: x => x.BiodataId,
                        principalTable: "Biodatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RiwayatPelatihans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BiodataId = table.Column<int>(type: "int", nullable: false),
                    NamaKursusSeminar = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MemilikiSertifikat = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Tahun = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiwayatPelatihans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiwayatPelatihans_Biodatas_BiodataId",
                        column: x => x.BiodataId,
                        principalTable: "Biodatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Biodatas_UserId",
                table: "Biodatas",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PendidikanTerakhirs_BiodataId",
                table: "PendidikanTerakhirs",
                column: "BiodataId");

            migrationBuilder.CreateIndex(
                name: "IX_RiwayatPekerjaans_BiodataId",
                table: "RiwayatPekerjaans",
                column: "BiodataId");

            migrationBuilder.CreateIndex(
                name: "IX_RiwayatPelatihans_BiodataId",
                table: "RiwayatPelatihans",
                column: "BiodataId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PendidikanTerakhirs");

            migrationBuilder.DropTable(
                name: "RiwayatPekerjaans");

            migrationBuilder.DropTable(
                name: "RiwayatPelatihans");

            migrationBuilder.DropTable(
                name: "Biodatas");
        }
    }
}
