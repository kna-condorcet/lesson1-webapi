namespace webapi.Customers;

public static class CustomersEndpoints
{
    public static void MapCustomerEndpoints(this WebApplication app)
    {
        app.MapGet("/customers", async (CustomerService service) =>
        {
            var customers = await service.List();
            return Results.Ok(customers);
        });

        app.MapPost("/customers",
            async (Customer request, CustomerService service) =>
            {
                var newCustomer = new Customer(request.Id, request.Name);
                await service.Add(newCustomer);
                return Results.Created($"/customers/{newCustomer.Id}", newCustomer);
            });

        app.MapPut("/customers/{id}", async (int id, Customer request, CustomerService data) =>
        {
            var existingCustomer = await data.GetById(id);
            if (existingCustomer is null)
            {
                return Results.NotFound();
            }

            var updatedCustomer = existingCustomer with
            {
                Name = request.Name
            };
            await data.Update(id, updatedCustomer);
            return Results.Ok(updatedCustomer);
        });


        app.MapDelete("/customers/{id}", async (int id, CustomerService data) =>
        {
            var customer = await data.GetById(id);
            if (customer is null)
            {
                return Results.NotFound();
            }

            await data.DeleteById(id);

            return Results.NoContent();
        });
    }
}