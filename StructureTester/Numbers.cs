using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdajovkySem1.StructureTester
{
    public class Numbers : IComparable
    {
        public Tuple<int, int, int> NumberTuple { get; set; }

        public Numbers(int first, int second, int third)
        {
            NumberTuple = new Tuple<int, int, int>(first, second, third);
        }
        public int CompareTo(object obj, int depth)
        {
            Numbers other = obj as Numbers;
            if (other == null)
            {
                throw new ArgumentException("Object is not of type Tuple<int, int, int>");
            }
            int comparison = depth % 3;
            switch (comparison)
            {
                case 0:
                    if (NumberTuple.Item1 == other.NumberTuple.Item1)
                    {
                        return 0;
                    }
                    else if (NumberTuple.Item1 > other.NumberTuple.Item1)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                case 1:
                    if (NumberTuple.Item2 == other.NumberTuple.Item2)
                    {
                        return 0;
                    }
                    else if (NumberTuple.Item2 > other.NumberTuple.Item2)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                case 2:
                    if (NumberTuple.Item3 == other.NumberTuple.Item3)
                    {
                        return 0;
                    }
                    else if (NumberTuple.Item3 < other.NumberTuple.Item3)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                default:
                    throw new InvalidOperationException("Unexpected depth index");
            }
        }

        public bool Equals(object obj)
        {
            Numbers other = obj as Numbers;
            if (other == null)
            {
                throw new ArgumentException("Object is not of type Tuple<int, int, int>");
            }
            return NumberTuple.Item1 == other.NumberTuple.Item1 && NumberTuple.Item2 == other.NumberTuple.Item2 && NumberTuple.Item3 == other.NumberTuple.Item3;
        }
        public override String ToString()
        {
            return $"{NumberTuple.Item1}, {NumberTuple.Item2}, {NumberTuple.Item3}";
        }
    }
}
