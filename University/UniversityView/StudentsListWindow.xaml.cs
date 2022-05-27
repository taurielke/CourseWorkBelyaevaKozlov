using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.BusinessLogics;
using UniversityBusinessLogic.ViewModels;

namespace UniversityView
{
    /// <summary>
    /// Логика взаимодействия для GetListWindow.xaml
    /// </summary>
    public partial class GetListWindow : Window
    {
        private readonly TeacherLogic _teacherLogic;
        private readonly StudentLogic _studentLogic;
        private readonly DocReportWarehouserLogic _reportLogic;
        public GetListWindow(TeacherLogic teacherLogic, StudentLogic studentLogic, DocReportWarehouserLogic reportLogic)
        {
            InitializeComponent();
            _teacherLogic = teacherLogic;
            _studentLogic = studentLogic;
            _reportLogic = reportLogic;
        }

        private void GetListWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                ComboBoxTeachers.ItemsSource = _teacherLogic.Read(null);
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

        private void ComboBoxTeachers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListBoxStudent.ItemsSource = _studentLogic.SelectByTeacher(new TeacherBindingModel { Id = (ComboBoxTeachers.SelectedItem as TeacherViewModel).Id });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonWord_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxTeachers.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите преподавателя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var dialog = new SaveFileDialog { Filter = "docx|*.docx" };
            if ((bool)dialog.ShowDialog())
            {
                try
                {
                    _reportLogic.SaveTeacherStudentsToWordFile(new ReportWarehouserBindingModel
                    {
                        FileName = dialog.FileName,
                        TeacherId = (ComboBoxTeachers.SelectedItem as TeacherViewModel).Id
                    }, ListBoxStudent.ItemsSource as List<StudentViewModel>);
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ButtonExcel_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxTeachers.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите преподавателя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" };
            if ((bool)dialog.ShowDialog())
            {
                try
                {
                    _reportLogic.SaveTeacherStudentToExcelFile(new ReportWarehouserBindingModel
                    {
                        FileName = dialog.FileName,
                        TeacherId = (ComboBoxTeachers.SelectedItem as TeacherViewModel).Id
                    }, ListBoxStudent.ItemsSource as List<StudentViewModel>);
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
