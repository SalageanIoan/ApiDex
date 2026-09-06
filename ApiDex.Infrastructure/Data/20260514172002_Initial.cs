using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiDex.Infrastructure.Data
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ApiDex");

            migrationBuilder.CreateTable(
                name: "DocumentationProjects",
                schema: "ApiDex",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    GeneralDescription = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    ArchitectureType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentationProjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RagSettings",
                schema: "ApiDex",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ActiveModel = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RagSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceDocumentations",
                schema: "ApiDex",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentationProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    GeneralDescription = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceDocumentations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceDocumentations_DocumentationProjects_DocumentationPr~",
                        column: x => x.DocumentationProjectId,
                        principalSchema: "ApiDex",
                        principalTable: "DocumentationProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceEndpoints",
                schema: "ApiDex",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceDocumentationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    HttpMethod = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    Route = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceEndpoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceEndpoints_ServiceDocumentations_ServiceDocumentation~",
                        column: x => x.ServiceDocumentationId,
                        principalSchema: "ApiDex",
                        principalTable: "ServiceDocumentations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceEvents",
                schema: "ApiDex",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceDocumentationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    Direction = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceEvents_ServiceDocumentations_ServiceDocumentationId",
                        column: x => x.ServiceDocumentationId,
                        principalSchema: "ApiDex",
                        principalTable: "ServiceDocumentations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceEventEndpoints",
                schema: "ApiDex",
                columns: table => new
                {
                    ServiceEventId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceEndpointId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceEventEndpoints", x => new { x.ServiceEventId, x.ServiceEndpointId });
                    table.ForeignKey(
                        name: "FK_ServiceEventEndpoints_ServiceEndpoints_ServiceEndpointId",
                        column: x => x.ServiceEndpointId,
                        principalSchema: "ApiDex",
                        principalTable: "ServiceEndpoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceEventEndpoints_ServiceEvents_ServiceEventId",
                        column: x => x.ServiceEventId,
                        principalSchema: "ApiDex",
                        principalTable: "ServiceEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceDependencies",
                schema: "ApiDex",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceDocumentationId = table.Column<Guid>(type: "uuid", nullable: false),
                    SourceEndpointId = table.Column<Guid>(type: "uuid", nullable: false),
                    DependencyType = table.Column<int>(type: "integer", nullable: false),
                    TargetEndpointId = table.Column<Guid>(type: "uuid", nullable: true),
                    TargetServiceTitle = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    TargetEndpointRoute = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceDependencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceDependencies_ServiceDocumentations_ServiceDocumentat~",
                        column: x => x.ServiceDocumentationId,
                        principalSchema: "ApiDex",
                        principalTable: "ServiceDocumentations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceDependencies_ServiceEndpoints_SourceEndpointId",
                        column: x => x.SourceEndpointId,
                        principalSchema: "ApiDex",
                        principalTable: "ServiceEndpoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceDependencies_ServiceEndpoints_TargetEndpointId",
                        column: x => x.TargetEndpointId,
                        principalSchema: "ApiDex",
                        principalTable: "ServiceEndpoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentationProjects_Title",
                schema: "ApiDex",
                table: "DocumentationProjects",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceDependencies_ServiceDocumentationId",
                schema: "ApiDex",
                table: "ServiceDependencies",
                column: "ServiceDocumentationId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceDependencies_SourceEndpointId",
                schema: "ApiDex",
                table: "ServiceDependencies",
                column: "SourceEndpointId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceDependencies_TargetEndpointId",
                schema: "ApiDex",
                table: "ServiceDependencies",
                column: "TargetEndpointId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceDocumentations_DocumentationProjectId_Title",
                schema: "ApiDex",
                table: "ServiceDocumentations",
                columns: new[] { "DocumentationProjectId", "Title" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceEndpoints_ServiceDocumentationId_Key",
                schema: "ApiDex",
                table: "ServiceEndpoints",
                columns: new[] { "ServiceDocumentationId", "Key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceEventEndpoints_ServiceEndpointId",
                schema: "ApiDex",
                table: "ServiceEventEndpoints",
                column: "ServiceEndpointId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceEvents_ServiceDocumentationId",
                schema: "ApiDex",
                table: "ServiceEvents",
                column: "ServiceDocumentationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceDependencies",
                schema: "ApiDex");

            migrationBuilder.DropTable(
                name: "ServiceEventEndpoints",
                schema: "ApiDex");

            migrationBuilder.DropTable(
                name: "ServiceEvents",
                schema: "ApiDex");

            migrationBuilder.DropTable(
                name: "ServiceEndpoints",
                schema: "ApiDex");

            migrationBuilder.DropTable(
                name: "ServiceDocumentations",
                schema: "ApiDex");

            migrationBuilder.DropTable(
                name: "DocumentationProjects",
                schema: "ApiDex");

            migrationBuilder.DropTable(
                name: "RagSettings",
                schema: "ApiDex");
        }
    }
}
