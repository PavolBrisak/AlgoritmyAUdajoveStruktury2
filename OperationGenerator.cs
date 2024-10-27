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

        public OperationGenerator()
        {
            _random = new Random();
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

        public int GenerateIntValue(int max = 100)
        {
            return _random.Next(0,max);
        }

        public int GenerateIntValue(int min, int max)
        {
            return _random.Next(min, max);
        }

        public string GenerateString(int maxLength = 10)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            int length = _random.Next(1, maxLength + 1);
            return new string(Enumerable.Repeat(chars, length)
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
