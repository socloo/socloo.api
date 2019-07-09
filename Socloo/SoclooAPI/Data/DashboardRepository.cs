﻿using MongoDB.Bson;
using SoclooAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoclooAPI.Data
{
    public class DashboardRepository<T> : Repository<T> where T : ILocalizableEntity<ObjectId>

    {
        public DashboardRepository(DataContext context) : base(context)
        {
        }
    }
}