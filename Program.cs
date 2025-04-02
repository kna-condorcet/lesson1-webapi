using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

List<Customer> customers = new List<Customer>();

app.MapGet("/customers", () =>
{
    return Results.Ok(customers);
});

app.MapPost("/customers", (Customer customer) =>
{
    customers.Add(customer);
    return Results.Ok();
});

app.MapPut("/customers/{id}", (int id, Customer customer) =>
{
    var existing = customers.FirstOrDefault(c => c.id == id);
    if (existing == null)
        return Results.BadRequest("id not found");
    customers.Remove(existing);
    customers.Add(customer);
    return Results.Ok();
});


app.MapDelete("/user/{id}", (int id) =>
{
    var existing = customers.FirstOrDefault(c => c.id == id);
    if (existing == null)
        return Results.BadRequest("id not found");
    customers.Remove(existing);
    return Results.Ok();
});

await app.RunAsync();

record Customer(int id, string name);
