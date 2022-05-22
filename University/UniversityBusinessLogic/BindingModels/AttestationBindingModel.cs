using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityBusinessLogic.BindingModels
{
    public class AttestationBindingModel
    {
        public int? Id { get; set; }

        public DateTime Date { get; set; }

        public string StudentId { get; set; }

        public string DenearyLogin { get; set; }
    }
}
