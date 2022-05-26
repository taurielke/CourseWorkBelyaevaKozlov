using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;
using UniversityDataBaseImplement.Models;
using UniversityDataBaseImplementation.Models;

namespace UniversityDataBaseImplement.Implements
{
    public class StudentStorage : IStudentStorage
    {
        public List<StudentViewModel> GetFullList()
        {
            using var context = new UniversityDatabase();
            return context.Students
                .Include(rec => rec.StudentDisciplines)
                .ThenInclude(rec => rec.Discipline)
                .Include(rec => rec.LearningPlanStudents)
                .ThenInclude(rec => rec.LearningPlan)
                .ToList()
                .Select(CreateModel).ToList();
            
        }
        public List<StudentViewModel> GetFilteredList(StudentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var context = new UniversityDatabase();
            
            return context.Students
                .Include(rec => rec.StudentDisciplines)
                .ThenInclude(rec => rec.Discipline)
                .Include(rec => rec.LearningPlanStudents)
                .ThenInclude(rec => rec.LearningPlan)
                .Where(rec => (rec.Name == model.Name) || (model.DeaneryId.HasValue && rec.DeaneryId == model.DeaneryId))
                .Select(CreateModel)
                .ToList();
            
        }
        public StudentViewModel GetElement(StudentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var context = new UniversityDatabase();
            var student = context.Students
                .Include(rec => rec.StudentDisciplines)
                .ThenInclude(rec => rec.Discipline)
                .Include(rec => rec.LearningPlanStudents)
                .ThenInclude(rec => rec.LearningPlan)
                .FirstOrDefault(rec => rec.Name == model.Name || rec.GradebookNumber == model.GradebookNumber);
            return student != null ? CreateModel(student) : null;
            
        }
        public void Insert(StudentBindingModel model)
        {
            var context = new UniversityDatabase();
            var transaction = context.Database.BeginTransaction();           
            try
            {
                Student student = new Student
                {
                    Name = model.Name,
                    StreamName = model.StreamName,
                    DeaneryId = (int)model.DeaneryId,
                };
                context.Students.Add(student);
                context.SaveChanges();
                CreateModel(model, student, context);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
                
            
        }
        public void Update(StudentBindingModel model)
        {
            var context = new UniversityDatabase();
            var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Students.FirstOrDefault(rec => rec.GradebookNumber == model.GradebookNumber);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Delete(StudentBindingModel model)
        {
            var context = new UniversityDatabase();
            Student element = context.Students.FirstOrDefault(rec => rec.GradebookNumber == model.GradebookNumber);
            if (element != null)
            {
                context.Students.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
            
        }
        private Student CreateModel(StudentBindingModel model, Student student, UniversityDatabase context)
        {
            student.StreamName = model.StreamName;
            student.Name = model.Name;
            student.DeaneryId = (int)model.DeaneryId;

            // нужно передавать student уже с заполнеными полями и добавленным таблицу Students  
            if (model.GradebookNumber.HasValue)
            {
                var studentDisciplines = context.StudentDisciplines.Where(rec => rec.GradebookNumber == model.GradebookNumber).ToList();
                context.StudentDisciplines.RemoveRange(studentDisciplines);
                var learningPlanStudents = context.LearningPlanStudents.Where(rec => rec.GradebookNumber == model.GradebookNumber).ToList();
                context.LearningPlanStudents.RemoveRange(learningPlanStudents);
                context.SaveChanges();
            }
            foreach (var ss in model.Disciplines)
            {
                context.StudentDisciplines.Add(new StudentDiscipline
                {
                    GradebookNumber = student.GradebookNumber,
                    DisciplineId = ss.Key,
                });
                context.SaveChanges();
            }
            foreach (var ss in model.LearningPlans)
            {
                context.LearningPlanStudents.Add(new LearningPlanStudent
                {
                    GradebookNumber = student.GradebookNumber,
                    LearningPlanId = ss.Key,
                });
                context.SaveChanges();
            }
            return student;
        }

        public void BindingDiscipline(int gradebookNumber, int subjectId)
        {
            var context = new UniversityDatabase();
            context.StudentDisciplines.Add(new StudentDiscipline
            {
                GradebookNumber = gradebookNumber,
                DisciplineId = subjectId,
            });
            context.SaveChanges();
            
        }
        public List<StudentViewModel> GetByDisciplineId(int subjectId)
        {
            var context = new UniversityDatabase();
            return context.Students
                .Include(rec => rec.StudentDisciplines)
                .ThenInclude(rec => rec.Discipline)
                .Include(rec => rec.LearningPlanStudents)
                .ThenInclude(rec => rec.LearningPlan)
                .ToList()
                .Where(rec => rec.StudentDisciplines.FirstOrDefault(ss => ss.DisciplineId == subjectId) != null)
                .Select(rec => new StudentViewModel
                {
                    GradebookNumber = rec.GradebookNumber,
                    DeaneryId = rec.DeaneryId,
                    StreamName = rec.StreamName,
                    Name = rec.Name,
                    Disciplines = rec.StudentDisciplines
                    .ToDictionary(recSS => recSS.DisciplineId, recSS => recSS.Discipline.Name),
                    LearningPlans = rec.LearningPlanStudents
                    .ToDictionary(recES => recES.LearningPlanId, recES => recES.LearningPlan.LearningPlanName)
                })
                .ToList();
            
        }
        private static StudentViewModel CreateModel(Student student)
        {
            return new StudentViewModel
            {
                GradebookNumber = student.GradebookNumber,
                DeaneryId = student.DeaneryId,
                StreamName = student.StreamName,
                Name = student.Name,
                Disciplines = student.StudentDisciplines
                .ToDictionary(recSS => recSS.DisciplineId, recSS => recSS.Discipline.Name),
                LearningPlans = student.LearningPlanStudents
                .ToDictionary(recES => recES.LearningPlanId, recES => recES.LearningPlan.LearningPlanName)
            };
        }
    }
}