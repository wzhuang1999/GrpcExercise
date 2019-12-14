using System;
using Testing.Interfaces;

namespace Testing
{
    internal class ClassC : IClassC
    {
        private readonly Random _random = new Random(2354);

        public double GetCorrectionValue(int value1)
        {
            return Math.PI * value1 + _random.NextDouble();
        }
    }
}