using Microsoft.Reporting.WinForms;
using System;
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
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            reportViewer.LocalReport.ReportPath = "../../Report.rdlc";
        }

        private void ReportWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxDiscipline.ItemsSource = _logicDiscipline.Read(new DisciplineBindingModel { DepartmentLogin = login });
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
                string desc = $"{discipline.Name}\nc {datePickerFrom.SelectedDate.Value.ToShortDateString()} по {datePickerTo.SelectedDate.Value.ToShortDateString()}";
                ReportParameter parameterPeriod = new ReportParameter("ReportParameterPeriod", desc);
                reportViewer.LocalReport.SetParameters(parameterPeriod);

                var dataSource = _logic.GetInterimReports(new ReportWarehouserBindingModel
                {
                    DateFrom = datePickerFrom.SelectedDate,
                    DateTo = datePickerTo.SelectedDate,
                    DisciplineId = discipline.Id
                });
                ReportDataSource source = new ReportDataSource("DataSetDiscipline", dataSource);
                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(source);
                reportViewer.RefreshReport();
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
                client.Credentials = new NetworkCredential(App.emailSender, App.emailPassword);
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

