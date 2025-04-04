namespace webapi.Customers;

public class InMemoryCustomerService: ICustomerService
{
    private static readonly List<Customer> _customers = new();
    
    public InMemoryCustomerService()
    {
        _customers.AddRange(new []
        {
            new Customer(1, "Acme"),
            new Customer(2, "Contoso"),
            new Customer(3, "WebstanZ"),
        });
    }
    
    public Task<List<Customer>> List()
    {
        return Task.FromResult(_customers);
    }
    
    public Task<Customer> GetById(int customerId)
    {
        var customer = _customers.FirstOrDefault(c => c.Id == customerId);
        return Task.FromResult(customer);
    }
    
    public Task Add(Customer customer)
    {
        _customers.Add(customer);
        return Task.CompletedTask;
    }

    public Task Update(int id, Customer updatedCustomer)
    {
        if (_customers.Any(c => c.Id == id))
        {
            var index = _customers.FindIndex(c => c.Id == id);
            _customers[index] = updatedCustomer;
        }
        return Task.CompletedTask;
    }

    public Task DeleteById(int id)
    {
        if (_customers.Any(c => c.Id == id))
        {
            var index = _customers.FindIndex(c => c.Id == id);
            _customers.RemoveAt(index);
        }
        return Task.CompletedTask;
    }
}

public record Customer(int Id, string Name);