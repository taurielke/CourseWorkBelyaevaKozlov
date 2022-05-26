using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class StudentLogic : IStudentLogic
    {
        private readonly IStudentStorage _studentStorage;
        private readonly ITeacherStorage _teacherStorage;
        private readonly ILearningPlanStorage _learningPlanStorage;

        public StudentLogic(IStudentStorage studentStorage, ITeacherStorage teacherStorage, ILearningPlanStorage learningPlanStorage)
        {
            _studentStorage = studentStorage;
            _teacherStorage = teacherStorage;
            _learningPlanStorage = learningPlanStorage;
        }
        public List<StudentViewModel> Read(StudentBindingModel model)
        {
            if (model == null)
            {
                return _studentStorage.GetFullList();
            }
            if (model.GradebookNumber.HasValue)
            {
                return new List<StudentViewModel> { _studentStorage.GetElement(model) };
            }
            return _studentStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(StudentBindingModel model)
        {
            var element = _studentStorage.GetElement(new StudentBindingModel
            {
                GradebookNumber = model.GradebookNumber,
            });
            if (element != null && element.GradebookNumber != model.GradebookNumber)
            {
                throw new Exception("Уже есть студент с такой зачетной книжкой");
            }
            if (model.GradebookNumber.HasValue)
            {
                _studentStorage.Update(model);
            }
            else
            {
                _studentStorage.Insert(model);
            }
        }
        public void Delete(StudentBindingModel model)
        {
            var element = _studentStorage.GetElement(new StudentBindingModel { GradebookNumber = model.GradebookNumber });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _studentStorage.Delete(model);
        }

        public List<StudentViewModel> SelectByTeacher(TeacherBindingModel model)
        {
            var teacher = _teacherStorage.GetElement(model);
            if (teacher == null)
            {
                throw new Exception("Преподаватель не найден");
            }
            return _studentStorage.GetByDisciplineId(teacher.DisciplineId);
        }

        public void BindingDiscipline(int gradebookNumber, int subjectId)
        {
            _studentStorage.BindingDiscipline(gradebookNumber, subjectId);
        }

        public void BindStudentLearningPlans(AddStudentToLearningPlanBindingModel model) 
        {
            var student = _studentStorage.GetElement(new StudentBindingModel { GradebookNumber = model.GradebookNumber });
            if (student == null) 
            {
                throw new Exception("Студент не найден");
            }
            student.LearningPlans.Clear();
            foreach (var learningPlanId in model.LearningPlansId)
            {
                var learningPlan = _learningPlanStorage.GetElement(new LearningPlanBindingModel { Id = learningPlanId });
                if (learningPlan == null)
                {
                    throw new Exception("План обучения не найден");
                }
                if (!student.LearningPlans.ContainsKey(learningPlanId)) 
                {
                    student.LearningPlans.Add(learningPlanId, learningPlan.LearningPlanName);
                }
            }
            _studentStorage.Update(new StudentBindingModel
            {
                GradebookNumber = student.GradebookNumber,
                StreamName = student.StreamName,
                Name = student.Name,
                DeaneryId = student.DeaneryId,
                Disciplines = student.Disciplines,
                LearningPlans = student.LearningPlans

            });
        }
    }
}
