namespace webapi.Customers;

public interface ICustomerService
{
    public Task<List<Customer>> List();

    public Task<Customer> GetById(int customerId);

    public Task Add(Customer customer);

    public Task Update(int id, Customer updatedCustomer);

    public Task DeleteById(int id);
}