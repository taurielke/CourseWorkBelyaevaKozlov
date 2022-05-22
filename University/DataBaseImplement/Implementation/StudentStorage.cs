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
            using (var context = new UniversityDatabase())
            {
                return context.Students
                  .Include(rec => rec.StudentDisciplines)
                  .ThenInclude(rec => rec.Discipline)
                  .Include(rec => rec.LearningPlanStudents)
                  .ThenInclude(rec => rec.LearningPlan).ToList()
                  .Select(rec => new StudentViewModel
                  {
                      GradebookNumber = rec.GradebookNumber,
                      Name = rec.Name,
                      Disciplines = rec.StudentDisciplines
                      .ToDictionary(recSS => recSS.DisciplineId, recSS => recSS.Discipline.Name),
                      LearningPlans = rec.LearningPlanStudents
                      .ToDictionary(recES => recES.LearningPlanId, recES => recES.LearningPlan.StreamName)

                  }).ToList();
            }
        }
        public List<StudentViewModel> GetFilteredList(StudentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                return context.Students
                  .Include(rec => rec.StudentDisciplines)
                  .ThenInclude(rec => rec.Discipline)
                  .Include(rec => rec.LearningPlanStudents)
                  .ThenInclude(rec => rec.LearningPlan)
                  .Where(rec => rec.Name == model.Name)
                  .Select(rec => new StudentViewModel
                  {
                      GradebookNumber = rec.GradebookNumber,
                      Name = rec.Name,
                      Disciplines = rec.StudentDisciplines
                      .ToDictionary(recSS => recSS.DisciplineId, recSS => recSS.Discipline.Name),
                      LearningPlans = rec.LearningPlanStudents
                      .ToDictionary(recES => recES.LearningPlanId, recES => recES.LearningPlan.StreamName)
                  })
                  .ToList();
            }
        }
        public StudentViewModel GetElement(StudentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                var student = context.Students
                  .Include(rec => rec.StudentDisciplines)
                  .ThenInclude(rec => rec.Discipline)
                  .Include(rec => rec.LearningPlanStudents)
                  .ThenInclude(rec => rec.LearningPlan)
                  .FirstOrDefault(rec => rec.Name == model.Name || rec.GradebookNumber == model.GradebookNumber);
                return student != null ?
                  new StudentViewModel
                  {
                      GradebookNumber = student.GradebookNumber,
                      Name = student.Name,
                      Disciplines = student.StudentDisciplines
                      .ToDictionary(recSS => recSS.DisciplineId, recSS => recSS.Discipline.Name),
                      LearningPlans = student.LearningPlanStudents
                      .ToDictionary(recES => recES.LearningPlanId, recES => recES.LearningPlan.StreamName)
                  } :
                  null;
            }
        }
        public void Insert(StudentBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Student s = new Student
                        {
                            Name = model.Name,
                        };
                        context.Students.Add(s);
                        context.SaveChanges();
                        CreateModel(model, s, context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }
        public void Update(StudentBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Students
                          .Include(rec => rec.StudentDisciplines)
                          .ThenInclude(rec => rec.Discipline)
                          .Include(rec => rec.LearningPlanStudents)
                          .ThenInclude(rec => rec.LearningPlan).FirstOrDefault(rec => rec.GradebookNumber == model.GradebookNumber);
                        if (element == null)
                        {
                            throw new Exception("Элемент не найден");
                        }
                        element.Name = model.Name;
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
            }
        }
        public void Delete(StudentBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
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
        }
        private Student CreateModel(StudentBindingModel model, Student student, UniversityDatabase context)
        {
            // нужно передавать student уже с заполнеными полями и добавленным таблицу Students  
            if (string.IsNullOrEmpty(model.GradebookNumber))
            {
                var studentDisciplines = context.StudentDisciplines.Where(rec => rec.GradebookNumber == model.GradebookNumber).ToList();
                context.StudentDisciplines.RemoveRange(studentDisciplines.Where(rec => !model.Disciplines.ContainsKey(rec.DisciplineId)).ToList());
                var educationPlanStudents = context.LearningPlanStudents.Where(rec => rec.GradebookNumber == model.GradebookNumber).ToList();

                context.LearningPlanStudents.RemoveRange(educationPlanStudents.Where(rec => !model.LearningPlans.ContainsKey(rec.LearningPlanId)).ToList());
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
            foreach (var ss in model.Disciplines)
            {
                context.StudentDisciplines.Add(new StudentDiscipline
                {
                    GradebookNumber = student.GradebookNumber,
                    DisciplineId = ss.Key,
                });
                context.SaveChanges();
            }
            return student;
        }

        public void BindingDiscipline(string gradebookNumber, int subjectId)
        {
            using (var context = new UniversityDatabase())
            {
                context.StudentDisciplines.Add(new StudentDiscipline
                {
                    GradebookNumber = gradebookNumber,
                    DisciplineId = subjectId,
                });
                context.SaveChanges();
            }
        }
        public List<StudentViewModel> GetByDisciplineId(int subjectId)
        {
            using (var context = new UniversityDatabase())
            {
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
                      Name = rec.Name,
                      Disciplines = rec.StudentDisciplines
                      .ToDictionary(recSS => recSS.DisciplineId, recSS => recSS.Discipline.Name),
                      LearningPlans = rec.LearningPlanStudents
                      .ToDictionary(recES => recES.LearningPlanId, recES => recES.LearningPlan.StreamName)
                  })
                  .ToList();
            }
        }
    }
}