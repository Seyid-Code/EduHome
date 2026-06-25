using EduHome.Services.Implements;
using EduHome.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using website.Data;
using website.Services.Implements;
using website.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICourseFeatureService, CourseFeatureService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<ISliderService, SliderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
