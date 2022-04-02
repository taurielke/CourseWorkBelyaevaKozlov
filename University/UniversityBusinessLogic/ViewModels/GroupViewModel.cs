using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace UniversityBusinessLogic.ViewModels
{
    public class GroupViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название группы")]
        public string GroupName { get; set; }
    }
}
