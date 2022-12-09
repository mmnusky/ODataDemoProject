namespace ODataDemoProject.Models.Customers.Exceptions
{
    public class CustomerDependencyException : Exception
    {
        public CustomerDependencyException(Exception exception)
            : base("Service dependency error occurred, contact support.", exception)
        {

        }
    }
}
