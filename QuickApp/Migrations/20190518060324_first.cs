using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuickApp.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Svcs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    DomainId = table.Column<int>(maxLength: 250, nullable: true),
                    AccountId = table.Column<int>(maxLength: 250, nullable: true),
                    Country = table.Column<string>(maxLength: 250, nullable: true),
                    ServiceDeliveryManager = table.Column<string>(maxLength: 250, nullable: true),
                    AccountManager = table.Column<string>(maxLength: 20, nullable: true),
                    Date = table.Column<DateTime>(nullable: true),
                    QuoteFTL = table.Column<string>(maxLength: 250, nullable: true),
                    PO = table.Column<string>(maxLength: 250, nullable: true),
                    Client = table.Column<string>(maxLength: 250, nullable: true),
                    Field = table.Column<string>(maxLength: 250, nullable: true),
                    Well = table.Column<string>(maxLength: 250, nullable: true),
                    AU = table.Column<string>(maxLength: 250, nullable: true),
                    AC = table.Column<string>(maxLength: 250, nullable: true),
                    Portfolio = table.Column<string>(maxLength: 250, nullable: true),
                    SubPortfolio = table.Column<string>(maxLength: 250, nullable: true),
                    MasterCode = table.Column<string>(maxLength: 250, nullable: true),
                    Currency = table.Column<string>(maxLength: 5, nullable: true),
                    FXRate = table.Column<decimal>(type: "NUMERIC(6,2)", nullable: true),
                    Comment = table.Column<string>(maxLength: 250, nullable: true),
                    TechnicalLead = table.Column<string>(maxLength: 250, nullable: true),
                    ChangePointTask = table.Column<string>(maxLength: 250, nullable: true),
                    ROFO = table.Column<decimal>(type: "MONEY", nullable: true),
                    iMF = table.Column<decimal>(type: "MONEY", nullable: true),
                    MMF = table.Column<decimal>(type: "MONEY", nullable: true),
                    SentToInvoice = table.Column<decimal>(type: "MONEY", nullable: true),
                    Revenue = table.Column<decimal>(type: "MONEY", nullable: true),
                    InvocieNumber = table.Column<string>(maxLength: 250, nullable: true),
                    Cost = table.Column<decimal>(type: "MONEY", nullable: true),
                    CostReceived = table.Column<decimal>(type: "MONEY", nullable: true),
                    CostType = table.Column<string>(maxLength: 250, nullable: true),
                    GLAccount = table.Column<string>(maxLength: 250, nullable: true),
                    CostDescription = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Svcs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Svcs");
        }
    }
}
