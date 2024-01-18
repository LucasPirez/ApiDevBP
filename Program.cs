using ApiDevBP.Configurations;
using ApiDevBP.Services;
using Microsoft.OpenApi.Models;
using Serilog;
using SQLite;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Host.UseSerilog();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ToDo API",
    });
    var xmlFilename = $"./Documentation.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddSingleton<ILocalDatabase, LocalDatabase>();
builder.Services.AddSingleton(provider => new SQLiteConnection(provider.GetRequiredService<ILocalDatabase>().GetLocalDbPath()));

builder.Services.Configure<ConnectionStrings>(
    builder.Configuration.GetSection(
        key: nameof(ConnectionStrings)));

builder.Services.AddScoped<IUserServices, UserServices>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
