using Npgsql;

namespace webapi.Customers;

public class DatabaseCustomerService: ICustomerService
{
    public async Task<List<Customer>> List()
    {
        var connectionString = "Host=localhost;Username=postgres;Password=example;Database=postgres";

        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT id, name FROM customers";
        var reader = await command.ExecuteReaderAsync();

        var result = new List<Customer>();
        
        while (await reader.ReadAsync())
        {
            result.Add(new Customer(reader.GetInt32(0), reader.GetString(1)));
        }

        return result;
    }

    public Task<Customer> GetById(int customerId)
    {
        throw new NotImplementedException();
    }

    public Task Add(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task Update(int id, Customer updatedCustomer)
    {
        throw new NotImplementedException();
    }

    public Task DeleteById(int id)
    {
        throw new NotImplementedException();
    }
}