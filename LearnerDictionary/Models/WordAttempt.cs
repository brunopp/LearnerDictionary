using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LearnerDictionary.Models
{
	[Table("WordAttempt")]
	public class WordAttempt
	{
		public WordAttempt()
		{
			CreatedUtc = DateTime.UtcNow;
		}

		public int Id { get; set; }

		[ForeignKey("Word")]
		public int WordId { get; set; }
		public virtual Word Word { get; set; }

		public DateTime CreatedUtc { get; set; }

		public bool Recognize { get; set; }

		public double Score
		{
			get
			{
                return Recognize ? 1 : 0;
			}
		}
	}
}