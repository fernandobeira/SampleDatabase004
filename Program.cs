using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using SampleDatabase004;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JsonOptions>(options => {
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var connection = string.Empty;
connection = builder.Configuration.GetConnectionString("SampleDatabaseSecret");

builder.Services.AddDbContext<Db2netr004Context>(option => option.UseSqlServer(connection));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("customers/{id}", (Db2netr004Context context, int id) => {
    Customer customer;
    customer = context.Customers
        .Where(customer => customer.CustomerId == id)
        .Include(customer => customer.CustomerAddresses)
        .Single();

    return customer;

})
.WithName("Customers")
.WithOpenApi();

app.Run();

