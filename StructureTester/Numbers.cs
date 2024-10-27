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
        public double A { get; set; }
        public string B { get; set; }
        public int C { get; set; }
        public double D { get; set; }

        private const double Tolerance = 1e-5;
        public int UniqueId { get; set; }


        public Numbers(double a, string b, int c, double d, int uniqueId)
        {
            A = a;
            B = b;
            C = c;
            D = d;
            UniqueId = uniqueId;
        }

        public int CompareTo(object obj, int depth)
        {
            Numbers other = obj as Numbers;
            if (other == null)
            {
                throw new ArgumentException("Object is not a GPSPosition");
            }

            int compareValue = depth % 4;
            int stringComparison;

            switch (compareValue)
            {
                case 0:
                    if (Math.Abs(Math.Abs(A) - Math.Abs(other.A)) < Tolerance)
                    {
                        stringComparison = string.Compare(B, other.B, StringComparison.Ordinal);
                        if (stringComparison == 0)
                            return 0;
                        return stringComparison < 0 ? -1 : 1;
                    }
                    return A < other.A ? -1 : 1;
                case 1:
                    if (C == other.C)
                    {
                        return 0;
                    }
                    return C < other.C ? -1 : 1;
                case 2:
                    if (Math.Abs(Math.Abs(D) - Math.Abs(other.D)) < Tolerance)
                    {
                        return 0;
                    }
                    return D < other.D ? -1 : 1;
                case 3:
                    stringComparison = string.Compare(B, other.B, StringComparison.Ordinal);
                    if (stringComparison == 0)
                    {
                        if (C == other.C)
                        {
                            return 0;
                        }
                        return C < other.C ? -1 : 1;
                    }
                    return stringComparison < 0 ? -1 : 1;
            }
            return 0;
        }

        public bool Equals(object obj)
        {
            Numbers other = obj as Numbers;
            if (other == null)
            {
                throw new ArgumentException("Object is not a GPSPosition");
            }

            int stringComparison = string.Compare(B, other.B, StringComparison.Ordinal);

            if (Math.Abs(Math.Abs(A) - Math.Abs(other.A)) < Tolerance &&
                Math.Abs(Math.Abs(D) - Math.Abs(other.D)) < Tolerance &&
                C == other.C &&
                stringComparison == 0)
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"A: {A}, B: {B}, C: {C}, D: {D}, unique ID : {UniqueId}";
        }

        public bool SpecificEquals(object obj)
        {
            Numbers other = obj as Numbers;
            if (other == null)
            {
                throw new ArgumentException("Object is not a GPSPosition");
            }
            if (UniqueId == other.UniqueId)
            {
                return true;
            }
            return false;
        }
    }
}
