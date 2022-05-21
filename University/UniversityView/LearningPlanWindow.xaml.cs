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
using System.Linq;

namespace UniversityView
{
    /// <summary>
    /// Логика взаимодействия для LearningPlanWindow.xaml
    /// </summary>
    public partial class LearningPlanWindow : Window
    {
        private readonly ILearningPlanLogic learningPlanLogic;
        private readonly IDisciplineLogic disciplineLogic;
        private Dictionary<int, string> disciplines;
        public int Id { set { Id = value; } }
        private int? id;
        public LearningPlanWindow(ILearningPlanLogic learningPlanLogic, IDisciplineLogic disciplineLogic)
        {
            InitializeComponent();
            this.learningPlanLogic = learningPlanLogic; 
            this.disciplineLogic = disciplineLogic;
        }
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var view = learningPlanLogic.Read(new LearningPlanBindingModel { Id = id})?[0];
                    if (view != null)
                    {
                        NameTextBox.Text = view.LearningPlanName;
                        SpecialtyTextBox.Text = view.SpecialtyName;
                        disciplines = view.DisciplineLearningPlans;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CreateClick(object sender, RoutedEventArgs e)
        {

        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = DialogResult;
            Close();
        }
    }
}
