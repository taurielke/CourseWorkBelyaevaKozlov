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
    /// Логика взаимодействия для TeacherWindow.xaml
    /// </summary>
    public partial class TeacherWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private int? id;
        public int DisciplineID
        {
            get { return Convert.ToInt32((ComboBoxDiscipline.SelectedItem as DisciplineViewModel).Id); }
            set
            {
                subjectId = value;
            }
        }
        private int? subjectId;
        public string Login { set { login = value; } }

        private string login;

        private readonly TeacherLogic _logicTeacher;
        private readonly DisciplineLogic _logicDiscipline;

        public TeacherWindow(TeacherLogic lectorLogic, DisciplineLogic subjectLogic)
        {
            InitializeComponent();
            this._logicTeacher = lectorLogic;
            this._logicDiscipline = subjectLogic;
        }

        private void TeacherWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxDiscipline.ItemsSource = _logicDiscipline.Read(new DisciplineBindingModel { DepartmentLogin = login });
            ComboBoxDiscipline.SelectedItem = SetValue(subjectId);
            if (id.HasValue)
            {
                try
                {
                    var view = _logicTeacher.Read(new TeacherBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        TextBoxName.Text = view.TeacherName;
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
            if (string.IsNullOrEmpty(TextBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ComboBoxDiscipline.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите дисциплину", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                _logicTeacher.CreateOrUpdate(new TeacherBindingModel
                {
                    Id = id,
                    TeacherName = TextBoxName.Text,
                    DisciplineID = DisciplineID
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

        private DisciplineViewModel SetValue(int? value)
        {
            foreach (var item in ComboBoxDiscipline.Items)
            {
                if ((item as DisciplineViewModel).Id == value)
                {
                    return item as DisciplineViewModel;
                }
            }
            return null;
        }
    }
}
