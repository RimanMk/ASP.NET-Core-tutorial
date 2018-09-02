using System;

namespace DI.Services
{
    public class RandomCounter : ICounter
    {
        static Random Rnd = new Random();
        private readonly int _value;
        public RandomCounter()
        {
            _value = Rnd.Next(0, 1000000);
        }
        public int Value
        {
            get { return _value; }
        }
    }
}
