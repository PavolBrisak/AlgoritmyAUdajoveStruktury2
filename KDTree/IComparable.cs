using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdajovkySem1
{
    public interface IComparable
    {
        int CompareTo(object obj, int depth);
        bool Equals(object obj);
        int GetDimension();
        bool SpecificEquals(object obj);
        object GetUniqueId();
    }
}
