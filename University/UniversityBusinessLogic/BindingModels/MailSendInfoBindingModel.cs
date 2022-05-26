using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityBusinessLogic.BindingModels
{
    public class MailSendInfoBindingModel
    {
        public string MailAddress { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public string FileName { get; set; }
    }
}
