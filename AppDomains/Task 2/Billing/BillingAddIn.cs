using System;
using Common;

namespace Billing
{
    public class BillingAddIn : MarshalByRefObject, IBillingAddIn
    {
        private int _counter;

        public Salary GetAverageSalaryByPersonId(PersonId id)
        {
            Console.WriteLine($"{nameof(this.GetAverageSalaryByPersonId)} was called in AppDomain {AppDomain.CurrentDomain.FriendlyName}");

            // Crazy bug ...
            if (_counter++ == 2)
            {
                throw new ApplicationException("Damn bug");
            }

            return new Salary(id, 34.45);
        }
    }
}
