using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Role
{
	public class RoleUpdateVM
	{
		[Required]
		public string Name { get; set; }
	}
}
