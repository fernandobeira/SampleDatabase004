using Microsoft.AspNetCore.Mvc;
using SampleDatabase004;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Db2netr004Context>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/addresses/{id}", async (int id, [FromServices] Db2netr004Context context) =>
{
    var address = await context.Addresses.FindAsync(id);

    if (address == null)
        return Results.NotFound(new { Message = "Endereço não encontrado" });

    return Results.Ok(address);
})
.WithName("GetAddressById")
.Produces<Address>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound);

app.Run();