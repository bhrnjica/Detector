
using Detector.Contract.Interfaces;
using Detector.Contract.Models;

using Detector.WebApi;
using Detector.WebApi.Data;
using Detector.WebApi.Extensions;
using Detector.WebApi.Repositories;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);


// Configure auth
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorizationBuilder().AddCurrentUserHandler();

// Add the service to generate JWT tokens
builder.Services.AddTokenService();


// Configure the database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSqlite<DetectorContext>(connectionString);

// Configure identity
builder.Services.AddIdentityCore<DetectorUser>()
                .AddEntityFrameworkStores<DetectorContext>();

// State that represents the current user from the database *and* the request
builder.Services.AddCurrentUser();

// Configure Open API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<SwaggerGeneratorOptions>(o => o.InferSecuritySchemes = true);

//Add Detector repositories
builder.Services.AddScoped<IDetectorRepository, DetectorRepository>();


//Add mediator middware
builder.Services.AddMediatR(x=> x.AsScoped(), typeof(Program));

// Configure OpenTelemetry
//builder.AddOpenTelemetry();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//Map default page to open swagger
app.Map("/", () => Results.Redirect("/swagger"));


//Provide Identity authentication and
//authorization in the WebAPi
app.MapUsers();

//Provide Detector serivce to the WebAapi
app.MapDetector();

//run Detector WebApi
app.Run();