using Newtonsoft.Json;
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
		[JsonIgnore]
		public IEnumerable<WordAttempt> Attempts { get; set; }
		public HttpPostedFileBase File { get; set; }
		public bool HasDefinition => !string.IsNullOrWhiteSpace(Definition);

		private int GetStraight(TimeSpan ts)
		{
			var c = 0;
			var attempts = Attempts.Where(x => x.CreatedUtc.Today() == DateTime.UtcNow.Today().Add(ts)).OrderByDescending(x => x.CreatedUtc);
			foreach (var attempt in attempts)
			{
				if (attempt.Recognize)
				{
					c++;
				}
				else
				{
					break;
				}
			}
			return c;
		}
		private int GetTotal(TimeSpan ts)
		{
			return Attempts.Count(x => x.CreatedUtc.Today() == DateTime.UtcNow.Today().Add(ts));
		}

		public int TodayStraight => GetStraight(new TimeSpan(0, 0, 0));
		public int TodayTotal => GetTotal(new TimeSpan(0, 0, 0));

		public int YesterdayStraight => GetStraight(new TimeSpan(-24, 0, 0));
		public int YesterdayTotal => GetTotal(new TimeSpan(-24, 0, 0));

		public int DayBeforeYesterdayStraight => GetStraight(new TimeSpan(-48, 0, 0));
		public int DayBeforeYesterdayTotal => GetTotal(new TimeSpan(-48, 0, 0));

		public int Total => Attempts.Count();
	}
}