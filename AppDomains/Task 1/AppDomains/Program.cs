using System;
using Common;

namespace AppDomains
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Host is running in AppDomain: {AppDomain.CurrentDomain.FriendlyName}");

            var billingAddIn = CreateBillingDomain();
            var instance = CreateBillingInstance(billingAddIn);

            var result = instance.GetAverageSalaryByPersonId(new PersonId(123));

            Console.WriteLine($"Salary is {result.Amount} EUR");

            Console.ReadKey();
        }

        private static AppDomain CreateBillingDomain()
        {
            var domainInfo = new AppDomainSetup
            {
                // The application base directory is where the assembly manager begins probing for assemblies.
                ApplicationBase = Environment.CurrentDirectory
            };

            // Defines the set of information that constitutes input to security policy decisions. 
            var evidence = AppDomain.CurrentDomain.Evidence;

            return AppDomain.CreateDomain("BillingDomain", evidence, domainInfo);
        }

        private static IBillingAddIn CreateBillingInstance(AppDomain billingDomain)
        {
            return (IBillingAddIn)billingDomain.CreateInstanceAndUnwrap("Billing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Billing.BillingAddIn");
        }
    }
}
