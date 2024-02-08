using Microsoft.EntityFrameworkCore.Migrations;

namespace VideoKlubAPI.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VideoKlub",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoKlub", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Odeljenje",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BrojRedova = table.Column<int>(type: "int", nullable: false),
                    BrojPolicaPoRedu = table.Column<int>(type: "int", nullable: false),
                    KlubID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odeljenje", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Odeljenje_VideoKlub_KlubID",
                        column: x => x.KlubID,
                        principalTable: "VideoKlub",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Film",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivFilma = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Reziser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Trajanje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GodIzlaska = table.Column<int>(type: "int", nullable: false),
                    Kolicina = table.Column<int>(type: "int", nullable: false),
                    Red = table.Column<int>(type: "int", nullable: false),
                    PozURedu = table.Column<int>(type: "int", nullable: false),
                    OdeljenjeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Film", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Film_Odeljenje_OdeljenjeID",
                        column: x => x.OdeljenjeID,
                        principalTable: "Odeljenje",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Film_OdeljenjeID",
                table: "Film",
                column: "OdeljenjeID");

            migrationBuilder.CreateIndex(
                name: "IX_Odeljenje_KlubID",
                table: "Odeljenje",
                column: "KlubID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Film");

            migrationBuilder.DropTable(
                name: "Odeljenje");

            migrationBuilder.DropTable(
                name: "VideoKlub");
        }
    }
}
