using Application.Common;
using Infrastructure.Database;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IItemRepository, ItemRepository>();
builder.Services.AddDbContext<ItemsDatabase>((options) => options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));
builder.Services.AddMediatR(assembly =>
{
    assembly.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly);
});
builder.Services.AddControllers()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
});

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();