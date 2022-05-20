using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityBusinessLogic.OfficePackage.HelperModels
{
    public class ExcelMergeParameters
    {
        public string CellFromName { get; set; }
        public string CellToName { get; set; }
        public string Merge => $"{CellFromName}:{CellToName}";
    }
}
