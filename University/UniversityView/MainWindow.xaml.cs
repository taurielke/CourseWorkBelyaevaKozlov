using System.Windows;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Unity;
using UniversityBusinessLogic.BusinessLogics;

namespace UniversityView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public string Login { set { login = value; } }

        private string login;

        private readonly DepartmentLogic _logicDepartment;

        public MainWindow(DepartmentLogic logic)
        {
            InitializeComponent();
            _logicDepartment = logic;
        }


        private void MenuItemTeachers_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<TeachersWindow>();
            form.Login = login;
            form.ShowDialog();
        }

        private void MenuItemDisciplines_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<DisciplinesWindow>();
            form.Login = login;
            form.ShowDialog();
        }
    }
}