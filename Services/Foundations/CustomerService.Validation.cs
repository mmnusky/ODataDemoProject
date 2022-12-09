
using ODataDemoProject.Models;
using ODataDemoProject.Models.Customers.Exceptions;
using static System.Net.Mime.MediaTypeNames;

namespace ODataDemoProject.Services.Foundations
{
    public partial class CustomerService
    {
        private void ValidateCustomerOnCreate(Customer customer)
        {
            ValidateCustomer(customer);

            Validate(
                (Rule: IsValidX(customer.Id), Parameter: nameof(customer.Id)),
                (Rule: IsValidX(customer.Name), Parameter: nameof(customer.Name)),
                (Rule: IsValidX(customer.CreatedDate), Parameter: nameof(customer.CreatedDate)),
                (Rule: IsValidX(customer.City), Parameter: nameof(customer.City))
                //(Rule: IsNotSame(
                //    firstId: tenant.CreatedBy,
                //    secondId: tenant.UpdatedBy,
                //    secondIdName: nameof(tenant.UpdatedBy)),
                //  Parameter: nameof(tenant.CreatedBy)),
            );
        }

        private static dynamic IsValidX(Guid Id) => new
        {
            Condition = Id == Guid.Empty,
            Message = "Id is required"
        };
        private static dynamic IsValidX(int age) => new
        {
            Condition = String.IsNullOrEmpty(age.ToString()),
            Message = "age is required"
        };

        private static dynamic IsValidX(string text) => new
        {
            Condition = String.IsNullOrEmpty(text),
            Message = "text is required"
        };

        private static dynamic IsValidX(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static dynamic IsNotSame(
            Guid firstId,
            Guid secondId,
            string secondIdName) => new
            {
                Condition = firstId != secondId,
                Message = $"Id is not the same as {secondIdName}"
            };
        private static dynamic IsNotSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate != secondDate,
                Message = $"Date is not the same as {secondDateName}"
            };

        private static void ValidateCustomer(Customer tenant)
        {
            if (tenant is null)
            {
                throw new NullCustomerException();
            }
        }

        private void ValidateCustomerId(Guid inputId)
        {
            if (inputId == Guid.Empty)
            {
                throw new InvalidCustomerException(
                    parameterName: nameof(inputId),
                    parameterValue: inputId);
            }
        }

        private void ValidateStorageCustomer(Customer customer, Guid id)
        {
            if (customer is null)
            {
                throw new NotFoundCustomerException(id);
            }
        }

        private void ValidateCustomerOnModify(Customer customer)
        {
            ValidateCustomer(customer);

            Validate(
                (Rule: IsValidX(customer.Id), Parameter: nameof(customer.Id)),
                (Rule: IsValidX(customer.Name), Parameter: nameof(customer.Name)),
                (Rule: IsValidX(customer.CreatedDate), Parameter: nameof(customer.CreatedDate)),
                (Rule: IsValidX(customer.City), Parameter: nameof(customer.City))
            );
        }

       

        private void ValidateAgainstStorageCustomerOnModify(Customer inputCustomer, Customer storageCustomer)
        {
            Validate(
                (Rule: IsNotSame(
                    firstDate: inputCustomer.CreatedDate,
                    secondDate: storageCustomer.CreatedDate,
                    secondDateName: nameof(storageCustomer.CreatedDate)),
                  Parameter: nameof(storageCustomer.CreatedDate))
               
            );
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidCustomerException = new InvalidCustomerException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidCustomerException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidCustomerException.ThrowIfContainsErrors();
        }
    }
}
