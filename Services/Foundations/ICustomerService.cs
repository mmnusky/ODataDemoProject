using ODataDemoProject.Models;

namespace ODataDemoProject.Services.Foundations
{
    public partial interface ICustomerService
    {
        ValueTask<Customer> CreateCustomerAsync(Customer tenant);
        ValueTask<Customer> RetreiveCustomerByIdAsync(Guid Id);
        IQueryable<Customer> RetrieveAllCustomer();
        ValueTask<Customer> ModifyCustomerAsync(Customer tenant);
        ValueTask<Customer> RemoveCustomerByIdAsync(Guid Id);
    }
}
