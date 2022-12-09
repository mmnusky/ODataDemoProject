using ODataDemoProject.Models;
using ODataDemoProject.Models.Customers.Exceptions;
using System.Data.SqlClient;

namespace ODataDemoProject.Services.Foundations
{
    public partial class CustomerService
    {
        private delegate ValueTask<Customer> ReturningCustomerFunction();

        private delegate IQueryable<Customer> ReturningCustomersFunction();

        private async ValueTask<Customer> TryCatch(ReturningCustomerFunction returningCustomerFunction)
        {
            try
            {
                return await returningCustomerFunction();
            }
            catch (NullCustomerException nullCustomerException)
            {
                throw CreateAndLogValidationException(exception: nullCustomerException);
            }
            catch (NotFoundCustomerException notFoundTenantException)
            {
                throw CreateAndLogValidationException(exception: notFoundTenantException);
            }
            catch (InvalidCustomerException invalidCustomerException)
            {
                throw CreateAndLogValidationException(exception: invalidCustomerException);
            }
        }

        private IQueryable<Customer> TryCatch(ReturningCustomersFunction returningCustomerFunction)
        {
            try
            {
                return returningCustomerFunction();
            }
            catch (SqlException sqlException)
            {
                throw CreateAndLogCriticalDependencyException(sqlException);
            }
            catch (Exception exception)
            {
                var failedCustomerServiceException =
                    new FailedCustomerServiceException(exception);

                throw CreateAndLogServiceException(failedCustomerServiceException);
            }
        }

        private CustomerServiceException CreateAndLogServiceException(Exception exception)
        {
            var customerServiceException = new CustomerServiceException(exception);
            //this.loggingBroker.LogError(tenantServiceException);

            return customerServiceException;
        }

        private CustomerValidationException CreateAndLogValidationException(Exception exception)
        {
            var customerValidationException = new CustomerValidationException(exception);
            //this.loggingBroker.LogError(tenantValidationException);

            return customerValidationException;
        }

        private CustomerDependencyException CreateAndLogCriticalDependencyException(Exception exception)
        {
            var customerDependencyException = new CustomerDependencyException(exception);
            //this.loggingBroker.LogCritical(tenantDependencyException);

            return customerDependencyException;
        }
    }
}
