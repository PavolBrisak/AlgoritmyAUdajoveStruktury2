using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdajovkySem1.StructureTester;

namespace UdajovkySem1
{
    public class KDTree<T> : IInsertable<T> where T : IComparable
    {
        public KDNode<T> Root { get; set; }
        private KDTreeIterator<T> _iterator;

        public KDTree()
        {
            Root = null;
            _iterator = new KDTreeIterator<T>();
        }

        public void Insert(T data)
        {
            if (Root == null)
            {
                Root = new KDNode<T>(data);
            }
            else
            {
                KDNode<T> current = Root;
                KDNode<T> parent = null;
                int depth = 0;
                while (current != null)
                {
                    parent = current;
                    if (current.Data.CompareTo(data, depth) <= 0)
                    {
                        current = current.LeftSon;
                    }
                    else if (current.Data.CompareTo(data, depth) > 0)
                    {
                        current = current.RightSon;
                    }
                    depth++;
                }
                if (parent.Data.CompareTo(data, depth - 1) <= 0)
                {
                    parent.LeftSon = new KDNode<T>(data);
                    parent.LeftSon.Parent = parent;
                    parent.LeftSon.Depth = depth;
                }
                else
                {
                    parent.RightSon = new KDNode<T>(data);
                    parent.RightSon.Parent = parent;
                    parent.RightSon.Depth = depth;
                }
            }
        }

        public void Delete(T data)
        {
            KDNode<T> node = FindNode(data);
            if (node == null)
            {
                return;
            }

            if (node.IsLeaf())
            {
                if (Root == node)
                {
                    Root = null;
                    return;
                }
                DeleteLeaf(node);
                return;
            }

            List<KDNode<T>> replacements = new List<KDNode<T>>();
            KDNode<T> replacementNode = null;

            if (node.RightSon != null)
            {
                replacementNode = _iterator.FindMin(node.RightSon, node.Depth);
            }
            else if (node.LeftSon != null)
            {
                replacementNode = _iterator.FindMax(node.LeftSon, node.Depth);
            }

            if (replacementNode.IsLeaf())
            {
                SwitchNodes(replacementNode, node);
            }
            else
            {
                replacements.Add(replacementNode);
                bool end = true;
                while (replacementNode != null && end)
                {
                    if (replacementNode.RightSon != null)
                    {
                        replacementNode = _iterator.FindMin(replacementNode, replacementNode.Depth);
                    }
                    else if (replacementNode.LeftSon != null)
                    {
                        replacementNode = _iterator.FindMax(replacementNode, replacementNode.Depth);
                    }
                    replacements.Add(replacementNode);
                    if (replacementNode.IsLeaf())
                    {
                        end = false;
                    }
                }

                for (int i = 0; i <= replacements.Count - 1; i++)
                {
                    SwitchNodes(replacements[i], node);
                }
            }

            DeleteLeaf(node);
        }

        private void DeleteLeaf(KDNode<T> node)
        {
            if (node.Parent.LeftSon == node)
            {
                node.Parent.LeftSon = null;
            }
            else
            {
                node.Parent.RightSon = null;
            }
            node.Parent = null;
        }

        private KDNode<T> FindNode(T data)
        {
            KDNode<T> current = Root;
            int depth = 0;

            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    return current;
                }

                int comparison = current.Data.CompareTo(data, depth);
                if (comparison <= 0)
                {
                    current = current.LeftSon;
                }
                else
                {
                    current = current.RightSon;
                }
                depth++;
            }

            return null;
        }

        private void SwitchNodes(KDNode<T> node1, KDNode<T> node2)
        {
            if (node1 == null || node2 == null)
                return;

            // Swap depths
            int tempDepth = node1.Depth;
            node1.Depth = node2.Depth;
            node2.Depth = tempDepth;

            // Swap parents
            KDNode<T> tempParent = node1.Parent;
            node1.Parent = node2.Parent;
            node2.Parent = tempParent;

            // Update parent's child reference
            if (node1.Parent != null)
            {
                if (node1.Parent.LeftSon == node2)
                {
                    node1.Parent.LeftSon = node1;
                }
                else
                {
                    node1.Parent.RightSon = node1;
                }
            }
            if (node2.Parent != null)
            {
                if (node2.Parent.LeftSon == node1)
                {
                    node2.Parent.LeftSon = node2;
                }
                else
                {
                    node2.Parent.RightSon = node2;
                }
            }

            // Swap children
            KDNode<T> tempLeftSon = node1.LeftSon;
            KDNode<T> tempRightSon = node1.RightSon;
            node1.LeftSon = node2.LeftSon;
            node1.RightSon = node2.RightSon;
            node2.LeftSon = tempLeftSon;
            node2.RightSon = tempRightSon;

            // Update children's parent reference
            if (node1.LeftSon != null)
            {
                node1.LeftSon.Parent = node1;
            }
            if (node1.RightSon != null)
            {
                node1.RightSon.Parent = node1;
            }
            if (node2.LeftSon != null)
            {
                node2.LeftSon.Parent = node2;
            }
            if (node2.RightSon != null)
            {
                node2.RightSon.Parent = node2;
            }

            // Update root if necessary
            if (Root == node1)
            {
                Root = node2;
            }
            else if (Root == node2)
            {
                Root = node1;
            }
        }

        public List<T> Find(T data)
        {
            List<T> found = new List<T>();
            KDNode<T> current = Root;
            int depth = 0;

            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    found.Add(current.Data);
                }

                int comparison = current.Data.CompareTo(data, depth);
                if (comparison <= 0)
                {
                    current = current.LeftSon;
                }
                else
                {
                    current = current.RightSon;
                }
                depth++;
            }

            return found;
        }

        public string Print()
        {
            StringBuilder stringBuilder = new StringBuilder();
            PrintSubTree(Root, 0, "Root", stringBuilder);
            if (stringBuilder.Length == 0)
            {
                return "Tree is empty";
            }
            return stringBuilder.ToString();
        }

        private void PrintSubTree(KDNode<T> node, int depth, string position, StringBuilder stringBuilder)
        {
            if (node == null)
                return;

            stringBuilder.AppendLine(new string(' ', depth * 2) + depth + " " + position + ": " + node.Data);

            PrintSubTree(node.LeftSon, depth + 1, "Left Son", stringBuilder);
            PrintSubTree(node.RightSon, depth + 1, "Right Son", stringBuilder);
        }
    }
    public class KDTreeIterator<T> where T : IComparable
    {
        public KDNode<T> FindMin(KDNode<T> node, int depth)
        {
            if (node == null)
                return null;

            Stack<KDNode<T>> stack = new Stack<KDNode<T>>();
            KDNode<T> current = node;
            KDNode<T> minNode = node;

            while (stack.Count > 0 || current != null)
            {
                while (current != null)
                {
                    stack.Push(current);
                    current = current.LeftSon;
                }

                current = stack.Pop();

                if (current.Data.CompareTo(minNode.Data, depth) < 0)
                {
                    minNode = current;
                }

                current = current.RightSon;
            }

            return minNode;
        }

        public KDNode<T> FindMax(KDNode<T> node, int depth)
        {
            if (node == null)
                return null;

            Stack<KDNode<T>> stack = new Stack<KDNode<T>>();
            KDNode<T> current = node;
            KDNode<T> maxNode = node;

            while (stack.Count > 0 || current != null)
            {
                while (current != null)
                {
                    stack.Push(current);
                    current = current.LeftSon;
                }

                current = stack.Pop();

                if (current.Data.CompareTo(maxNode.Data, depth) > 0)
                {
                    maxNode = current;
                }

                current = current.RightSon;
            }

            return maxNode;
        }
    }
}
