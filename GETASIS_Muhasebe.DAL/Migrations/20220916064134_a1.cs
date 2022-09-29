using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GETASIS_Muhasebe.DAL.Migrations
{
    public partial class a1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdSoyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VergiDairesi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VergiNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cari", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doviz",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kisa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doviz", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OdemeTip",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OdemeTip", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hesap",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DovizId = table.Column<int>(type: "int", nullable: false),
                    AcilisBakiye = table.Column<double>(type: "float", nullable: true),
                    BankaAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sube = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IBAN = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hesap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hesap_Doviz_DovizId",
                        column: x => x.DovizId,
                        principalTable: "Doviz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Odeme",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Alacak = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Borc = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BelgeNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VadeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CariId = table.Column<int>(type: "int", nullable: false),
                    HesapId = table.Column<int>(type: "int", nullable: false),
                    OdemeTipId = table.Column<int>(type: "int", nullable: false),
                    Silindi = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odeme", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Odeme_Cari_CariId",
                        column: x => x.CariId,
                        principalTable: "Cari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Odeme_Hesap_HesapId",
                        column: x => x.HesapId,
                        principalTable: "Hesap",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Odeme_OdemeTip_OdemeTipId",
                        column: x => x.OdemeTipId,
                        principalTable: "OdemeTip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Doviz",
                columns: new[] { "Id", "Ad", "Kisa" },
                values: new object[,]
                {
                    { 1, "Türk Lirası", "₺" },
                    { 2, "Dolar", "$" },
                    { 3, "Euro", "€" },
                    { 4, "Altın", "Au" }
                });

            migrationBuilder.InsertData(
                table: "OdemeTip",
                columns: new[] { "Id", "Ad" },
                values: new object[,]
                {
                    { 1, "Personel" },
                    { 2, "Cari" },
                    { 3, "Vergi" },
                    { 4, "Şahsi" },
                    { 5, "Diğer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hesap_DovizId",
                table: "Hesap",
                column: "DovizId");

            migrationBuilder.CreateIndex(
                name: "IX_Odeme_CariId",
                table: "Odeme",
                column: "CariId");

            migrationBuilder.CreateIndex(
                name: "IX_Odeme_HesapId",
                table: "Odeme",
                column: "HesapId");

            migrationBuilder.CreateIndex(
                name: "IX_Odeme_OdemeTipId",
                table: "Odeme",
                column: "OdemeTipId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Odeme");

            migrationBuilder.DropTable(
                name: "Cari");

            migrationBuilder.DropTable(
                name: "Hesap");

            migrationBuilder.DropTable(
                name: "OdemeTip");

            migrationBuilder.DropTable(
                name: "Doviz");
        }
    }
}
