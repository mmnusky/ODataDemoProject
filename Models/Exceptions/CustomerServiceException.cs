namespace ODataDemoProject.Models.Customers.Exceptions
{
    public class CustomerServiceException : Exception
    {
        public CustomerServiceException(Exception innerException)
            : base(message: "Service error occurred, contact support.", innerException)
        {

        }
    }
}
