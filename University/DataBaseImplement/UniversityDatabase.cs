﻿using System;
using System.Collections.Generic;
using System.Text;
using UniversityDataBaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using UniversityDataBaseImplement.Models;
using UniversityDataBaseImplementation.Models;

namespace UniversityDataBaseImplement
{
    public class UniversityDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-3ONQPAF5\SQLEXPRESS;Initial Catalog=FurnitureAssemblyDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Attestation> Attestations { set; get; }
        public virtual DbSet<Discipline> Disciplines { set; get; }
        public virtual DbSet<Department> Departments { set; get; }
        
        public virtual DbSet<StudentDiscipline> StudentDisciplines { set; get; }
        public virtual DbSet<LearningPlanStudent> LearningPlanStudents { set; get; }
        public virtual DbSet<DisciplineLearningPlan> DisciplineLearningPlans { set; get; }
        public virtual DbSet<InterimReport> InterimReports { set; get; }
        public virtual DbSet<LearningPlan> LearningPlans { set; get; }
        public virtual DbSet<Student> Students { set; get; }
        public virtual DbSet<Teacher> Teachers { set; get; }
        public virtual DbSet<Deanery> Deaneries { set; get; }
    }
}
