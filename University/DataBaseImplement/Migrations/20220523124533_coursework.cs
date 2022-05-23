using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversityDataBaseImplement.Migrations
{
    public partial class coursework : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Deaneries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deaneries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentLogin = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentLogin);
                });

            migrationBuilder.CreateTable(
                name: "Disciplines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentLogin = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InterimReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfExam = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterimReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisciplineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attestations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SemesterNumber = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentGradebookNumber = table.Column<int>(type: "int", nullable: false),
                    DeaneryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attestations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attestations_Deaneries_DeaneryId",
                        column: x => x.DeaneryId,
                        principalTable: "Deaneries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LearningPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeaneryId = table.Column<int>(type: "int", nullable: false),
                    StreamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hours = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LearningPlans_Deaneries_DeaneryId",
                        column: x => x.DeaneryId,
                        principalTable: "Deaneries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    GradebookNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeaneryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.GradebookNumber);
                    table.ForeignKey(
                        name: "FK_Students_Deaneries_DeaneryId",
                        column: x => x.DeaneryId,
                        principalTable: "Deaneries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DisciplineLearningPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisciplineId = table.Column<int>(type: "int", nullable: false),
                    LearningPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineLearningPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisciplineLearningPlans_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplineLearningPlans_LearningPlans_LearningPlanId",
                        column: x => x.LearningPlanId,
                        principalTable: "LearningPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LearningPlanTeachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    LearningPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningPlanTeachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LearningPlanTeachers_LearningPlans_LearningPlanId",
                        column: x => x.LearningPlanId,
                        principalTable: "LearningPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LearningPlanTeachers_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LearningPlanStudents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GradebookNumber = table.Column<int>(type: "int", nullable: false),
                    LearningPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningPlanStudents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LearningPlanStudents_LearningPlans_LearningPlanId",
                        column: x => x.LearningPlanId,
                        principalTable: "LearningPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LearningPlanStudents_Students_GradebookNumber",
                        column: x => x.GradebookNumber,
                        principalTable: "Students",
                        principalColumn: "GradebookNumber",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "StudentDisciplines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GradebookNumber = table.Column<int>(type: "int", nullable: false),
                    DisciplineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentDisciplines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentDisciplines_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentDisciplines_Students_GradebookNumber",
                        column: x => x.GradebookNumber,
                        principalTable: "Students",
                        principalColumn: "GradebookNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attestations_DeaneryId",
                table: "Attestations",
                column: "DeaneryId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineLearningPlans_DisciplineId",
                table: "DisciplineLearningPlans",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineLearningPlans_LearningPlanId",
                table: "DisciplineLearningPlans",
                column: "LearningPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_LearningPlans_DeaneryId",
                table: "LearningPlans",
                column: "DeaneryId");

            migrationBuilder.CreateIndex(
                name: "IX_LearningPlanStudents_GradebookNumber",
                table: "LearningPlanStudents",
                column: "GradebookNumber");

            migrationBuilder.CreateIndex(
                name: "IX_LearningPlanStudents_LearningPlanId",
                table: "LearningPlanStudents",
                column: "LearningPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_LearningPlanTeachers_LearningPlanId",
                table: "LearningPlanTeachers",
                column: "LearningPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_LearningPlanTeachers_TeacherId",
                table: "LearningPlanTeachers",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentDisciplines_DisciplineId",
                table: "StudentDisciplines",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentDisciplines_GradebookNumber",
                table: "StudentDisciplines",
                column: "GradebookNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Students_DeaneryId",
                table: "Students",
                column: "DeaneryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attestations");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "DisciplineLearningPlans");

            migrationBuilder.DropTable(
                name: "InterimReports");

            migrationBuilder.DropTable(
                name: "LearningPlanStudents");

            migrationBuilder.DropTable(
                name: "LearningPlanTeachers");

            migrationBuilder.DropTable(
                name: "StudentDisciplines");

            migrationBuilder.DropTable(
                name: "LearningPlans");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Disciplines");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Deaneries");
        }
    }
}
