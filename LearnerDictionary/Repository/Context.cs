using LearnerDictionary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LearnerDictionary.Repository
{
	public class Context : DbContext
	{
		public Context()
			: base("db")
		{
		}

		public DbSet<Word> Words { get; set; }
		public DbSet<WordAttempt> WordAttempts { get; set; }
		public DbSet<WordExample> WordExamples { get; set; }
		public DbSet<Language> Languages { get; set; }
	}
}