using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;
using UniversityDataBaseImplement.Models;

namespace UniversityDataBaseImplement.Implements
{
    public class AttestationStorage : IAttestationStorage
    {
        public List<AttestationViewModel> GetFullList()
        {
            using (var context = new UniversityDatabase())
            {
                return context.Attestations
                .Select(rec => new AttestationViewModel
                {
                    Id = rec.Id,
                    Date = rec.Date,
                    StudentName = context.Students.FirstOrDefault(x => x.GradebookNumber == rec.StudentGradebookNumber).Name
                }).ToList();
            }
        }
        public List<AttestationViewModel> GetFilteredList(AttestationBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                return context.Attestations
                .Where(rec => rec.StudentGradebookNumber == model.StudentId && rec.DeaneryId== model.DeaneryId)
                .Select(rec => new AttestationViewModel
                {
                    Id = rec.Id,
                    Date = rec.Date,
                    StudentName = context.Students.FirstOrDefault(x => x.GradebookNumber == rec.StudentGradebookNumber).Name
                })
                .ToList();
            }
        }
        public AttestationViewModel GetElement(AttestationBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                var Cert = context.Attestations
                .FirstOrDefault(rec => rec.Date == model.Date || rec.DeaneryId == model.DeaneryId);
                return Cert != null ?
                new AttestationViewModel
                {
                    Id = Cert.Id,
                    Date = Cert.Date,
                    StudentId = Cert.StudentGradebookNumber,
                    StudentName = context.Students.FirstOrDefault(x => x.GradebookNumber == x.GradebookNumber).Name
                } :
                null;
            }
        }
        public void Insert(AttestationBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                context.Attestations.Add(CreateModel(model, new Attestation()));
                context.SaveChanges();
            }
        }
        public void Update(AttestationBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                var element = context.Attestations.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        public void Delete(AttestationBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
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
        }
        private Attestation CreateModel(AttestationBindingModel model, Attestation Attestation)
        {
            Attestation.Date = model.Date;
            Attestation.DeaneryId = (int)model.DeaneryId;
            return Attestation;
        }
    }
}
