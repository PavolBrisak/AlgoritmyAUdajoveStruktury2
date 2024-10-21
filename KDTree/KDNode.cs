using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdajovkySem1
{
    public class KDNode<T> where T : IComparable
    {
        public T Data { get; set; }
        public KDNode<T> Parent { get; set; }
        public KDNode<T> LeftSon { get; set; }
        public KDNode<T> RightSon { get; set; }

        public int Depth { get; set; }

        public KDNode(T data)
        {
            Data = data;
            Parent = null;
            LeftSon = null;
            RightSon = null;
        }

        public bool IsLeaf()
        {
            if (LeftSon == null && RightSon == null)
            {
                return true;
            }
            return false;
        }
    }
}
