using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnerDictionary.Models.ViewModels
{
	public class WordFormViewModel
	{
		public WordFormViewModel()
		{
			Languages = new List<SelectListItem>()
			{
				new SelectListItem
				{
					Value = "en-US",
					Text = "English"
				},
				new SelectListItem
				{
					Value = "da-DK",
					Text = "Danish"
				}
			};
		}

		public int Id { get; set; }
		public string Value { get; set; }
		public string Definition { get; set; }
		public string Language { get; set; }
		public IEnumerable<SelectListItem> Languages { get; set; }
		public IEnumerable<WordExample> Examples { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}