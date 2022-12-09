namespace ODataDemoProject.Models.Customers.Exceptions
{ 
    public class FailedCustomerServiceException : Exception
    {
        public FailedCustomerServiceException(Exception exception)
            : base("Failed tenant service error occurred, contact support.", exception)
        {

        }
    }
}
