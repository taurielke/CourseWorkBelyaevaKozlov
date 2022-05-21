﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UniversityDataBaseImplement;

namespace UniversityDataBaseImplement.Migrations
{
    [DbContext(typeof(UniversityDatabase))]
    partial class UniversityDatabaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.14")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("UniversityDataBaseImplement.Models.Attestation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DisciplineId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExamDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Mark")
                        .HasColumnType("int");

                    b.Property<int>("RecordBookNumber")
                        .HasColumnType("int");

                    b.Property<int>("SemesterNumber")
                        .HasColumnType("int");

                    b.Property<int?>("StudentRecordBookNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DisciplineId");

                    b.HasIndex("StudentRecordBookNumber");

                    b.ToTable("Attestations");
                });

            modelBuilder.Entity("UniversityDataBaseImplement.Models.Discipline", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DisciplineDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisciplineName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Disciplines");
                });

            modelBuilder.Entity("UniversityDataBaseImplement.Models.DisciplineLearningPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DisciplineId")
                        .HasColumnType("int");

                    b.Property<int>("LearningPlanId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DisciplineId");

                    b.HasIndex("LearningPlanId");

                    b.ToTable("DisciplineLearningPlans");
                });

            modelBuilder.Entity("UniversityDataBaseImplement.Models.InterimReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DisciplineId")
                        .HasColumnType("int");

                    b.Property<int>("Mark")
                        .HasColumnType("int");

                    b.Property<int>("RecordBookNumber")
                        .HasColumnType("int");

                    b.Property<int>("SemesterNumber")
                        .HasColumnType("int");

                    b.Property<int?>("StudentRecordBookNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DisciplineId");

                    b.HasIndex("StudentRecordBookNumber");

                    b.ToTable("InterimReports");
                });

            modelBuilder.Entity("UniversityDataBaseImplement.Models.LearningPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LearningPlanName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpecialtyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("LearningPlans");
                });

            modelBuilder.Entity("UniversityDataBaseImplement.Models.Student", b =>
                {
                    b.Property<int>("RecordBookNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseYear")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EnrollingDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LearningPlanId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RecordBookNumber");

                    b.HasIndex("LearningPlanId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("UniversityDataBaseImplement.Models.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TeacherName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("UniversityDataBaseImplement.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("UniversityDatabaseImplement.Models.Department", b =>
                {
                    b.Property<string>("DepartmentLogin")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DepartmentLogin");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("UniversityDataBaseImplement.Models.Attestation", b =>
                {
                    b.HasOne("UniversityDataBaseImplement.Models.Discipline", "Discipline")
                        .WithMany()
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniversityDataBaseImplement.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentRecordBookNumber");

                    b.Navigation("Discipline");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("UniversityDataBaseImplement.Models.Discipline", b =>
                {
                    b.HasOne("UniversityDataBaseImplement.Models.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("UniversityDataBaseImplement.Models.DisciplineLearningPlan", b =>
                {
                    b.HasOne("UniversityDataBaseImplement.Models.Discipline", "Discipline")
                        .WithMany("DisciplineLearningPlans")
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniversityDataBaseImplement.Models.LearningPlan", "LearningPlan")
                        .WithMany("DisciplineLearningPlans")
                        .HasForeignKey("LearningPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Discipline");

                    b.Navigation("LearningPlan");
                });

            modelBuilder.Entity("UniversityDataBaseImplement.Models.InterimReport", b =>
                {
                    b.HasOne("UniversityDataBaseImplement.Models.Discipline", "Discipline")
                        .WithMany()
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniversityDataBaseImplement.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentRecordBookNumber");

                    b.Navigation("Discipline");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("UniversityDataBaseImplement.Models.LearningPlan", b =>
                {
                    b.HasOne("UniversityDataBaseImplement.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("UniversityDataBaseImplement.Models.Student", b =>
                {
                    b.HasOne("UniversityDataBaseImplement.Models.LearningPlan", "LearningPlan")
                        .WithMany("Students")
                        .HasForeignKey("LearningPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LearningPlan");
                });

            modelBuilder.Entity("UniversityDataBaseImplement.Models.Discipline", b =>
                {
                    b.Navigation("DisciplineLearningPlans");
                });

            modelBuilder.Entity("UniversityDataBaseImplement.Models.LearningPlan", b =>
                {
                    b.Navigation("DisciplineLearningPlans");

                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
