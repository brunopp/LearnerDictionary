using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnerDictionary.Models.ViewModels
{
	public class WordExampleApiViewModel
	{
		public int Id { get; set; }
		public int WordId { get; set; }
        public string Text { get; set; }
	}
}