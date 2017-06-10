using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Web;

namespace LearnerDictionary.Models
{
	[Table("Word")]
	public class Word
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
    }

    public enum WordStatus
    {
        Unknown,
        Shaky,
        Known
    }
}