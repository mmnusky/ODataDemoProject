namespace ODataDemoProject.Models.Customers.Exceptions
{
    public class NullCustomerException : Exception
    {
        public NullCustomerException() : base(message: "The tenant is null.") { }
    }
}
