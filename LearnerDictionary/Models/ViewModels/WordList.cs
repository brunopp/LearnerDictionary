using LearnerDictionary.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnerDictionary.Models.ViewModels
{
    public class WordListViewModel
    {
        public IEnumerable<WordViewModel> WordsWithoutDefinition { get; set; }
        public IEnumerable<WordViewModel> WordsWithDefintion { get; set; }
    }
}