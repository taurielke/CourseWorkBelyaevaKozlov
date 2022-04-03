using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityBusinessLogic.Interfaces
{
    public interface IUserStorage
    {
        List<UserViewModel> GetFullList();
        UserViewModel GetElement(UserBindingModel model);
        void Insert(UserBindingModel model);
        void Update(UserBindingModel model);
        void Delete(UserBindingModel model);
    }
}