using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdajovkySem1
{
    internal class KDNode<T> where T : IComparable
    {
        public T _data { get; set; }
        public KDNode<T> _parent { get; set; }
        public KDNode<T> _leftSon { get; set; }
        public KDNode<T> _rightSon { get; set; }

        public KDNode(T data)
        {
            _data = data;
            _parent = null;
            _leftSon = null;
            _rightSon = null;
        }

    }
}
