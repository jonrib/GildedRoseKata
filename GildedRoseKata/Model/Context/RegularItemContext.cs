﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GildedRoseKata.Model.Context
{
    public class RegularItemContext : DbContext
    {
        public RegularItemContext(DbContextOptions<RegularItemContext> options)
            : base(options)
        {
        }

        public DbSet<RegularItem> RegularItems { get; set; }
    }
}
