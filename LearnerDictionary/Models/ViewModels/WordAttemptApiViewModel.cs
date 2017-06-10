using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnerDictionary.Models.ViewModels
{
	public class WordAttemptApiViewModel
	{
		public int WordId { get; set; }
        public string WordValue { get; set; }
        public bool Recognize { get; set; }
        public string Defintion { get; set; }
	}
}