using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Emp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initOracle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Emps",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    FirstName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    LastName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Division = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Building = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Title = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Room = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false) 
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emps", x => x.EmployeeId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emps");
        }
    }
}
