using AutoMapper;
using LearnerDictionary.Models.ViewModels;
using LearnerDictionary.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LearnerDictionary.Controllers
{
    public class WordApiController : ApiController
	{
		private readonly Context _context;

        public WordApiController(Context context)
		{
			_context = context;
        }

        public void PostWord(WordFormViewModel model)
        {

        }

		public void PostAttempt(WordAttemptApiViewModel model)
		{
			var word = _context.Words.FirstOrDefault(x => x.Id == model.WordId);
            word.Value = model.WordValue;
			word.Definition = model.Defintion;
			word.Attempts.Add(new Models.WordAttempt
			{
				Word = word,
				Recognize = model.Recognize
			});

			if (word.LearningDone)
			{
				word.IsLearning = false;
			}

			_context.SaveChanges();
		}

		public int PostExample(WordExampleApiViewModel model)
		{
			var word = _context.Words.FirstOrDefault(x => x.Id == model.WordId);
			var example = word.Examples.FirstOrDefault(x => x.Id == model.Id);
			if (example == null)
			{
				example = new Models.WordExample
				{
					WordId = model.WordId
				};
				word.Examples.Add(example);
			}
			example.Text = model.Text;

			_context.SaveChanges();

			return example.Id;
		}
		
		public void DeleteExample(int id)
		{
			var example = _context.WordExamples.FirstOrDefault(x => x.Id == id);
			if (example != null)
			{
				_context.WordExamples.Remove(example);
				_context.SaveChanges();
			}
		}

		public void Delete(int id)
		{
			var word = _context.Words.FirstOrDefault(x => x.Id == id);
			word.Deleted = true;
			_context.SaveChanges();
		}
    }
}
