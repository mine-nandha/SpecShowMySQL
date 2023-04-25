using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SpecShow.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string? UserName { get; set; }
		public string? FullName { get; set; }
		[DataType(DataType.EmailAddress)]
        public string? UserEmail { get; set; }
		[DataType(DataType.Password)]
        public string? Password { get; set; }

	}
}
