using Database;
using Microsoft.EntityFrameworkCore;
using PrintersManager.Exceptions;
using PrintersManager.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PrintersDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddExceptionHandler<PrintersManagerExceptionHandler>();

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPrintersService, PrintersService>();
builder.Services.AddScoped<IEmployeesService, EmployeesService>();
builder.Services.AddScoped<IBranchesService, BranchesService>();
builder.Services.AddScoped<IInstallationsService, InstallationsService>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(); 

app.UseHttpsRedirection();

app.MapControllerRoute(
    name: "api",
    pattern: "api/{controller}/{action}");

app.Run();