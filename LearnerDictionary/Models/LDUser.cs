using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnerDictionary.Models
{
    public class LDUser : IUser
    {
        public string Id => "1";

        public string UserName { get; set; }
    }
}