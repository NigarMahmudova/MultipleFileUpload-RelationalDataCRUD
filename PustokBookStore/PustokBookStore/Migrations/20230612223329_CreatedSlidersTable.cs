using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PustokBookStore.Migrations
{
    public partial class CreatedSlidersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sliders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Title2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    BtnText = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BtnUrl = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sliders");
        }
    }
}
