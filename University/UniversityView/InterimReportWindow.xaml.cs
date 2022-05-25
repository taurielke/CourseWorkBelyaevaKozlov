using System;
using System.Linq;
using System.Windows;
using Unity;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.BusinessLogics;
using UniversityBusinessLogic.ViewModels;

namespace UniversityView
{
    /// <summary>
    /// Логика взаимодействия для InterimReportWindow.xaml
    /// </summary>
    public partial class InterimReportWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private int? id;
        public int TeacherId
        {
            get { return Convert.ToInt32((ComboBoxTeacher.SelectedItem as TeacherViewModel).Id); }
            set
            {
                lectorId = value;
            }
        }
        private int? lectorId;

        private readonly InterimReportLogic _logicInterimReport;
        private readonly TeacherLogic _logicTeacher;

        public InterimReportWindow(InterimReportLogic checkListLogic, TeacherLogic lectorLogic)
        {
            InitializeComponent();
            this._logicInterimReport = checkListLogic;
            this._logicTeacher = lectorLogic;
        }

        private void InterimReportWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxTeacher.ItemsSource = _logicTeacher.Read(null);
            if (id.HasValue)
            {
                try
                {
                    ComboBoxTeacher.SelectedItem = SetValue(lectorId);
                    var view = _logicInterimReport.Read(new InterimReportBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        DatePicker.SelectedDate = view.DateOfExam;
                        ComboBoxTeacher.SelectedItem = view.TeacherName;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (DatePicker.SelectedDate == null)
            {
                MessageBox.Show("Заполните дату проведения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ComboBoxTeacher.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите преподавателя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                _logicInterimReport.CreateOrUpdate(new InterimReportBindingModel
                {
                    Id = id,
                    DateOfExam = (DateTime)DatePicker.SelectedDate,
                    TeacherId = TeacherId
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
        private TeacherViewModel SetValue(int? value)
        {
            foreach (var item in ComboBoxTeacher.Items)
            {
                if ((item as TeacherViewModel).Id == value)
                {
                    return item as TeacherViewModel;
                }
            }
            return null;
        }

    }
}
