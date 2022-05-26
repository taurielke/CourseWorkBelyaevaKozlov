﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace UniversityBusinessLogic.ViewModels
{
    public class LearningPlanViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название потока")]
        public string LearningPlanName { get; set; }

        [DisplayName("Количество часов")]
        public int Hours { get; set; }
        public Dictionary<int, string> LearningPlanTeachers { get; set; }
        public Dictionary<int, string> LearningPlanStudents { get; set; }
        public int? DeaneryId { get; set; }
    }
}
