﻿using Poco.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocoSql.Test.Models.Mappings
{
    public class OrderMap : PocoSqlMapping<Order>
    {
        public OrderMap()
        {
            // Primary Key
            this.HasKey(t => t.OrderId);

            // Table & Column Mappings
            this.ToTable("Orders");

            this.HasOptional(t => t.User)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.UserId);
        }
    }
}
