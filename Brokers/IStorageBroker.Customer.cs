using ODataDemoProject.Models;

namespace ODataDemoProject.Brokers
{
    public partial interface IStorageBroker
    {
        ValueTask<Customer> InsertCustomerAsync(Customer custmer);
        IQueryable<Customer> SelectAllCustomer();
        ValueTask<Customer> SelectCustomerByIdAsync(Guid id);
        ValueTask<Customer> UpdateCustomerAsync(Customer custmer);
        ValueTask<Customer> DeleteCustomerAsync(Customer custmer);
    }
}
