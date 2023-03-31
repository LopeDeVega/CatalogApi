﻿namespace Catalog.Settings;


public class MongoDbSettings
{
    public string Host { get; set; }
    public int Port { get; set; }

    // Connection string // only a read property no set needed
    public string ConnectionString
    {
        get
        {
            return $"mongodb://{Host}:{Port}";
        }
    }
}
