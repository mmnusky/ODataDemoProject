using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using ODataDemoProject.Models;

namespace ODataDemoProject.Brokers
{
    public partial class StorageBroker : IStorageBroker
    {
        public DbSet<Customer> Customers { get; set; }
        public async ValueTask<Customer> DeleteCustomerAsync(Customer custmer)
        {
            using var broker = new StorageBroker(configuration);
            EntityEntry<Customer> customerEntityEntry = broker.Customers.Remove(entity: custmer);

            await broker.SaveChangesAsync();

            return customerEntityEntry.Entity;
        }

        public async ValueTask<Customer> InsertCustomerAsync(Customer custmer)
        {
            using var broker = new StorageBroker(configuration);
            EntityEntry<Customer> customerEntityEntry = await broker.Customers.AddAsync(custmer);
            await broker.SaveChangesAsync();

            return customerEntityEntry.Entity;
        }

        public IQueryable<Customer> SelectAllCustomer() => this.Customers.AsQueryable();
        public async ValueTask<Customer> SelectCustomerByIdAsync(Guid id)
        {
            using var broker = new StorageBroker(configuration);
            broker.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return await broker.Customers.FindAsync(id);
        }

        public async ValueTask<Customer> UpdateCustomerAsync(Customer custmer)
        {
            using var broker = new StorageBroker(configuration);
            EntityEntry<Customer> customerEntityEntry = broker.Customers.Update(entity: custmer);

            await broker.SaveChangesAsync();

            return customerEntityEntry.Entity;
        }
    }
}
