using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityBusinessLogic.OfficePackage.HelperEnums;
using UniversityBusinessLogic.OfficePackage.HelperModels;

namespace UniversityBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToPdf
    {
        public void CreateDoc(PdfInfo info)
        {
            CreatePdf(info);
            CreateParagraph(new PdfParagraph
            {
                Text = info.Title,
                Style = "NormalTitle"
            });
            CreateParagraph(new PdfParagraph
            {
                Text = $"с { info.DateFrom.ToShortDateString() } по { info.DateTo.ToShortDateString() }",
                Style = "Normal"
            });
            CreateTable(new List<string> { "5cm", "3cm", "5cm", "4cm" });
            CreateRow(new PdfRowParameters
            {
                Texts = new List<string> { "ФИО студента", "Дата аттестации", "Планы обучения", "Дисциплины" },
                Style = "NormalTitle",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });
            foreach (var att in info.Attestations)
            {
                for (int i = 0; i < att.LearningPlanDisciplines.Count; i++)
                {
                    if (i == 0)
                    {
                        CreateRow(new PdfRowParameters
                        {
                            Texts = new List<string> { att.StudentName,
                                                att.AttestationDate.ToShortDateString(),
                                                att.LearningPlanDisciplines[i].Item1.LearningPlanName,
                                                string.Join(", ", att.LearningPlanDisciplines[i].Item2.Select(disc => disc.Name).ToList())},
                            Style = "Normal",
                            ParagraphAlignment = PdfParagraphAlignmentType.Left
                        });
                    }
                    else
                    {
                        CreateRow(new PdfRowParameters
                        {
                            Texts = new List<string> { String.Empty, String.Empty,
                                                att.LearningPlanDisciplines[i].Item1.LearningPlanName,
                                               string.Join(", ", att.LearningPlanDisciplines[i].Item2.Select(disc => disc.Name).ToList())},
                            Style = "Normal",
                            ParagraphAlignment = PdfParagraphAlignmentType.Left
                        });
                    }
                }
            }
            SavePdf(info);
        }
        protected abstract void CreatePdf(PdfInfo info);
        protected abstract void CreateParagraph(PdfParagraph paragraph);
        protected abstract void CreateTable(List<string> columns);
        protected abstract void CreateRow(PdfRowParameters rowParameters);
        protected abstract void SavePdf(PdfInfo info);
    }
}
