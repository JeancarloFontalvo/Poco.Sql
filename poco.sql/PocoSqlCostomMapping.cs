﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poco.Sql
{
    public class PocoSqlCostomMapping
    {
        public string SelectQuery { get; private set; }
        public string InsertQuery { get; private set; }
        public string UpdateQuery { get; private set; }
        public string DeleteQuery { get; private set; }

        public void Select(string sqlStatement)
        {
            SelectQuery = sqlStatement;
        }

        public void Insert(string sqlStatement)
        {
            InsertQuery = sqlStatement;
        }

        public void Update(string sqlStatement)
        {
            UpdateQuery = sqlStatement;
        }

        public void Delete(string sqlStatement)
        {
            DeleteQuery = sqlStatement;
        }
    }
}
