using System;
using System.Linq;
using System.Windows;
using Unity;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.BusinessLogics;

namespace UniversityView
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {

        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly DepartmentLogic _logicDepartment;

        public AuthorizationWindow(DepartmentLogic logicDepartment)
        {
            InitializeComponent();
            this._logicDepartment = logicDepartment;
        }

        private void ButtonEnter_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxLogin.Text))
            {
                MessageBox.Show("Пустое поле 'Логин'", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxPassword.Password))
            {
                MessageBox.Show("Пустое поле 'Пароль'", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var viewDepartment = _logicDepartment.Read(new DepartmentBindingModel
                {
                    DepartmentLogin = TextBoxLogin.Text,
                });
                if (viewDepartment != null && viewDepartment[0] != null && viewDepartment.Count > 0 && viewDepartment[0].Password == TextBoxPassword.Password)
                {
                    var window = Container.Resolve<MainWindow>();
                    //window.Login = viewDepartment[0].Login;
                    window.ShowDialog();
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<RegistrationWindow>();
            window.ShowDialog();
        }
    }
}
