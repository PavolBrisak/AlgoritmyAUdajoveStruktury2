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
        public KDTreeLevelOrderIterator<T> Iterator { get; set; }

        public KDTree()
        {
            Root = null;
            Iterator = new KDTreeLevelOrderIterator<T>();
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
                    if (current.Data.CompareTo(data, depth) < 0)
                    {
                        current = current.RightSon;
                    }
                    else if (current.Data.CompareTo(data, depth) >= 0)
                    {
                        current = current.LeftSon;
                    }
                    depth++;
                }
                if (parent.Data.CompareTo(data, depth - 1) < 0)
                {
                    parent.RightSon = new KDNode<T>(data);
                    parent.RightSon.Parent = parent;
                    parent.RightSon.Depth = depth;
                }
                else
                {
                    parent.LeftSon = new KDNode<T>(data);
                    parent.LeftSon.Parent = parent;
                    parent.LeftSon.Depth = depth;
                }
            }
        }

        public void Delete(T data)
        {
            KDNode<T> node = FindSpecificNode(data);
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

            KDNode<T> replacementNode = null;
            Iterator = new KDTreeLevelOrderIterator<T>();
            List<KDNode<T>> nodesToBeInsertedBack = new List<KDNode<T>>();


            if (node.RightSon != null)
            {
                replacementNode = Iterator.FindMin(node.RightSon, node.Depth);
                Console.WriteLine("Replacement node: " + replacementNode.Data + " for node: " + node.Data);

                bool end = true;
                while (end)
                {
                    KDNode<T> nodeToBeInsertedBack = Iterator.FindEqual(node.RightSon, node.Depth, replacementNode);
                    if (nodeToBeInsertedBack != null)
                    {
                        Console.WriteLine("Node to be inserted back: " + nodeToBeInsertedBack.Data + " for replacement node: " + replacementNode.Data);
                        nodesToBeInsertedBack.Add(nodeToBeInsertedBack);
                        Delete(nodeToBeInsertedBack.Data);
                    }
                    else
                    {
                        end = false;
                    }
                }

            }
            else if (node.LeftSon != null)
            {
                replacementNode = Iterator.FindMax(node.LeftSon, node.Depth);
                Console.WriteLine("Replacement node: " + replacementNode.Data + " for node: " + node.Data);

                bool end = true;
                while (end)
                {
                    KDNode<T> nodeToBeInsertedBack = Iterator.FindEqual(node.LeftSon, node.Depth, replacementNode);
                    if (nodeToBeInsertedBack != null)
                    {
                        Console.WriteLine("Node to be inserted back: " + nodeToBeInsertedBack.Data + " for replacement node: " + replacementNode.Data);
                        nodesToBeInsertedBack.Add(nodeToBeInsertedBack);
                        Delete(nodeToBeInsertedBack.Data);
                    }
                    else
                    {
                        end = false;
                    }
                }
            }

            Console.WriteLine("Replacement node: " + replacementNode.Data + " for node: " + node.Data);

            if (replacementNode != null && replacementNode.IsLeaf())
            {
                SwitchNodes(replacementNode, node);
            }
            else
            {
                List<KDNode<T>> replacements = new List<KDNode<T>>();
                KDNode<T> replacementNodeCopy = replacementNode;
                bool end = true;
                while (replacementNode != null && end)
                {
                    if (replacementNode.RightSon != null)
                    {
                        replacementNode = Iterator.FindMin(replacementNode.RightSon, replacementNode.Depth);
                        bool endEqual = true;
                        while (endEqual)
                        {
                            KDNode<T> nodeToBeInsertedBack = Iterator.FindEqual(replacementNodeCopy.RightSon, replacementNodeCopy.Depth, replacementNode);
                            if (nodeToBeInsertedBack != null)
                            {
                                nodesToBeInsertedBack.Add(nodeToBeInsertedBack);
                                Delete(nodeToBeInsertedBack.Data);
                            }
                            else
                            {
                                endEqual = false;
                            }
                        }
                        replacementNodeCopy = replacementNode;
                    }
                    else if (replacementNode.LeftSon != null)
                    {
                        replacementNode = Iterator.FindMax(replacementNode.LeftSon, replacementNode.Depth);
                        bool endEqual = true;
                        while (endEqual)
                        {
                            KDNode<T> nodeToBeInsertedBack = Iterator.FindEqual(replacementNodeCopy.LeftSon, replacementNodeCopy.Depth, replacementNode);
                            if (nodeToBeInsertedBack != null)
                            {
                                nodesToBeInsertedBack.Add(nodeToBeInsertedBack);
                                Delete(nodeToBeInsertedBack.Data);
                            }
                            else
                            {
                                endEqual = false;
                            }
                        }
                        replacementNodeCopy = replacementNode;
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

            for (int i = 0; i <= nodesToBeInsertedBack.Count - 1; i++)
            {
                Insert(nodesToBeInsertedBack[i].Data);
                Console.WriteLine("bol som tu a vkladam " + nodesToBeInsertedBack[i].Data);
            }
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

        private KDNode<T> FindSpecificNode(T data)
        {
            KDNode<T> current = Root;
            int depth = 0;

            while (current != null)
            {
                if (current.Data.SpecificEquals(data))
                {
                    return current;
                }

                int comparison = current.Data.CompareTo(data, depth);
                if (comparison < 0)
                {
                    current = current.RightSon;
                }
                else
                {
                    current = current.LeftSon;
                }
                depth++;
            }

            return null;
        }

        private void SwitchNodes(KDNode<T> node1, KDNode<T> node2)
        {
            if (node1 == null || node2 == null)
            {
                return;
            }

            Console.WriteLine("Switching " + node1.Data + " with " + node2.Data);

            int tempDepth = node1.Depth;
            node1.Depth = node2.Depth;
            node2.Depth = tempDepth;

            KDNode<T> tempParent = node1.Parent;
            node1.Parent = node2.Parent;
            node2.Parent = tempParent;

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

            KDNode<T> tempLeftSon = node1.LeftSon;
            KDNode<T> tempRightSon = node1.RightSon;
            node1.LeftSon = node2.LeftSon;
            node1.RightSon = node2.RightSon;
            node2.LeftSon = tempLeftSon;
            node2.RightSon = tempRightSon;

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

            if (Root == node1)
            {
                Root = node2;
            }
            else if (Root == node2)
            {
                Root = node1;
            }

            Console.WriteLine("Switched " + node1.Data + " with " + node2.Data);
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
                if (comparison < 0)
                {
                    current = current.RightSon;
                }
                else
                {
                    current = current.LeftSon;
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

    public class KDTreeLevelOrderIterator<T> where T : IComparable
    {
        private KDNode<T> _current;
        private Queue<KDNode<T>> _queue;

        public KDTreeLevelOrderIterator()
        {
            _current = null;
            _queue = new Queue<KDNode<T>>();
        }

        public void ProcessNode()
        {
            if (_current == null)
                return;

            if (_current.LeftSon != null)
            {
                _queue.Enqueue(_current.LeftSon);
            }
            if (_current.RightSon != null)
            {
                _queue.Enqueue(_current.RightSon);
            }

            _current = _queue.Dequeue();
        }

        public KDNode<T> GetCurrent()
        {
            return _current;
        }

        private void ClearQueue()
        {
            while (_queue.Count > 0)
            {
                _queue.Dequeue();
            }
        }

        public KDNode<T> FindMin(KDNode<T> node, int depth)
        {
            if (node == null)
                return null;

            KDNode<T> minNode = node;
            _queue.Enqueue(node);

            while (_queue.Count > 0)
            {
                KDNode<T> current = _queue.Dequeue();

                if (current.Data.CompareTo(minNode.Data, depth) < 0)
                {
                    minNode = current;
                }

                if (current.LeftSon != null)
                {
                    _queue.Enqueue(current.LeftSon);
                }
                if (current.RightSon != null)
                {
                    _queue.Enqueue(current.RightSon);
                }
            }

            ClearQueue();
            return minNode;
        }

        public KDNode<T> FindMax(KDNode<T> node, int depth)
        {
            if (node == null)
                return null;

            KDNode<T> maxNode = node;
            _queue.Enqueue(node);

            while (_queue.Count > 0)
            {
                KDNode<T> current = _queue.Dequeue();

                if (current.Data.CompareTo(maxNode.Data, depth) > 0)
                {
                    maxNode = current;
                }

                if (current.LeftSon != null)
                {
                    _queue.Enqueue(current.LeftSon);
                }
                if (current.RightSon != null)
                {
                    _queue.Enqueue(current.RightSon);
                }
            }

            ClearQueue();
            return maxNode;
        }

        public KDNode<T> FindEqual(KDNode<T> son, int depth, KDNode<T> replacementNode)
        {
            if (son == null)
                return null;

            KDNode<T> current = son;
            _queue.Enqueue(son);

            while (_queue.Count > 0) {
                current = _queue.Dequeue();
                if ((current.Data.CompareTo(replacementNode.Data, depth)) == 0 && (current != replacementNode))
                {
                    return current;
                }
                if (current.LeftSon != null)
                {
                    _queue.Enqueue(current.LeftSon);
                }
                if (current.RightSon != null)
                {
                    _queue.Enqueue(current.RightSon);
                }
            }

            ClearQueue();
            return null;
        }
    }
}
