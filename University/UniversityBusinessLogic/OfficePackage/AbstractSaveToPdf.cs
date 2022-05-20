using System;
using System.Collections.Generic;
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
                Text = $"с{ info.DateFrom.ToShortDateString() } по { info.DateTo.ToShortDateString() }",
                Style = "Normal"
            });
            CreateTable(new List<string> { "3cm", "6cm", "3cm", "2cm", "3cm" });
            CreateRow(new PdfRowParameters
            {
                Texts = new List<string> { "Дата заказа", "Изделие", "Количество", "Сумма", "Статус" },
                Style = "NormalTitle",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });
            foreach (var attestation in info.Attestations)
            {
                CreateRow(new PdfRowParameters
                {
                    Texts = new List<string> {attestation.SemesterNumber.ToString(), attestation.DisciplineName, attestation.Mark.ToString(), attestation.ExamDate.ToShortDateString()},
                    Style = "Normal",
                    ParagraphAlignment = PdfParagraphAlignmentType.Left
                });
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
