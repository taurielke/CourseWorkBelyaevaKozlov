using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using Unity.Lifetime;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.BusinessLogics;
using UniversityDataBaseImplement.Implementation;

namespace UniversityView
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IUnityContainer container = null;
        public static IUnityContainer Container
        {
            get
            {
                if (container == null)
                {
                    container = BuildUnityContainer();
                }
                return container;
            }
        }
        /*protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AuthorizationWindow authorizationWindow = Container.Resolve<AuthorizationWindow>();
            authorizationWindow.ShowDialog();
        }*/
        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IAttestationStorage, AttestationStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IAttestationLogic, AttestationLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDisciplineLogic, DisciplineLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDisciplineStorage, DisciplineStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IInterimReportLogic, InterimReportLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IInterimReportStorage, InterimReportStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ILearningPlanLogic, LearningPlanLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ILearningPlanStorage, LearningPlanStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStudentLogic, StudentLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStudentStorage, StudentStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITeacherLogic, TeacherLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITeacherStorage, TeacherStorage>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}
