﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace UniversityBusinessLogic.ViewModels
{
	public class DeaneryViewModel
	{
		[DisplayName("Логин")]
		public string Login { get; set; }
		[DisplayName("Название")]
		public string Name { get; set; }
		[DisplayName("Пароль")]
		public string Password { get; set; }
	}
}
