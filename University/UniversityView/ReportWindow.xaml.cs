
using BoldReports.UI.Xaml;
using BoldReports.Windows;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Windows;
using Unity;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.BusinessLogics;
using UniversityBusinessLogic.ViewModels;

namespace UniversityView
{
    /// <summary>
    /// Логика взаимодействия для ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public string Login { set { login = value; } }

        private string login;

        private readonly ReportWarehouserLogic _logic;
        private readonly DisciplineLogic _logicDiscipline;
        private readonly DepartmentLogic _logicDepartment;
        public ReportWindow(ReportWarehouserLogic logic, DisciplineLogic logicDiscipline, DepartmentLogic departmentLogic)
        {
            _logic = logic;
            _logicDiscipline = logicDiscipline;
            _logicDepartment = departmentLogic;
            InitializeComponent();
            ReportViewer.ReportPath = @"C:\Users\Алексей\Documents\GitHub\CourseWorkBelyaevaKozlov\University\UniversityView\Report.rdlc";
            ReportViewer.ProcessingMode = BoldReports.UI.Xaml.ProcessingMode.Local;
        }

        private void ReportWindow_Loaded(object sender, RoutedEventArgs e)
        {
            List<DisciplineViewModel> models = _logicDiscipline.Read(new DisciplineBindingModel { DepartmentLogin = login });
            ComboBoxDiscipline.ItemsSource = models;
        }


        private void ButtonShow_Click(object sender, RoutedEventArgs e)
        {
            if (datePickerFrom.SelectedDate == null || datePickerTo.SelectedDate == null)
            {
                MessageBox.Show("Вы не указали дату начала или дату окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ComboBoxDiscipline.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не указали дисциплину", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (datePickerFrom.SelectedDate >= datePickerTo.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var discipline = (DisciplineViewModel)ComboBoxDiscipline.SelectedItem;
                var dataSource = _logic.GetInterimReports(new ReportWarehouserBindingModel
                {
                    DateFrom = datePickerFrom.SelectedDate,
                    DateTo = datePickerTo.SelectedDate,
                    DisciplineId = discipline.Id
                });
                ReportDataSource source = new ReportDataSource("DataSetDiscipline", dataSource);
                ReportViewer.DataSources.Clear();
                ReportViewer.DataSources.Add(source);

                
                string desc = $"{discipline.Name}\nc {datePickerFrom.SelectedDate.Value.ToShortDateString()} по {datePickerTo.SelectedDate.Value.ToShortDateString()}";
                var values = new List<string>();
                values.Add(desc);
                ReportParameter parameterPeriod = new ReportParameter { Name = "ReportParameterPeriod", Values = values };
                var parameters = new List<ReportParameter>();
                parameters.Add(parameterPeriod);
                ReportViewer.SetParameters(parameters);

                
                ReportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [Obsolete]
        private void ButtonSendToMail(object sender, RoutedEventArgs e)
        {
            if (datePickerFrom.SelectedDate >= datePickerTo.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MailMessage msg = new MailMessage();
            SmtpClient client = new SmtpClient();
            try
            {
                var discipline = (DisciplineViewModel)ComboBoxDiscipline.SelectedItem;
                var department = _logicDepartment.Read(new DepartmentBindingModel { DepartmentLogin = login })[0];
                msg.Subject = "Отчёт по дисциплине";
                msg.Body = $"Отчёт по дисциплине {discipline.Name} за период c " + datePickerFrom.SelectedDate.Value.ToShortDateString() +
                " по " + datePickerTo.SelectedDate.Value.ToShortDateString();
                msg.From = new MailAddress(App.emailSender);
                msg.To.Add(department.Email);
                msg.IsBodyHtml = true;
                _logic.SaveInterimReportsByDateByDisciplineToPdfFile(new ReportWarehouserBindingModel
                {
                    FileName = App.defaultReportPath,
                    DateFrom = datePickerFrom.SelectedDate,
                    DateTo = datePickerTo.SelectedDate,
                    DisciplineId = discipline.Id
                });
                string file = App.defaultReportPath;
                Attachment attach = new Attachment(file, MediaTypeNames.Application.Octet);
                ContentDisposition disposition = attach.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(file);
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
                msg.Attachments.Add(attach);
                client.Host = App.emailHost;
                client.Port = App.emailPort;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(textBoxMail.Text, App.emailPassword);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(msg);
                MessageBox.Show("Сообщение отправлено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

