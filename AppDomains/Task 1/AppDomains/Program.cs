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
            var print = CreatePrintDomain();
            var printInstance = CreatePrintInstance(print);

            var result = instance.GetAverageSalaryByPersonId(new PersonId(123));
            var printMessage = printInstance.FormatSalary(result);
            PrintMessage(printMessage);

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

        private static AppDomain CreatePrintDomain()
        {
            var domainInfo = new AppDomainSetup
            {
                // The application base directory is where the assembly manager begins probing for assemblies.
                ApplicationBase = Environment.CurrentDirectory
            };

            // Defines the set of information that constitutes input to security policy decisions. 
            var evidence = AppDomain.CurrentDomain.Evidence;

            return AppDomain.CreateDomain("PrintDomain", evidence, domainInfo);
        }

        private static IBillingAddIn CreateBillingInstance(AppDomain billingDomain)
        {
            return (IBillingAddIn)billingDomain.CreateInstanceAndUnwrap("Billing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Billing.BillingAddIn");
        }

        private static IPrintAddIn CreatePrintInstance(AppDomain printDomain)
        {
            return (IPrintAddIn) printDomain.CreateInstanceAndUnwrap(
                "Print, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Print.PrintAddIn");
        }

        private static void PrintMessage(string message)
        {
            Console.Write(message);
        }
    }
}
