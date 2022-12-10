using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;

using Detector.Api;
using Detector.Api.Authorization;
using Detector.Api.Authentication;
using Detector.Api.Users;

var builder = WebApplication.CreateBuilder(args);

// Configure auth
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorizationBuilder().AddCurrentUserHandler();

// Add the service to generate JWT tokens
builder.Services.AddTokenService();

// Configure the database
//var connectionString = builder.Configuration.GetConnectionString("Todos") ?? "Data Source=Todos.db";
//builder.Services.AddSqlite<TodoDbContext>(connectionString);

// Configure identity
//builder.Services.AddIdentityCore<TodoUser>()
//                .AddEntityFrameworkStores<TodoDbContext>();

// State that represents the current user from the database *and* the request
builder.Services.AddCurrentUser();

// Configure Open API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<SwaggerGeneratorOptions>(o => o.InferSecuritySchemes = true);



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Map("/", () => Results.Redirect("/swagger"));

// Configure the APIs
app.MapDetector();
app.MapUsers();


app.Run();
