﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.Sql
{
    public interface IPocoSqlMapping
    {
        string GetTableName();
        string GetPrimaryKey();
        string GetQueryByName(string name);
        PocoSqlStoredProceduresMapping GetStoredProceduresMappings();
        PocoSqlCostomMapping GetCustomMappings();
        bool GetPrimaryAutoGenerated();
        bool GetIsVirtual();
        PropertyMap GetMapping(string propertyName);
        IRelationshipMap GetRelationship(string propertyName);
    }
}
