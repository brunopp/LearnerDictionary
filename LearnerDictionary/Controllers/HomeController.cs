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

			var words = _context.Words.Include("Attempts").Include("Examples").ToList();

			var wordsByAttemptsAndScore = words.OrderByDescending(x => x.IsLearning).ThenByDescending(x => x.RelevantToday).ThenBy(x => x.Score);
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

			var latestFiveWordIdAttempts = new HashSet<int>(_context.WordAttempts.OrderByDescending(x => x.CreatedUtc).Take(5).Select(x => x.WordId));

			var take = wordsInProcess.Count() - latestFiveWordIdAttempts.Count;
			var skip = rng.Next(take);

			var word = wordsInProcess.Where(x => !latestFiveWordIdAttempts.Contains(x.Id)).Skip(skip).FirstOrDefault();

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