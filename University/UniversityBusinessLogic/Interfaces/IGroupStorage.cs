using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.Interfaces
{
    public interface IGroupStorage
    {
        List<GroupViewModel> GetFullList();
        List<GroupViewModel> GetFilteredList(GroupBindingModel model);
        GroupViewModel GetElement(GroupBindingModel model);
        void Insert(GroupBindingModel model);
        void Update(GroupBindingModel model);
        void Delete(GroupBindingModel model);
    }
}
