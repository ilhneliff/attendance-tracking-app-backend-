using Microsoft.EntityFrameworkCore;
using NonattendanceApp.AppDb;
using NonattendanceApp.Repositories;
using NonattendanceApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(ITokenService), typeof(TokenService));
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Allow",
        policy =>
        {
            policy.AllowAnyOrigin()      // Geliştirme için -> tüm originlere izin
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Allow");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();