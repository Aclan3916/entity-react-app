using ContactManagersApi.Data;
using ContactManagersApi.interfaces;
using Microsoft.EntityFrameworkCore;
using ContactManagersApi.Services;
using Microsoft.AspNetCore.JsonPatch;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost5173", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Add services to the container.

//Register EF Core with SQLite
builder.Services.AddDbContext<PersonDb>(options => 
    options.UseSqlite(("Data Source = people.db")));

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IContactService, ContactService>();

var app = builder.Build();

app.UseCors("AllowLocalhost5173");                                            

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
