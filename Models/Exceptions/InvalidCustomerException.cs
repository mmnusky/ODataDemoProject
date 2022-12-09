using Xeptions;

namespace ODataDemoProject.Models.Customers.Exceptions
    {

    public class InvalidCustomerException : Xeption
    {
        public InvalidCustomerException(string parameterName, Guid parameterValue)
            : base(message: $"Invalid tenant, " +
                  $"paremeter name: {parameterName}" +
                  $"parameter value: {parameterValue}")
        { }
        public InvalidCustomerException() : base(message: "Invalid tenant. please fix the error and try again") { }
    }
}
