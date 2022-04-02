using System;
using System.Collections.Generic;
using System.Text;
using UniversityDataBaseImplement.Models;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;
using UniversityBusinessLogic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace UniversityDataBaseImplement.Implementation
{
    public class StudentStorage : IStudentStorage
    {
        public List<StudentViewModel> GetFullList()
        {
            using var context = new UniversityDatabase();
            return context.Students
            .Include(rec => rec.StudentLearningPlans)
            .ThenInclude(rec => rec.LearningPlan)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public List<StudentViewModel> GetFilteredList(StudentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityDatabase();
            return context.Students
                .Include(rec => rec.StudentLearningPlans)
                .ThenInclude(rec => rec.LearningPlan)
                .Where(rec => rec.StudentName.Contains(model.StudentName))
                .ToList()
                .Select(CreateModel)
                .ToList();
        }
        public StudentViewModel GetElement(StudentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityDatabase();
            var student = context.Students
                .Include(rec => rec.StudentLearningPlans)
                .ThenInclude(rec => rec.LearningPlan)
                .FirstOrDefault(rec => rec.StudentName == model.StudentName || rec.RecordBookNumber == model.RecordBookNumber);
            return student != null ? CreateModel(student) : null;
        }
        public void Insert(StudentBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Student student = new Student()
                {
                    StudentName = model.StudentName,
                    EnrollingDate = model.EnrollingDate
                };
                context.Students.Add(student);
                context.SaveChanges();
                CreateModel(model, student, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(StudentBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Students.FirstOrDefault(rec => rec.RecordBookNumber == model.RecordBookNumber);
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
            using var context = new UniversityDatabase();
            Student element = context.Students.FirstOrDefault(rec => rec.RecordBookNumber == model.RecordBookNumber);
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
        private static Student CreateModel(StudentBindingModel model, Student student, UniversityDatabase context)
        {
            student.StudentName = model.StudentName;
            student.EnrollingDate = model.EnrollingDate;
            if (model.RecordBookNumber.HasValue)
            {
                var StudentLearningPlans = context.StudentLearningPlans.Where(rec => rec.RecordBookNumber == model.RecordBookNumber.Value).ToList();
                context.StudentLearningPlans.RemoveRange(StudentLearningPlans.Where(rec => !model.StudentLearningPlans.ContainsKey(rec.LearningPlanId)).ToList());
                context.SaveChanges();
                foreach (var updateLearningPlan in StudentLearningPlans)
                {
                    model.StudentLearningPlans.Remove(updateLearningPlan.LearningPlanId);
                }
                context.SaveChanges();
            }
            // добавили новые
            foreach (var pc in model.StudentLearningPlans)
            {
                context.StudentLearningPlans.Add(new StudentLearningPlan
                {
                    RecordBookNumber = student.RecordBookNumber,
                    LearningPlanId = pc.Key
                });
                context.SaveChanges();
            }
            return student;
        }
        private static StudentViewModel CreateModel(Student student)
        {
            using var context = new UniversityDatabase();
            return new StudentViewModel
            {
                RecordBookNumber = student.RecordBookNumber,
                StudentName = student.StudentName,
                EnrollingDate = student.EnrollingDate,
                CourseYear = student.CourseYear,
                GroupId = student.GroupId,
                GroupName = context.Groups.FirstOrDefault(rec => rec.Id == student.GroupId)?.GroupName,
                StudentLearningPlans = student.StudentLearningPlans.ToDictionary(recPC => recPC.LearningPlanId, recPC => (recPC.LearningPlan?.LearningPlanName))
            };
        }
    }
}
