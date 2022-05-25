using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityBusinessLogic.OfficePackage.HelperModels
{
    public class WordParagraphWarehouser
    {
        public List<(string, WordTextPropertiesWarehouser)> Texts { get; set; }
        public WordTextPropertiesWarehouser TextProperties { get; set; }
    }
}
