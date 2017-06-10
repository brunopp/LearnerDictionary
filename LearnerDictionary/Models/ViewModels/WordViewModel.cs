using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnerDictionary.Models.ViewModels
{
	public class WordViewModel
	{
		public int Id { get; set; }
		public string Value { get; set; }
		public string Definition { get; set; }
		public string Language { get; set; }
		public WordStatus Status { get; set; }
        public double Score { get; set; }
        public bool HasDefintion { get; set; }
    }
}