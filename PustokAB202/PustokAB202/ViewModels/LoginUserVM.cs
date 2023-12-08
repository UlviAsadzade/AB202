﻿using System.ComponentModel.DataAnnotations;

namespace PustokAB202.ViewModels
{
	public class LoginUserVM
	{
		[Required]
		[MinLength(4)]
		public string UsernameOrEmail { get; set; }
		[Required]
		[MinLength(8)]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public bool IsRemembered { get; set; }
	}
}
