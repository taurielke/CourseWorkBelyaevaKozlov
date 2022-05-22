using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversityDataBaseImplement.Migrations
{
    public partial class c : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
<<<<<<<< HEAD:University/DataBaseImplement/Migrations/20220521194918_InitialCreate.cs
                name: "Attestations",
========
                name: "Deaneries",
>>>>>>>> 300594a27c949bdcc488c4d70265fd149aa48490:University/DataBaseImplement/Migrations/20220521184947_c.cs
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
<<<<<<<< HEAD:University/DataBaseImplement/Migrations/20220521194918_InitialCreate.cs
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentGradebookNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeaneryLogin = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attestations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Deaneries",
                columns: table => new
                {
                    Login = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
========
                    DeaneryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
>>>>>>>> 300594a27c949bdcc488c4d70265fd149aa48490:University/DataBaseImplement/Migrations/20220521184947_c.cs
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
<<<<<<<< HEAD:University/DataBaseImplement/Migrations/20220521194918_InitialCreate.cs
                    table.PrimaryKey("PK_Deaneries", x => x.Login);
========
                    table.PrimaryKey("PK_Deaneries", x => x.Id);
>>>>>>>> 300594a27c949bdcc488c4d70265fd149aa48490:University/DataBaseImplement/Migrations/20220521184947_c.cs
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
<<<<<<<< HEAD:University/DataBaseImplement/Migrations/20220521194918_InitialCreate.cs
========
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LearningPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeaneryId = table.Column<int>(type: "int", nullable: false),
                    LearningPlanName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecialtyName = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
>>>>>>>> 300594a27c949bdcc488c4d70265fd149aa48490:University/DataBaseImplement/Migrations/20220521184947_c.cs
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
                name: "Students",
                columns: table => new
                {
                    RecordBookNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
<<<<<<<< HEAD:University/DataBaseImplement/Migrations/20220521194918_InitialCreate.cs
                    StreamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hours = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    GradebookNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.GradebookNumber);
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
========
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnrollingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CourseYear = table.Column<int>(type: "int", nullable: false),
                    LearningPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.RecordBookNumber);
                    table.ForeignKey(
                        name: "FK_Students_LearningPlans_LearningPlanId",
                        column: x => x.LearningPlanId,
                        principalTable: "LearningPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
>>>>>>>> 300594a27c949bdcc488c4d70265fd149aa48490:University/DataBaseImplement/Migrations/20220521184947_c.cs
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
<<<<<<<< HEAD:University/DataBaseImplement/Migrations/20220521194918_InitialCreate.cs
                name: "LearningPlanStudents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GradebookNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentDisciplines",
========
                name: "Attestations",
>>>>>>>> 300594a27c949bdcc488c4d70265fd149aa48490:University/DataBaseImplement/Migrations/20220521184947_c.cs
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
<<<<<<<< HEAD:University/DataBaseImplement/Migrations/20220521194918_InitialCreate.cs
                    GradebookNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DisciplineId = table.Column<int>(type: "int", nullable: false)
========
                    DeaneryId = table.Column<int>(type: "int", nullable: false),
                    RecordBookNumber = table.Column<int>(type: "int", nullable: false),
                    SemesterNumber = table.Column<int>(type: "int", nullable: false),
                    DisciplineId = table.Column<int>(type: "int", nullable: false),
                    Mark = table.Column<int>(type: "int", nullable: false),
                    ExamDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentRecordBookNumber = table.Column<int>(type: "int", nullable: true)
>>>>>>>> 300594a27c949bdcc488c4d70265fd149aa48490:University/DataBaseImplement/Migrations/20220521184947_c.cs
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentDisciplines", x => x.Id);
                    table.ForeignKey(
<<<<<<<< HEAD:University/DataBaseImplement/Migrations/20220521194918_InitialCreate.cs
                        name: "FK_StudentDisciplines_Disciplines_DisciplineId",
========
                        name: "FK_Attestations_Deaneries_DeaneryId",
                        column: x => x.DeaneryId,
                        principalTable: "Deaneries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attestations_Disciplines_DisciplineId",
>>>>>>>> 300594a27c949bdcc488c4d70265fd149aa48490:University/DataBaseImplement/Migrations/20220521184947_c.cs
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentDisciplines_Students_GradebookNumber",
                        column: x => x.GradebookNumber,
                        principalTable: "Students",
                        principalColumn: "GradebookNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LearningPlanTeacher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    LearningPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningPlanTeacher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LearningPlanTeacher_LearningPlans_LearningPlanId",
                        column: x => x.LearningPlanId,
                        principalTable: "LearningPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LearningPlanTeacher_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
<<<<<<<< HEAD:University/DataBaseImplement/Migrations/20220521194918_InitialCreate.cs
========
                name: "IX_Attestations_DeaneryId",
                table: "Attestations",
                column: "DeaneryId");

            migrationBuilder.CreateIndex(
                name: "IX_Attestations_DisciplineId",
                table: "Attestations",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_Attestations_StudentRecordBookNumber",
                table: "Attestations",
                column: "StudentRecordBookNumber");

            migrationBuilder.CreateIndex(
>>>>>>>> 300594a27c949bdcc488c4d70265fd149aa48490:University/DataBaseImplement/Migrations/20220521184947_c.cs
                name: "IX_DisciplineLearningPlans_DisciplineId",
                table: "DisciplineLearningPlans",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineLearningPlans_LearningPlanId",
                table: "DisciplineLearningPlans",
                column: "LearningPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_LearningPlanStudents_GradebookNumber",
                table: "LearningPlanStudents",
                column: "GradebookNumber");

            migrationBuilder.CreateIndex(
                name: "IX_LearningPlanStudents_LearningPlanId",
                table: "LearningPlanStudents",
                column: "LearningPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_LearningPlanTeacher_LearningPlanId",
                table: "LearningPlanTeacher",
                column: "LearningPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_LearningPlanTeacher_TeacherId",
                table: "LearningPlanTeacher",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentDisciplines_DisciplineId",
                table: "StudentDisciplines",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
<<<<<<<< HEAD:University/DataBaseImplement/Migrations/20220521194918_InitialCreate.cs
                name: "IX_StudentDisciplines_GradebookNumber",
                table: "StudentDisciplines",
                column: "GradebookNumber");
========
                name: "IX_InterimReports_StudentRecordBookNumber",
                table: "InterimReports",
                column: "StudentRecordBookNumber");

            migrationBuilder.CreateIndex(
                name: "IX_LearningPlans_DeaneryId",
                table: "LearningPlans",
                column: "DeaneryId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_LearningPlanId",
                table: "Students",
                column: "LearningPlanId");
>>>>>>>> 300594a27c949bdcc488c4d70265fd149aa48490:University/DataBaseImplement/Migrations/20220521184947_c.cs
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attestations");

            migrationBuilder.DropTable(
                name: "Deaneries");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "DisciplineLearningPlans");

            migrationBuilder.DropTable(
                name: "InterimReports");

            migrationBuilder.DropTable(
                name: "LearningPlanStudents");

            migrationBuilder.DropTable(
                name: "LearningPlanTeacher");

            migrationBuilder.DropTable(
                name: "StudentDisciplines");

            migrationBuilder.DropTable(
                name: "LearningPlans");

            migrationBuilder.DropTable(
<<<<<<<< HEAD:University/DataBaseImplement/Migrations/20220521194918_InitialCreate.cs
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Disciplines");

            migrationBuilder.DropTable(
                name: "Students");
========
                name: "Deaneries");
>>>>>>>> 300594a27c949bdcc488c4d70265fd149aa48490:University/DataBaseImplement/Migrations/20220521184947_c.cs
        }
    }
}
