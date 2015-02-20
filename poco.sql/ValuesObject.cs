﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Poco.Sql
{
    public class ValuesObject
    {
        public static object Create(params object[] args)
        {
            dynamic obj = new ExpandoObject();


            List<string> sqlParams = getPramsFromSqlString(args);
            int startPos = sqlParams == null ? 0 : 1; // if we have sqlParams that means that the first agrument is an sql string

            // loop over all objects that were sent for merging
            for (int i = startPos; i < args.Length; i++)
            {
                // get an object and make sure it's valid
                object currentObj = args[i];
                if (currentObj == null) continue;

                IDictionary<String, Object> objDic = (IDictionary<String, Object>)obj;

                // loop over all elements of current object
                PropertyInfo[] propertyInfos = currentObj.GetType().GetProperties();
                foreach (PropertyInfo propertyInfo in propertyInfos.Where(p => p.PropertyType.FullName.StartsWith("System") && !p.PropertyType.FullName.StartsWith("System.Collections"))) // only loop on objects that are not custom class
                {
                    if (objDic.ContainsKey(propertyInfo.Name)) continue; // same key can't be added twice (first key found will be used)

                    var val = propertyInfo.GetValue(currentObj);
                    if (propertyInfo.PropertyType == typeof(DateTime) && (DateTime)val == DateTime.MinValue)
                        val = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;

                    objDic.Add(propertyInfo.Name, val);
                }
            }

            return (object)obj;
        }

        private static object getDefaultValue(Type t)
        {
            if (t == typeof(DateTime))
                return DateTime.MinValue;
            else if (t.IsValueType)
                return Activator.CreateInstance(t);

            return null;
        }

        /// <summary>
        /// Gets the prams from SQL string, if the first object in the arguments that were sent to the creator is a string (sql statement)
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>List<string></returns>
        private static List<string> getPramsFromSqlString(object[] args)
        {
            List<string> sqlParams = null;
            if (args.Length > 1 && args[0] is String)
            {
                sqlParams = new List<string>();

                string sql = args[0].ToString();
                Regex regex = new Regex(@"(?<=@)([\w\-]+)");
                var matches = regex.Matches(sql);

                for (int i = 0; i < matches.Count; i++)
                {
                    var val = matches[i].Value;
                    if (!sqlParams.Contains(val))
                        sqlParams.Add(matches[i].Value);
                }
            }
            return sqlParams;
        }
    }
}