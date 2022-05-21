using System;
using System.Windows;
using Unity;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.BusinessLogics;

namespace UniversityView
{
    /// <summary>
    /// Логика взаимодействия для DisciplineWindow.xaml
    /// </summary>
    public partial class DisciplineWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private int? id;

        public string Login { set { login = value; } }

        private string login;

        private readonly DisciplineLogic _logicDiscipline;

        public DisciplineWindow(DisciplineLogic logic)
        {
            InitializeComponent();
            this._logicDiscipline = logic;
        }

        private void DisciplineWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var view = _logicDiscipline.Read(new DisciplineBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        TextBoxName.Text = view.DisciplineName;
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
            try
            {
                _logicDiscipline.CreateOrUpdate(new DisciplineBindingModel
                {
                    Id = id,
                    DisciplineName = TextBoxName.Text,
                    DepartmentLogin = login
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
    }
}
