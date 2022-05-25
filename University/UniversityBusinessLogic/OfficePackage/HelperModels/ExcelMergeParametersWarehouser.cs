using DocumentFormat.OpenXml.Spreadsheet;

namespace UniversityBusinessLogic.OfficePackage.HelperModels
{
    class ExcelMergeParametersWarehouser
    {
        public Worksheet Worksheet { get; set; }
        public string CellFromName { get; set; }
        public string CellToName { get; set; }
        public string Merge => $"{CellFromName}:{CellToName}";
    }
}
