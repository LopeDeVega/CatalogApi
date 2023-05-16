using Catalog.Repositories;
using Catalog.Settings;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// How mongo db treat Guid and DateTimeOffset fields
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

// Implementing the MongoClient
// Get the MongoDBSetting from appsetting.json via MongDbSettings class (setting folder)
var mongDbSettings = builder.Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();

// Building the connection string
builder.Services.AddSingleton<IMongoClient>(ServiceProvider =>
{
    // MongoClient instance
    return new MongoClient(mongDbSettings.ConnectionString);
});
// Using Mongo repository
builder.Services.AddSingleton<IItemsRepository, MongoDbItemsRepository>();

builder.Services.AddControllers(
    //it avoid removing the sufix from method names so all the controller will work correctly.
    options => options.SuppressAsyncSuffixInActionNames = false
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adding Health services to check
builder.Services.AddHealthChecks()
    // name to check the db name and also fater 4 second with not connection crash 
    .AddMongoDb(
        mongDbSettings.ConnectionString,
        name: "mongodb",
        timeout: TimeSpan.FromSeconds(5),
        //An array to get all the healthCheck for ready endpoint (it's db up and running and ready to recieve requests)
        tags: new[] {"ready"});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//It will avoid to re-direct from http to htpps
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();

}

app.UseAuthorization();

app.MapControllers();

//make sure that Db can send request
app.MapHealthChecks("/health/ready",
    new HealthCheckOptions
    {
        Predicate = (check) => check.Tags.Contains("ready"),
        ResponseWriter = async (context, report) =>
        {
            var result = JsonSerializer.Serialize(
                new
                {
                    status = report.Status.ToString(),
                    checks = report.Entries.Select(entry => new
                    {
                        name = entry.Key,
                        status = entry.Value.Status.ToString(),
                        //to catch the exception if not null all ok or there is an exception -> show none
                        Exception = entry.Value.Exception != null ? entry.Value.Exception.Message : "none",
                        duration = entry.Value.Duration.ToString()
                    })
                }
            );

            //Format the response
            context.Response.ContentType = MediaTypeNames.Application.Json;
            //Write the response
            await context.Response.WriteAsync( result );
        }
    });

//be sure that our services API is up and running
app.MapHealthChecks("/health/live",
    new HealthCheckOptions
    {
        Predicate = (_) => false
    }); ;


app.Run();
