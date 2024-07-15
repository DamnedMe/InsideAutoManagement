using AutoMapper;
using InsideAutoManagement.Data;
using InsideAutoManagement.Data.Seed;
using InsideAutoManagement.Mapper;
using InsideAutoManagement.Model;
using Microsoft.EntityFrameworkCore;

/// <summary>   
/// Add services to the container.
/// </summary>
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<InsideAutoManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("InsideAutoManagementContext")
    ?? throw new InvalidOperationException("Connection string 'InsideAutoManagementContext' not found.")));

/// <summary>   
/// Configure AutoMapper
/// </summary>
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new InsideAutoMapperProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
//builder.Services.AddAutoMapper(typeof(Program));

/// <summary>   
/// Register services
/// </summary>
builder.Services.AddScoped<CarDealer>();
builder.Services.AddControllers();

/// <summary>  
/// Configure Swagger
/// </summary>
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

/// <summary>
/// Initialize and seed the database
/// </summary>
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<InsideAutoManagementContext>();
    Seeding.Initialize(context);
}

/// <summary>
/// Initialize and seed the database
/// </summary>
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
