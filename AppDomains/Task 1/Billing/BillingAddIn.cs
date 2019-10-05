using System;
using Common;

namespace Billing
{
    public class BillingAddIn : MarshalByRefObject, IBillingAddIn
    {
        public Salary GetAverageSalaryByPersonId(PersonId id)
        {
            Console.WriteLine($"{nameof(this.GetAverageSalaryByPersonId)} was called in AppDomain {AppDomain.CurrentDomain.FriendlyName}");

            return new Salary(id, 34.45);
        }
    }
}
