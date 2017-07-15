using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LearnerDictionary.Models;
using System.Collections.Generic;

namespace LearnerDictionary.Test
{
	[TestClass]
	public class TestWord
	{
		[TestMethod]
		public void TestRelevantToday_IsNotLearning_ReturnsFalse()
		{
			var word = new Word();
			word.UtcNow = new DateTime(2000, 1, 1);

			Assert.IsFalse(word.RelevantToday);
		}

		[TestMethod]
		public void TestRelevantToday_IsNotLearningWithBadAttempts_ReturnsFalse()
		{
			var word = new Word();
			word.Attempts = new List<WordAttempt>();
			for (int i = 0; i < 4; i++)
			{
				word.Attempts.Add(new WordAttempt
				{
					CreatedUtc = new DateTime(2000, 1, 1)
				});
			}
			word.UtcNow = new DateTime(2000, 1, 1);

			Assert.IsFalse(word.RelevantToday);
		}

		[TestMethod]
		public void TestRelevantToday_IsLearningWithBadAttempts_ReturnsTrue()
		{
			var word = new Word();
			word.IsLearning = true;
			word.Attempts = new List<WordAttempt>();
			for (int i = 0; i < 4; i++)
			{
				word.Attempts.Add(new WordAttempt
				{
					CreatedUtc = new DateTime(2000, 1, 1)
				});
			}
			word.UtcNow = new DateTime(2000, 1, 1);

			Assert.IsTrue(word.RelevantToday);
		}

		[TestMethod]
		public void TestRelevantToday_IsLearningWith4GoodAttempts_ReturnsTrue()
		{
			var word = new Word();
			word.IsLearning = true;
			word.Attempts = new List<WordAttempt>();
			for (int i = 0; i < 4; i++)
			{
				word.Attempts.Add(new WordAttempt
				{
					CreatedUtc = new DateTime(2000, 1, 1),
					Recognize = true
				});
			}
			word.UtcNow = new DateTime(2000, 1, 1);

			Assert.IsTrue(word.RelevantToday);
		}

		[TestMethod]
		public void TestRelevantToday_IsLearningWith5GoodAttempts_ReturnsFalse()
		{
			var word = new Word();
			word.Attempts = new List<WordAttempt>();
			word.IsLearning = true;
			for (int i = 0; i < 5; i++)
			{
				word.Attempts.Add(new WordAttempt
				{
					CreatedUtc = new DateTime(2000, 1, 1),
					Recognize = true
				});
			}
			word.UtcNow = new DateTime(2000, 1, 1);

			Assert.IsFalse(word.RelevantToday);
		}

		[TestMethod]
		public void TestRelevantToday_IsLearningWith5GoodAttemptsYesterday_ReturnsTrue()
		{
			var word = new Word();
			word.Attempts = new List<WordAttempt>();
			word.IsLearning = true;
			for (int i = 0; i < 5; i++)
			{
				word.Attempts.Add(new WordAttempt
				{
					CreatedUtc = new DateTime(1999, 12, 31),
					Recognize = true
				});
			}
			word.UtcNow = new DateTime(2000, 1, 1);

			Assert.IsTrue(word.RelevantToday);
		}
		
		[TestMethod]
		public void TestLearningDone_HasNoAttempts_ReturnsFalse()
		{
			var word = new Word();
			word.Attempts = new List<WordAttempt>();
			word.IsLearning = true;
			word.UtcNow = new DateTime(2000, 1, 1);

			Assert.IsFalse(word.LearningDone);
		}

		[TestMethod]
		public void TestLearningDone_Has20GoodAttemptsOnSameDay_ReturnsFalse()
		{
			var word = new Word();
			word.Attempts = new List<WordAttempt>();
			word.IsLearning = true;
			for (int i = 0; i < 20; i++)
			{
				word.Attempts.Add(new WordAttempt
				{
					CreatedUtc = new DateTime(1999, 12, 31),
					Recognize = true
				});
			}
			word.UtcNow = new DateTime(2000, 1, 1);

			Assert.IsFalse(word.LearningDone);
		}

		[TestMethod]
		public void TestLearningDone_Has20GoodAttemptsOn20DifferentDays_ReturnsTrue()
		{
			var word = new Word();
			word.Attempts = new List<WordAttempt>();
			word.IsLearning = true;
			for (int i = 0; i < 20; i++)
			{
				word.Attempts.Add(new WordAttempt
				{
					CreatedUtc = new DateTime(1999, 12, i + 1),
					Recognize = true
				});
			}
			word.UtcNow = new DateTime(2000, 1, 1);

			Assert.IsTrue(word.LearningDone);
		}
		
		[TestMethod]
		public void TestLearningDone_Has5GoodAttemptsLast3ConsecutiveDays_ReturnsTrue()
		{
			var word = new Word();
			word.Attempts = new List<WordAttempt>();
			word.IsLearning = true;
			for (int i = 0; i < 5; i++)
			{
				word.Attempts.Add(new WordAttempt
				{
					CreatedUtc = new DateTime(1999, 12, 29),
					Recognize = true
				});
			}
			for (int i = 0; i < 5; i++)
			{
				word.Attempts.Add(new WordAttempt
				{
					CreatedUtc = new DateTime(1999, 12, 30),
					Recognize = true
				});
			}
			for (int i = 0; i < 5; i++)
			{
				word.Attempts.Add(new WordAttempt
				{
					CreatedUtc = new DateTime(1999, 12, 31),
					Recognize = true
				});
			}
			word.UtcNow = new DateTime(2000, 1, 1);

			Assert.IsTrue(word.LearningDone);
		}

		[TestMethod]
		public void TestLearningDone_Has5GoodAttemptsLast3ConsecutiveDaysAnd1BadAttemptYesterday_ReturnsFalse()
		{
			var word = new Word();
			word.Attempts = new List<WordAttempt>();
			word.IsLearning = true;
			for (int i = 0; i < 5; i++)
			{
				word.Attempts.Add(new WordAttempt
				{
					CreatedUtc = new DateTime(1999, 12, 29),
					Recognize = true
				});
			}
			for (int i = 0; i < 5; i++)
			{
				word.Attempts.Add(new WordAttempt
				{
					CreatedUtc = new DateTime(1999, 12, 30),
					Recognize = true
				});
			}
			for (int i = 0; i < 5; i++)
			{
				word.Attempts.Add(new WordAttempt
				{
					CreatedUtc = new DateTime(1999, 12, 31),
					Recognize = true
				});
			}
			word.Attempts.Add(new WordAttempt
			{
				CreatedUtc = new DateTime(1999, 12, 31),
				Recognize = false
			});
			word.UtcNow = new DateTime(2000, 1, 1);

			Assert.IsFalse(word.LearningDone);
		}
	}
}
