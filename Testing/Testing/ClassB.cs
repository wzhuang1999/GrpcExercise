using System;
using Testing.Interfaces;

namespace Testing
{
    internal class ClassB : IClassB
    {
        public double ConvertValue(int value1)
        {
            // There is some very complicated logic ...
            return Math.Pow(value1, 2);
        }
    }
}