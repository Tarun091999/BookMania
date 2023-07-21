using AutoMapper;
using BookManagement.BLL.Services;
using BookManagement.DAL.Context;
using BookManagement.DAL.Repositories;
using BookManagement.Entity.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//var provider = builder.Services.BuildServiceProvider();
//var configuration = provider.GetRequiredService<IConfiguration>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BookManagementDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Add Repositories Here 
builder.Services.AddScoped (typeof(IBookManagementGenericRepo<>), typeof(BookManagementGenericRepo<>));
builder.Services.AddScoped<IBookRepo, BookRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();

//Add Services Here 
builder.Services.AddTransient<IBookServices, BookServices>();
builder.Services.AddTransient<IUserServices, UserServices>();
builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<IBookAllocationServices, BookAllocationServices>();
builder.Services.AddTransient<IReturnBookServices, ReturnBookServices>();


//builder.Services.AddSingleton(System.Configuration);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
