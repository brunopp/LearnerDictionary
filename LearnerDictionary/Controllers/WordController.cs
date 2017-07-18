using LearnerDictionary.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearnerDictionary.Models;
using LearnerDictionary.Models.ViewModels;

namespace LearnerDictionary.Controllers
{
    public class WordController : Controller
	{
		private readonly Context _context;

		public WordController(Context context)
		{
			_context = context;
		}

		// GET: Word
		public ActionResult Index()
        {
            var wordList = new WordListViewModel();

			var words = _context.Words.Include("Language").Include("Attempts");

			var l = new List<WordViewModel>();
			foreach (var word in words)
			{
				l.Add(new WordViewModel
				{
					Id = word.Id,
					Definition = word.Definition,
					Language = word.Language.Value,
					Value = word.Value,
					Status = word.Status,
                    Score = word.Score,
                    HasDefintion = word.HasDefintion,
					Deleted = word.Deleted
				});
			}

            var wordsOrdered = l.OrderBy(x => x.Value);

            wordList.WordsWithoutDefinition = wordsOrdered.Where(x => !x.HasDefintion);
            wordList.WordsWithDefintion = wordsOrdered.Where(x => x.HasDefintion);

            return View(wordList);
        }

        // GET: Word/Details/5
        public ActionResult Details(int id)
        {
			var word = _context.Words.FirstOrDefault(x => x.Id == id);

			var model = new WordFormViewModel
			{
				Id = word.Id,
				Definition = word.Definition,
				Language = word.Language.Value,
				Value = word.Value
			};
			foreach (var lang in model.Languages)
			{
				lang.Selected = word.Language.Value == lang.Value;
			}
			return View(model);
        }

        // GET: Word/Create
        public ActionResult Create()
        {
            return View(new WordFormViewModel());
        }

        // POST: Word/Create
        [HttpPost]
        public ActionResult Create(WordFormViewModel model)
        {
            try
            {
				_context.Words.Add(new Word
				{
					Value = model.Value,
					Definition = model.Definition,
					Language = _context.Languages.First(x => x.Value == model.Language),
                    Examples = model.Examples?.ToList()
				});
				_context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("IX_Value"))
                {
                    ModelState.AddModelError("Value", ex);
                }

                return View(model);
            }
		}

		// GET: Word/Edit/5
		public ActionResult Edit(int id)
		{
			var word = _context.Words.FirstOrDefault(x => x.Id == id);

			var model = new WordFormViewModel
			{
				Id = word.Id,
				Definition = word.Definition,
				Language = word.Language.Value,
				Value = word.Value,
                Examples = word.Examples
			};
			foreach (var lang in model.Languages)
			{
				lang.Selected = word.Language.Value == lang.Value;
			}
			return View(model);
		}

        // POST: Word/Edit/5
        [HttpPost]
        public ActionResult Edit(WordFormViewModel model)
        {
            try
			{
				var word = _context.Words.FirstOrDefault(x => x.Id == model.Id);
				word.Value = model.Value;
				word.Definition = model.Definition;
				word.Language = _context.Languages.First(x => x.Value == model.Language);
                while (word.Examples.Any())
                {
                    _context.WordExamples.Remove(word.Examples.First());
                }
                word.Examples = model.Examples?.ToList();
				_context.SaveChanges();

				return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("IX_Value"))
                {
                    ModelState.AddModelError("Value", ex);
                }

                return View(model);
            }
        }

        // GET: Word/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Word/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
