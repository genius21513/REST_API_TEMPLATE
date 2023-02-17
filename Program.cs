using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using REST_API_TEMPLATE.Data;
using REST_API_TEMPLATE.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { 
        Title = "Album Image API", 
        Version = "v1",
        Description = "An API to perform Album and Image operations",
    });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// Register Library Service to use it with Dependency Injection in Controllers
builder.Services.AddTransient<ILibraryService, LibraryService>();

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


// add environmet variables
//builder.Configuration.AddEnvironmentVariables();

//var connectionString = builder.Configuration["ConnectionString"];
//Console.WriteLine(connectionString);

// Register database
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase")));

//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseNpgsql(builder.Configuration.GetConnectionString("PgDatabase")));

var app = builder.Build();

//Console.WriteLine("Running migration:------------------");

//using (var context = (AppDbContext)app.Services.GetService(typeof(AppDbContext)))
//{
//    context.Database.Migrate();
//}

using (var scope = app.Services.CreateScope())
{
    Console.WriteLine("Running migration:------------------");
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    Console.WriteLine(context.Database.GetConnectionString());
    context.Database.EnsureCreated();
    context.Database.Migrate();
    //scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//app.UseHsts();

//app.Use((context, next) =>
//{
//    context.Request.Scheme = "https";
//    context.Request.Host = new HostString(context.Request.Host.Host, 443);
//    return next();
//});

app.UseAuthorization();

app.MapControllers();

app.Run();
