using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using InsideAutoManagement.Data;
using InsideAutoManagement.Data.Seed;
using Microsoft.AspNetCore.Hosting;
using AutoMapper;
using InsideAutoManagement.Mapper;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<InsideAutoManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("InsideAutoManagementContext") ?? throw new InvalidOperationException("Connection string 'InsideAutoManagementContext' not found.")));

// Add services to the container.
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new InsideAutoMapperProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
//builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
