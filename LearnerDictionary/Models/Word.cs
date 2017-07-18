using Microsoft.Owin.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Web;

namespace LearnerDictionary.Models
{
	[Table("Word")]
	public class Word : ISystemClock
	{
		public Word()
		{
			CreatedUtc = DateTime.UtcNow;
		}

		public int Id { get; set; }

		public DateTime CreatedUtc { get; set; }

		[Index(IsUnique = true)]
		public string Value { get; set; }

		[ForeignKey("Language")]
		public int LanguageId { get; set; }
		public virtual Language Language { get; set; }

		public string Definition { get; set; }

		public virtual ICollection<WordAttempt> Attempts { get; set; }

		public virtual ICollection<WordExample> Examples { get; set; }

        private double? _score;
		public double Score
		{
			get
			{
                if (_score.HasValue)
                {
                    return _score.Value;
                }

				if (!Attempts.Any())
				{
                    _score = 0;
				}
                else
                {
                    _score = Attempts.OrderByDescending(x => x.CreatedUtc).Take(5).Sum(a => a.Score);
                }

                return _score.Value;
			}
		}
		
		public bool HasAttempts
		{
			get
			{
				return Examples.Any();
			}
		}

        private WordStatus? _status = null;
        public WordStatus Status
        {
            get
            {
                if (_status.HasValue)
                {
                    return _status.Value;
                }

                _status = WordStatus.Unknown;
                if (Score > 0)
                {
                    if (Score >= 5)
                    {
                        _status = WordStatus.Known;
                    }
                    else
                    {
                        _status = WordStatus.Shaky;
                    }
                }
                
                return _status.Value;
            }
        }

        public bool HasDefintion => !string.IsNullOrWhiteSpace(Definition);

		public bool IsLearning { get; set; }

		/// <summary>
		/// Returns true if the learning is done.
		/// For the word to have learning done the word's latest 15 attempts must be good and spread over at least 3 days.
		/// </summary>
		public bool LearningDone
		{
			get
			{
				if (!Attempts.Any())
				{
					return false;
				}

				// take 5 latest attempts grouped by day
				var groupedByDay = Attempts.GroupBy(x => x.CreatedUtc.Today(), e => e, (k, v) => v.OrderByDescending(x => x.CreatedUtc.Today()));
				if (groupedByDay.Count() < 3)
				{
					return false;
				}

				var last15Attempts = groupedByDay.SelectMany(x => x).OrderByDescending(x => x.CreatedUtc).Take(15);
				if (last15Attempts.All(x => x.Recognize))
				{
					return true;
				}

				return false;
			}
		}

		/// <summary>
		/// Returns true if the word is relevant today.
		/// The word is relevant if the word's 4 latest attempts today are not all recognized.
		/// </summary>
		public bool RelevantToday
		{
			get
			{
				var todayAttempts = Attempts.Where(x => x.CreatedUtc.Today() == UtcNow.DateTime.Today()).OrderByDescending(x => x.CreatedUtc);
				if (todayAttempts.Count() >= 4 && todayAttempts.Take(4).All(x => x.Recognize))
				{
					return false;
				}

				return true;
			}
		}

		private DateTimeOffset? _utcNow;
		[NotMapped]
		public DateTimeOffset UtcNow
		{
			get
			{
				if (_utcNow == null)
				{
					return DateTimeOffset.UtcNow;
				}

				return _utcNow.Value;
			}
			set
			{
				_utcNow = value;
			}
		}

		public bool Deleted { get; set; }
	}

    public enum WordStatus
    {
        Unknown,
        Shaky,
        Known
    }
}