using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdajovkySem1.StructureTester;

namespace UdajovkySem1
{
    internal class KDTree<T> : IInsertable<T> where T : IComparable
    {
        public KDNode<T> _root { get; set; }

        public KDTree()
        {
            _root = null;
        }

        public void Insert(T data)
        {
            if (_root == null)
            {
                _root = new KDNode<T>(data);
            }
            else
            {
                KDNode<T> current = _root;
                KDNode<T> parent = null;
                int depth = 0;
                while (current != null)
                {
                    parent = current;
                    if (current._data.CompareTo(data, depth) <= 0)
                    {
                        current = current._leftSon;
                        //Console.WriteLine("left");
                    }
                    else if (current._data.CompareTo(data, depth) > 0)
                    {
                        current = current._rightSon;
                        //Console.WriteLine("right");
                    }
                    depth++;
                }
                if (parent._data.CompareTo(data, depth - 1) <= 0)
                {
                    parent._leftSon = new KDNode<T>(data);
                    parent._leftSon._parent = parent;
                    //Console.WriteLine("vlozil som do laveho");
                    //Console.WriteLine(parent._leftSon._data);
                }
                else
                {
                    parent._rightSon = new KDNode<T>(data);
                    parent._rightSon._parent = parent;
                    //Console.WriteLine("vlozil som do praveho");
                    //Console.WriteLine(parent._rightSon._data);
                }
            }
        }

        public void Delete()
        {

        }

        public List<T> Find(T data)
        {
            List<T> found = new List<T>();
            KDNode<T> current = _root;
            int depth = 0;

            while (current != null)
            {
                if (current._data.Equals(data))
                {
                    found.Add(current._data);
                }

                int comparison = current._data.CompareTo(data, depth);
                if (comparison <= 0)
                {
                    current = current._leftSon;
                }
                else
                {
                    current = current._rightSon;
                }
                depth++;
            }

            return found;
        }

        public void Print()
        {
            // TODO zbavit sa rekurzie
            PrintSubTree(_root, 0,"Root");
        }

        private void PrintSubTree(KDNode<T> node, int depth, string position)
        {
            if (node == null)
                return;

            Console.WriteLine(new string(' ', depth * 2) + depth + " " + position + ": " + node._data);

            PrintSubTree(node._leftSon, depth + 1, "Left Son");
            PrintSubTree(node._rightSon, depth + 1, "Right Son");
        }
    }
}
