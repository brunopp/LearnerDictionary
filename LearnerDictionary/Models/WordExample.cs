using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LearnerDictionary.Models
{
	[Table("WordExample")]
	public class WordExample
	{
		public int Id { get; set; }

		[ForeignKey("Word")]
		public int WordId { get; set; }
		public virtual Word Word { get; set; }

		public string Text { get; set; }
	}
}