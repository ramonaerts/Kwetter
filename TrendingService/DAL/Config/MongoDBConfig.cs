﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrendingService.DAL.Config
{
    public class MongoDBConfig
    {
        public string Database { get; set; }
        public string Host { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(User) || string.IsNullOrEmpty(Password))
                    return $@"mongodb://{Host}";
                return $@"mongodb://{User}:{Password}@{Host}";
            }
        }
    }
}
