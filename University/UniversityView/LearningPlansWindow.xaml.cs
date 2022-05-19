using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UniversityBusinessLogic.BusinessLogics;
using UniversityBusinessLogic.Interfaces;
using Unity;
using UniversityBusinessLogic.ViewModels;
using UniversityBusinessLogic.BindingModels;

namespace UniversityView
{
    /// <summary>
    /// Логика взаимодействия для LearningPlans.xaml
    /// </summary>
    public partial class LearningPlans : Window
    {
        private readonly ILearningPlanLogic logic;
        public LearningPlans(ILearningPlanLogic logic)
        { 
            InitializeComponent();
            this.logic = logic;
        }
        private void LoadData()
        {
            var list = logic.Read(null);
            if (list != null)
            {
                DataGrid.ItemsSource = list;
                DataGrid.Columns[0].Visibility = Visibility.Hidden;
                DataGrid.Columns[3].Visibility = Visibility.Hidden;
                DataGrid.Columns[1].Header = "Название";
                DataGrid.Columns[2].Header = "количество";
                DataGrid.Columns[4].Header = "Id заказа";
                DataGrid.SelectedItem = null;
            }
        }
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void CreateClick(object sender, RoutedEventArgs e)
        {
            LearningPlanWindow window = App.Container.Resolve<LearningPlanWindow>();
            window.ShowDialog();
            LoadData();
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите план обучения");
                return;
            }
            int id = ((LearningPlanViewModel)DataGrid.SelectedItem).Id;
            logic.Delete(new LearningPlanBindingModel { Id = id });
            LoadData();
        }

        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите план обучения");
                return;
            }
            int id = ((LearningPlanViewModel)DataGrid.SelectedItem).Id;
            LearningPlanWindow window = App.Container.Resolve<LearningPlanWindow>();
            window.Id = id;
            window.ShowDialog();
            LoadData();
        }
    }
}
