namespace ODataDemoProject.Models.Customers.Exceptions
{
    public class NotFoundCustomerException : Exception
    {
        public NotFoundCustomerException(Guid tenantId)
            : base(message: $"Couldn't find tenant with id {tenantId}.") { }
    }
}
