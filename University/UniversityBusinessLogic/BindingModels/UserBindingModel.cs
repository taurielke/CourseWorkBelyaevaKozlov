using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityBusinessLogic.BindingModels
{
    public class UserBindingModel
    {
        public int? Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
