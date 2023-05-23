namespace Cartalog.Api.Settings;


public class MongoDbSettings
{
    public string Host { get; set; }
    public int Port { get; set; }

    public string User { get; set; }
    public string Password { get; set; }

    // Connection string // only a read property no set needed
    public string ConnectionString
    {
        get
        {   //User:Password were added later
            //when secret management was implemented to add a user-name and password for security
            return $"mongodb://{User}:{Password}@{Host}:{Port}";
        }
    }
}
