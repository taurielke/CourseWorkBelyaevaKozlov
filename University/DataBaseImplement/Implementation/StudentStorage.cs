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
            return context.Students.Select(CreateModel).ToList();
            /*return context.Students.Include(rec => rec.LearningPlan).ToList().Select(CreateModel).ToList();*/
        }
        public List<StudentViewModel> GetFilteredList(StudentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityDatabase();
            return context.Students.Include(rec => rec.LearningPlan)
                /*.Where(rec => rec.RecordBookNumber.Equals(model.RecordBookNumber))
                .ToList()*/
                .Where(rec => rec.RecordBookNumber.Equals(model.RecordBookNumber))
                .Select(CreateModel).ToList();

        }
        public StudentViewModel GetElement(StudentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityDatabase();
            var student = context.Students.Include(rec => rec.LearningPlan).FirstOrDefault(rec => rec.RecordBookNumber == model.RecordBookNumber);
            return student != null ? CreateModel(student) : null;
        }
        public void Insert(StudentBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Students.Add(CreateModel(model, new Student()));
                context.SaveChanges();
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
                CreateModel(model, element);
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
        private static Student CreateModel(StudentBindingModel model, Student student)
        {
            student.RecordBookNumber = (int)model.RecordBookNumber;
            student.StudentName = model.StudentName;
            student.EnrollingDate = model.EnrollingDate;
            student.CourseYear = model.CourseYear;
            student.LearningPlanId = model.LearningPlanId;
                
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
                LearningPlanId = student.LearningPlanId,
                LearningPlanName = context.LearningPlans.FirstOrDefault(rec => rec.Id == student.LearningPlanId)?.LearningPlanName
            };
        }
    }
}
