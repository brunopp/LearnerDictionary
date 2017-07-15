using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnerDictionary.Models
{
	public static class DateTimeExtentions
	{
		public static DateTime Today(this DateTime dateTime)
		{
			return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
		}
	}
}