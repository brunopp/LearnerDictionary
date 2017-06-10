using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnerDictionary.Repository
{
    public class ConnectionString
    {
        public string Value { get; }

        public ConnectionString(string connectionString)
        {
            Value = connectionString;
        }
    }
}