using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdajovkySem1.StructureTester;

namespace UdajovkySem1.Tester
{
    internal class StructureTester<Structure, Data> where Structure : IInsertable<Data>
    {
        private Structure _structure;
        private int _numberOfData;
        private Func<Data> _dataGenerator;

        public StructureTester(Structure structure, int numberOfData, Func<Data> dataGenerator)
        {
            _structure = structure;
            _numberOfData = numberOfData;
            _dataGenerator = dataGenerator;
        }

        public void TestInsert()
        {
            for (int i = 0; i < _numberOfData; i++)
            {
                Data data = _dataGenerator();
                _structure.Insert(data);
            }
        }
    }
}
