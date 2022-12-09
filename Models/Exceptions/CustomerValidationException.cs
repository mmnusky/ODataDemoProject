namespace ODataDemoProject.Models.Customers.Exceptions
{
    public class CustomerValidationException : Exception
    {
        public CustomerValidationException(Exception innerException)
            : base(message: "Invalid tenant, please contact musharaf", innerException) { }
    }
}
