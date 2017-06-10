using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LearnerDictionary.Models
{
	[Table("Language")]
	public class Language
	{
		public int Id { get; set; }
		public string Value { get; set; }

		public virtual ICollection<Word> Words { get; set; }
	}
}