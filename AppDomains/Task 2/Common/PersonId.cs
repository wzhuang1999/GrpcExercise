using System;

namespace Common
{
    [Serializable]
    public class PersonId
    {
        public int Value { get; }

        public PersonId(int value)
        {
            this.Value = value;
        }
    }
}