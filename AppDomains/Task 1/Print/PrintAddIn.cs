using System;
using Common;

namespace Print
{
    public class PrintAddIn : MarshalByRefObject, IPrintAddIn
    {
        public string FormatSalary(Salary salary)
        {
            var message = $"Salary is {salary.Amount} EUR";
            return message;
        }
    }
}