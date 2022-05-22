using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
namespace UniversityBusinessLogic.BusinessLogics
{
	public class DisciplineLogic : IDisciplineLogic
	{
		private readonly IDisciplineStorage _subjectStorage;
		public DisciplineLogic(IDisciplineStorage subjectStorage)
		{
			_subjectStorage = subjectStorage;
		}
		public List<DisciplineViewModel> Read(DisciplineBindingModel model)
		{
			if (model == null)
			{
				return _subjectStorage.GetFullList();
			}
			if (model.Id.HasValue)
			{
				return new List<DisciplineViewModel> { _subjectStorage.GetElement(model) };
			}
			return _subjectStorage.GetFilteredList(model);
		}
		public void CreateOrUpdate(DisciplineBindingModel model)
		{
			var element = _subjectStorage.GetElement(new DisciplineBindingModel
			{
				Name = model.Name
			});
			if (element != null && element.Id != model.Id)
			{
				throw new Exception("Уже есть предмет с таким названием");
			}
			if (model.Id.HasValue)
			{
				_subjectStorage.Update(model);
			}
			else
			{
				_subjectStorage.Insert(model);
			}
		}
		public void Delete(DisciplineBindingModel model)
		{
			var element = _subjectStorage.GetElement(new DisciplineBindingModel { Id = model.Id });
			if (element == null)
			{
				throw new Exception("Элемент не найден");
			}
			_subjectStorage.Delete(model);
		}
	}
}
