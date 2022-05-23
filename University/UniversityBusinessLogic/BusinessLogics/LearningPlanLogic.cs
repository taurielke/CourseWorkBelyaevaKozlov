using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.BusinessLogics
{
	public class LearningPlanLogic : ILearningPlanLogic
	{
		private readonly ILearningPlanStorage _educationPlanStorage;
		public LearningPlanLogic(ILearningPlanStorage educationPlanStorage)
		{
			_educationPlanStorage = educationPlanStorage;
		}
		public List<LearningPlanViewModel> Read(LearningPlanBindingModel model)
		{
			if (model == null)
			{
				return _educationPlanStorage.GetFullList();
			}
			if (model.Id.HasValue)
			{
				return new List<LearningPlanViewModel> { _educationPlanStorage.GetElement(model) };
			}
			return _educationPlanStorage.GetFilteredList(model);
		}
		public void CreateOrUpdate(LearningPlanBindingModel model)
		{
			var element = _educationPlanStorage.GetElement(new LearningPlanBindingModel
			{
				StreamName = model.StreamName,
				Hours = model.Hours
			});
			if (element != null && element.Id != model.Id)
			{
				throw new Exception("Уже есть план обучения для данного потока с таким количеством часов");
			}
			if (model.Id.HasValue)
			{
				_educationPlanStorage.Update(model);
			}
			else
			{
				_educationPlanStorage.Insert(model);
			}
		}
		public void Delete(LearningPlanBindingModel model)
		{
			var element = _educationPlanStorage.GetElement(new LearningPlanBindingModel { Id = model.Id });
			if (element == null)
			{
				throw new Exception("Элемент не найден");
			}
			_educationPlanStorage.Delete(model);
		}
	}
}
