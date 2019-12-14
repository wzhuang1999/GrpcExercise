using System;
using Testing.Interfaces;

namespace Testing
{
    internal class ClassA : IClassA
    {
        private readonly IClassB _classB;
        private readonly IClassC _classC;

        public ClassA(IClassB classB, IClassC classC)
        {
            _classB = classB;
            _classC = classC;
        }

        public double Method1(int value1, int value2)
        {
            if (value2 == 0)
            {
                throw new ArgumentException("Second value should not be 0", nameof(value2));
            }

            var convertedValue1 = _classB.ConvertValue(value1);
            var convertedValue2 = _classB.ConvertValue(value2);
            var correctionValue = _classC.GetCorrectionValue(value1);

            return convertedValue1 / convertedValue2 + correctionValue;
        }
    }
}