using AutoMapper;
using Boilerplate.API.Configurations;
using Boilerplate.Data.Configurations;
using Boilerplate.Data.Context;
using Boilerplate.Data.Repositories;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Interfaces;
using Boilerplate.Service.Services;
using Newtonsoft.Json;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigureServices(builder.Services);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//using (var scope = app.Services.CreateScope())
//using (var context = scope.ServiceProvider.GetService<DatabaseContext>())
//    context?.Database.Migrate();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.Configure<DatabaseConfiguration>(
        builder.Configuration.GetSection("database:sqlite"));

    services.AddSingleton(new MapperConfiguration(config =>
    {
        config.AddProfile<UserMapProfile>();
    }).CreateMapper());

    services.AddDbContext<DatabaseContext>();

    builder.Services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
    builder.Services.AddScoped<IBaseService<User>, BaseService<User>>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IUserService, UserService>();

    services.AddMvc(option => option.EnableEndpointRouting = false)
        .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
}