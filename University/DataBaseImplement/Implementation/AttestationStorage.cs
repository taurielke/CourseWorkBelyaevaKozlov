using System;
using System.Collections.Generic;
using System.Text;
using UniversityDataBaseImplement.Models;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;
using UniversityBusinessLogic.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace UniversityDataBaseImplement.Implementation
{
    public class AttestationStorage : IAttestationStorage 
        //тут надо будет разобраться с getFiteredList, как мы будем получать аттестацию за определенный
        //период по определенному студенту. наверное чтобы были одинаковыми студенты и план обучения?
        //выбирать по номер зачетки, потом номер семестра, выбираем план обучения с нужным номером семестра 
        //естественно из тех что привязаны к зачетке. и по нему выводим все аттестации с дисциплинами
        //где айди дисциплины совпадает с айди из этого плана обучения???????????????
        //OK можно было сделать легче - просто чтобы номер зачетки и номер семестра были такие как в запросе. у каждого челика не могут семаки повторяться. бинго
    {
        public List<AttestationViewModel> GetFullList()
        {
            using var context = new UniversityDatabase();
            return context.Attestations.Select(CreateModel).ToList();
        }
        public List<AttestationViewModel> GetFilteredList(AttestationBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityDatabase();
            //add data filtering
            return context.Attestations.Where(rec => rec.RecordBookNumber == model.RecordBookNumber).Select(CreateModel).ToList();
        }
        public AttestationViewModel GetElement(AttestationBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityDatabase();
            var attestation = context.Attestations.FirstOrDefault(rec => rec.RecordBookNumber == model.RecordBookNumber || rec.Id == model.Id);
            return attestation != null ? CreateModel(attestation) : null;
        }
        public void Insert(AttestationBindingModel model)
        {
            using var context = new UniversityDatabase();
            context.Attestations.Add(CreateModel(model, new Attestation()));
            context.SaveChanges();
        }
        public void Update(AttestationBindingModel model)
        {
            using var context = new UniversityDatabase();
            var element = context.Attestations.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
        }
        public void Delete(AttestationBindingModel model)
        {
            using var context = new UniversityDatabase();
            Attestation element = context.Attestations.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Attestations.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Attestation CreateModel(AttestationBindingModel model, Attestation attestation)
        {
            attestation.RecordBookNumber = model.RecordBookNumber;
            attestation.SemesterNumber = model.SemesterNumber;
            attestation.DisciplineId = model.DisciplineId;
            attestation.Mark = model.Mark;
            attestation.ExamDate = model.ExamDate;
            return attestation;
        }

        private static AttestationViewModel CreateModel(Attestation attestation)
        {
            using var context = new UniversityDatabase();
            return new AttestationViewModel
            {
                Id = attestation.Id,
                RecordBookNumber = attestation.RecordBookNumber,
                StudentName = context.Students.FirstOrDefault(rec => rec.RecordBookNumber == attestation.RecordBookNumber)?.StudentName,
                SemesterNumber = attestation.SemesterNumber,
                DisciplineId = attestation.DisciplineId,
                DisciplineName = context.Disciplines.FirstOrDefault(rec => rec.Id == attestation.DisciplineId)?.DisciplineName,
                Mark = attestation.Mark,
                ExamDate = attestation.ExamDate
            };
        }
    }
}
