using Xeptions;

namespace ODataDemoProject.Models.Customers.Exceptions
{
    public class AlreadyExistsCustomerException : Xeption
    {
        public AlreadyExistsCustomerException(Exception innerException)
                    : base(message: "Tenant with the same id already exists.", innerException) { }
    }
}
