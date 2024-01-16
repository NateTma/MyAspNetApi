using MyAspNetApi.Models;
using MyAspNetApi.Services;
using MyAspNetApi.Data;

var builder = WebApplication.CreateBuilder(args);

//binds our settings as well as our mongodbservice
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.Configure<Dictionary<string, MongoDBSettings>>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<MongoDBService>();
builder.Services.AddSingleton<MongoDBWService>();

// Add services to the container.
builder.Services.AddControllers();

//Configure MongoDB context
//builder.Services.AddScoped<MongoDBContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
