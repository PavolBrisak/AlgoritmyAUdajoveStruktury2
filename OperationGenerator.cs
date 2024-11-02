using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdajovkySem1
{
    public class OperationGenerator
    {
        private readonly Random _random;

        public OperationGenerator(Random random)
        {
            _random = random;
        }

        public double GenerateValueFromMinMax(double min, double max)
        {
            double value = _random.NextDouble() * (max - min) + min;
            return Math.Round(value);
        }

        public double GenerateValueFromMinMax(double min, double max, int desMiesta)
        {
            double value = _random.NextDouble() * (max - min) + min;
            return Math.Round(value, desMiesta);
        }

        public int GenerateIntValue()
        {
            return _random.Next();
        }

        public int GenerateIntValue(int min, int max)
        {
            return _random.Next(min, max);
        }

        public string GenerateString(int maxLength)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, maxLength)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        public void GenerateInsert(int count, Action action)
        {
            for (int i = 0; i < count; i++)
            {
                action();
            }
        }

        public void GenerateFind(int count, Action action)
        {
            for (int i = 0; i < count; i++)
            {
                action();
            }
        }

        public void GenerateDelete(int count, Action action)
        {
            for (int i = 0; i < count; i++)
            {
                action();
            }
        }
    }
}
