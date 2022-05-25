using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityBusinessLogic.OfficePackage.HelperModels
{
    public class WordTextPropertiesWarehouser
    {
        public string Size { get; set; }
        public bool Bold { get; set; }
        public JustificationValues JustificationValues { get; set; }
    }
}
