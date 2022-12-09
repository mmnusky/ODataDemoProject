using FluentAssertions.Common;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using ODataDemoProject.Brokers;
using ODataDemoProject.Brokers.Loggings;
using ODataDemoProject.Models;
using ODataDemoProject.Services.Foundations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddOData(options =>
    options.Select()
        .Filter()
        .OrderBy()
        .SetMaxTop(null)
        .Count());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StorageBroker>();

builder.Services.AddTransient<IStorageBroker, StorageBroker>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<ILoggingBroker, LoggingBroker>();

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

//static IEdmModel GetEdmModel()
//{
//    var builder = new ODataConventionModelBuilder();
//    builder.EntitySet<Customer>("Customer");
//    return builder.GetEdmModel();
//}
