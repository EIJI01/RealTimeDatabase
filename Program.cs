using Microsoft.EntityFrameworkCore;
using RealTimeDatabase.Database;
using RealTimeDatabase.Hubs;
using RealTimeDatabase.MiddlewareExtensions;
using RealTimeDatabase.SubscribeTableDependencies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnections")));

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddSingleton<Dashboard>();
builder.Services.AddSingleton<SubscribeProductTableDependency>();
builder.Services.AddSingleton<SubscribeSaleTableDependency>();

builder.Services.AddCors(o => o.AddPolicy(
        "MyPolicy", builder =>
        {
            builder.WithOrigins("http://localhost:5000")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .SetIsOriginAllowed((host) => true)
                   .AllowCredentials();
        }));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
var connectionString = app.Configuration.GetConnectionString("DefaultConnections");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyPolicy");
app.UseHttpsRedirection();

app.MapHub<Dashboard>("/dashboardHub");
app.MapControllers();

app.UseSqlTableDependency<SubscribeProductTableDependency>(connectionString);
app.UseSqlTableDependency<SubscribeSaleTableDependency>(connectionString);

app.Run();
