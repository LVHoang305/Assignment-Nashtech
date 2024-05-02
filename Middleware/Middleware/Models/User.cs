using System;
using System.ComponentModel.DataAnnotations;
namespace Middleware.Models
{
	public class Users
	{
        [Key] public int ID { get; set; }
        public string Name { get; set; }
		public string Password { get; set; }
	}
}

