using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdajovkySem1.StructureTester
{
    internal interface IInsertable<T>
    {
        void Insert(T data);
    }
}
