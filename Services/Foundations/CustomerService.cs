

using ODataDemoProject.Brokers;
using ODataDemoProject.Brokers.Loggings;
using ODataDemoProject.Models;
using ODataDemoProject.Services.Foundations;

namespace ODataDemoProject.Services.Foundations
{
    public partial class CustomerService : ICustomerService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public CustomerService(
            IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;


        }

        public ValueTask<Customer> CreateCustomerAsync(Customer customer) =>
        TryCatch(async () =>
        {
            customer.Id = Guid.NewGuid();
            customer.CreatedDate = DateTime.UtcNow;
            ValidateCustomerOnCreate(customer);

            return await storageBroker.InsertCustomerAsync(customer);
        });

        public ValueTask<Customer> ModifyCustomerAsync(Customer customer) =>
        TryCatch(async () =>
        {
            ValidateCustomerOnModify(customer);

            Customer maybeCustomer =
                await this.storageBroker.SelectCustomerByIdAsync(customer.Id);

            ValidateStorageCustomer(maybeCustomer, customer.Id);
           // ValidateAgainstStorageCustomerOnModify(customer, maybeCustomer);

            return await this.storageBroker.UpdateCustomerAsync(customer);
        });
        public ValueTask<Customer> RemoveCustomerByIdAsync(Guid Id) =>
        TryCatch(async () =>
        {

            ValidateCustomerId(Id);
            Customer maybeCustomer = await storageBroker.SelectCustomerByIdAsync(Id);
            ValidateStorageCustomer(maybeCustomer, Id);

            return await this.storageBroker.DeleteCustomerAsync(maybeCustomer);
        });

        public ValueTask<Customer> RetreiveCustomerByIdAsync(Guid Id) =>
       TryCatch(async () =>
       {
           ValidateCustomerId(Id);

           Customer maybeCustomer =
               await this.storageBroker.SelectCustomerByIdAsync(Id);
           ValidateStorageCustomer(maybeCustomer, Id);

           return maybeCustomer;
       });

        public IQueryable<Customer> RetrieveAllCustomer() =>
        TryCatch(() => this.storageBroker.SelectAllCustomer());
    }
}

