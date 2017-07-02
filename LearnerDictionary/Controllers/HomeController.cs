using LearnerDictionary.Models;
using LearnerDictionary.Models.ViewModels;
using LearnerDictionary.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnerDictionary.Controllers
{
	public class HomeController : Controller
	{
		private readonly Context _context;
		private readonly Random _random;

		public HomeController(Context context, Random random)
		{
			_context = context;
			_random = random;
		}

		public ActionResult Index()
		{
            var seed = _context.Words.Sum(x => x.Attempts.Count());
			var rng = new Random(seed);
			var now = DateTime.UtcNow;

			var words = _context.Words.Include("Attempts").Include("Examples").ToList().Where(x => x.HasDefintion);

			var wordsByAttemptsAndScore = words.OrderByDescending(x => x.IsLearning).ThenByDescending(x => x.HasAttempts).ThenBy(x => x.Score);
			var wordsInProcess = wordsByAttemptsAndScore.Take(20);

			var updateWords = wordsInProcess.Where(x => !x.IsLearning);
			if (updateWords.Any())
			{
				foreach (var updateWord in updateWords)
				{
					updateWord.IsLearning = true;
				}
				_context.SaveChanges();
			}

			var relevantWords = new List<Word>();

            var word = wordsInProcess.OrderBy(x => x.Score * rng.NextDouble() + rng.NextDouble()).FirstOrDefault();

			return View(new WordFormViewModel
			{
				Id = word.Id,
				Definition = word.Definition,
				Language = word.Language.Value,
				Value = word.Value,
				Examples = word.Examples
			});
		}
	}
}