using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.OfficePackage.HelperEnums;
using UniversityBusinessLogic.OfficePackage.HelperModels;

namespace UniversityBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToWord
    {
        public void CreateDoc(WordInfo info)
        {
            CreateWord(info);
            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)> { (info.Title, new WordTextProperties { Bold = true, Size = "24", }) },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });

            foreach (var lp in info.LearningPlanDisciplines)
            {
                var learningPlanDisciplines = new List<(string, WordTextProperties)>();
                learningPlanDisciplines.Add((lp.LearningPlanName + ": ", new WordTextProperties { Bold = true, Size = "24", }));

                foreach (var d in lp.Disciplines)
                {
                    learningPlanDisciplines.Add((d + "; ", new WordTextProperties { Size = "24", }));

                }

                CreateParagraph(new WordParagraph
                {
                    Texts = learningPlanDisciplines,
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationType = WordJustificationType.Both
                    }
                });
            }

            SaveWord(info);
        }
        protected abstract void CreateWord(WordInfo info);
        protected abstract void CreateParagraph(WordParagraph paragraph);
        protected abstract void SaveWord(WordInfo info);
    }
}
