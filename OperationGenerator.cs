using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdajovkySem1
{
    public class OperationGenerator
    {
        public double GenerateValueFromMinMax(double min, double max)
        {
            Random random = new Random();
            return random.NextDouble() * (max - min) + min;
        }

        internal GPSPosition GenerateGPSPosition()
        {
            throw new NotImplementedException();
        }
    }
}
