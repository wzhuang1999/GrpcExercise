using System;

namespace Common
{
    [Serializable]
    public class Salary
    {
        public enum PeriodType
        {
            Monthly,
            Yearly
        }

        public PersonId PersonId { get; }
        public double Amount { get; }
        public PeriodType Period { get; }

        public Salary(PersonId personId, double amount)
        {
            this.PersonId = personId;
            this.Amount = amount;
            this.Period = PeriodType.Monthly;
        }
    }
}