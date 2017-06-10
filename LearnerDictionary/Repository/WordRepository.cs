using Dapper;
using LearnerDictionary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LearnerDictionary.Repository
{
    public class WordRepository
    {
        private readonly ConnectionString _connectionString;

        public WordRepository(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public void Post(Word word)
        {
            using (var cn = new SqlConnection(_connectionString.Value))
            {
                cn.Execute("INSERT INTO [Word] (Value, Definition, CreatedUtc, LanguageId) VALUES (@Value, @Definition, @CreatedUtc, @LanguageId)", word);
            }
        }
    }
}