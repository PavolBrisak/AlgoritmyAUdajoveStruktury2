using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdajovkySem1
{
    public class KDTree<T>  where T : IComparable
    {
        public KDNode<T> Root { get; set; }
        public KDTreeLevelOrderIterator<T> Iterator { get; set; }

        public KDTree()
        {
            Root = null;
            Iterator = new KDTreeLevelOrderIterator<T>(Root);
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
            List<KDNode<T>> nodesToBeDeleted = new List<KDNode<T>>();
            KDNode<T> mainNode = FindSpecificNode(data);
            List<KDNode<T>> nodesToBeInsertedBack = new List<KDNode<T>>();

            if (mainNode != null)
            {
                nodesToBeDeleted.Add(mainNode);
            }

            while (nodesToBeDeleted.Count > 0)
            {
                KDNode<T> node = nodesToBeDeleted[0];
                nodesToBeDeleted.RemoveAt(0);

                if (node.IsLeaf())
                {
                    if (Root == node)
                    {
                        Root = null;
                        continue;
                    }

                    DeleteLeaf(node);
                    continue;
                }

                KDNode<T> replacementNode = null;
                KDTreeIteratorForMinMax<T> iterator = new KDTreeIteratorForMinMax<T>();

                if (node.RightSon != null)
                {
                    replacementNode = iterator.FindMin(node, node.RightSon);

                    bool end = true;
                    while (end)
                    {
                        KDNode<T> nodeToBeInsertedBack = iterator.FindEqual(node.RightSon, node.Depth, replacementNode,
                            nodesToBeDeleted);
                        if (nodeToBeInsertedBack != null)
                        {
                            if ((!nodesToBeInsertedBack.Contains(nodeToBeInsertedBack)) &&
                                (!nodesToBeDeleted.Contains(nodeToBeInsertedBack)))
                            {
                                nodesToBeInsertedBack.Add(nodeToBeInsertedBack);
                                nodesToBeDeleted.Add(nodeToBeInsertedBack);
                            }
                            else
                            {
                                end = false;
                            }
                        }
                        else
                        {
                            end = false;
                        }
                    }

                }
                else if (node.LeftSon != null)
                {
                    replacementNode = iterator.FindMax(node, node.LeftSon);

                    bool end = true;
                    while (end)
                    {
                        KDNode<T> nodeToBeInsertedBack =
                            iterator.FindEqual(node.LeftSon, node.Depth, replacementNode, nodesToBeDeleted);
                        if (nodeToBeInsertedBack != null)
                        {
                            if ((!nodesToBeInsertedBack.Contains(nodeToBeInsertedBack)) &&
                                (!nodesToBeDeleted.Contains(nodeToBeInsertedBack)))
                            {
                                nodesToBeInsertedBack.Add(nodeToBeInsertedBack);
                                nodesToBeDeleted.Add(nodeToBeInsertedBack);
                            }
                            else
                            {
                                end = false;
                            }
                        }
                        else
                        {
                            end = false;
                        }
                    }
                }

                if (replacementNode != null && replacementNode.IsLeaf())
                {
                    SwitchNodes(replacementNode, node);
                }
                else
                {
                    List<KDNode<T>> replacements = new List<KDNode<T>>();
                    replacements.Add(replacementNode);
                    KDNode<T> replacementNodeCopy = replacementNode;
                    bool end = true;
                    while (replacementNode != null && end)
                    {
                        if (replacementNode.RightSon != null)
                        {
                            replacementNode = iterator.FindMin(replacementNode, replacementNode.RightSon);
                            bool endEqual = true;
                            while (endEqual)
                            {
                                KDNode<T> nodeToBeInsertedBack = iterator.FindEqual(replacementNodeCopy.RightSon,
                                    replacementNodeCopy.Depth, replacementNode, nodesToBeDeleted);
                                if (nodeToBeInsertedBack != null)
                                {
                                    if ((!nodesToBeInsertedBack.Contains(nodeToBeInsertedBack)) &&
                                        (!nodesToBeDeleted.Contains(nodeToBeInsertedBack)))
                                    {
                                        nodesToBeInsertedBack.Add(nodeToBeInsertedBack);
                                        nodesToBeDeleted.Add(nodeToBeInsertedBack);
                                    }
                                    else
                                    {
                                        endEqual = false;
                                    }
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
                            replacementNode = iterator.FindMax(replacementNode, replacementNode.LeftSon);
                            bool endEqual = true;
                            while (endEqual)
                            {
                                KDNode<T> nodeToBeInsertedBack = iterator.FindEqual(replacementNodeCopy.LeftSon,
                                    replacementNodeCopy.Depth, replacementNode, nodesToBeDeleted);
                                if (nodeToBeInsertedBack != null)
                                {
                                    if ((!nodesToBeInsertedBack.Contains(nodeToBeInsertedBack)) &&
                                        (!nodesToBeDeleted.Contains(nodeToBeInsertedBack)))
                                    {
                                        nodesToBeInsertedBack.Add(nodeToBeInsertedBack);
                                        nodesToBeDeleted.Add(nodeToBeInsertedBack);
                                    }
                                    else
                                    {
                                        endEqual = false;
                                    }
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
            }

            for (int i = 0; i <= nodesToBeInsertedBack.Count - 1; i++)
            {
                Insert(nodesToBeInsertedBack[i].Data);
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

        public T FindSpecificData(T data)
        {
            KDNode<T> node = FindSpecificNode(data);
            if (node == null)
            {
                return default(T);
            }
            return node.Data;
        }

        private void SwitchNodes(KDNode<T> node1, KDNode<T> node2)
        {
            if (node1 == null || node2 == null)
            {
                return;
            }

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
            PrintSubTree(Root, stringBuilder);
            if (stringBuilder.Length == 0)
            {
                return "Tree is empty";
            }
            return stringBuilder.ToString();
        }

        private void PrintSubTree(KDNode<T> node, StringBuilder stringBuilder)
        {
            if (node == null)
                return;

            Stack<(KDNode<T> node, int depth, string position)> stack = new Stack<(KDNode<T>, int, string)>();
            stack.Push((node, 0, "Root"));

            while (stack.Count > 0)
            {
                var (currentNode, depth, position) = stack.Pop();
                stringBuilder.AppendLine(new string(' ', depth * 2) + depth + " " + position + ": " + currentNode.Data);

                if (currentNode.RightSon != null)
                {
                    stack.Push((currentNode.RightSon, depth + 1, "Right Son"));
                }
                if (currentNode.LeftSon != null)
                {
                    stack.Push((currentNode.LeftSon, depth + 1, "Left Son"));
                }
            }
        }
    }

    public class KDTreeIteratorForMinMax<T> where T : IComparable
    {
        private KDNode<T> _current;
        private Queue<KDNode<T>> _queue;

        public KDTreeIteratorForMinMax()
        {
            _current = null;
            _queue = new Queue<KDNode<T>>();
        }
        private void ClearQueue()
        {
            while (_queue.Count > 0)
            {
                _queue.Dequeue();
            }
        }
        public KDNode<T> FindMin(KDNode<T> mainNode, KDNode<T> hisSon)
        {
            if (hisSon == null)
                return null;

            ClearQueue();
            KDNode<T> minNode = hisSon;
            _queue.Enqueue(hisSon);

            int dimensionMainNode = mainNode.Data.GetDimension();
            int mainDepth = mainNode.Depth;
            int mainComparisonValue = mainDepth % dimensionMainNode;

            while (_queue.Count > 0)
            {
                KDNode<T> current = _queue.Dequeue();

                if (current.Data.CompareTo(minNode.Data, mainDepth) < 0)
                {
                    minNode = current;
                }

                int currentComparisonValue = current.Depth % dimensionMainNode;

                if (currentComparisonValue == mainComparisonValue)
                {
                    if (current.LeftSon != null)
                    {
                        _queue.Enqueue(current.LeftSon);
                    }
                }
                else
                {
                    if (current.LeftSon != null)
                    {
                        _queue.Enqueue(current.LeftSon);
                    }
                    if (current.RightSon != null)
                    {
                        _queue.Enqueue(current.RightSon);
                    }
                }
            }
            return minNode;
        }

        public KDNode<T> FindMax(KDNode<T> mainNode, KDNode<T> hisSon)
        {
            if (hisSon == null)
                return null;

            ClearQueue();
            KDNode<T> maxNode = hisSon;
            _queue.Enqueue(hisSon);

            int dimensionMainNode = mainNode.Data.GetDimension();
            int mainDepth = mainNode.Depth;
            int mainComparisonValue = mainDepth % dimensionMainNode;

            while (_queue.Count > 0)
            {
                KDNode<T> current = _queue.Dequeue();

                if (current.Data.CompareTo(maxNode.Data, mainDepth) > 0)
                {
                    maxNode = current;
                }

                int currentComparisonValue = current.Depth % dimensionMainNode;

                if (currentComparisonValue == mainComparisonValue)
                {
                    if (current.RightSon != null)
                    {
                        _queue.Enqueue(current.RightSon);
                    }
                }
                else
                {
                    if (current.LeftSon != null)
                    {
                        _queue.Enqueue(current.LeftSon);
                    }

                    if (current.RightSon != null)
                    {
                        _queue.Enqueue(current.RightSon);
                    }
                }
            }

            return maxNode;
        }
        public KDNode<T> FindEqual(KDNode<T> son, int depth, KDNode<T> replacementNode, List<KDNode<T>> nodesToBeDeleted)
        {
            if (son == null)
                return null;

            if (son.LeftSon == null && son.RightSon == null)
                return null;

            ClearQueue();
            KDNode<T> current = son;
            _queue.Enqueue(son);

            while (_queue.Count > 0)
            {
                current = _queue.Dequeue();
                if ((current.Data.CompareTo(replacementNode.Data, depth)) == 0 && (current != replacementNode) && (!nodesToBeDeleted.Contains(current)))
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

            return null;
        }
    }

    public class KDTreeLevelOrderIterator<T> where T : IComparable
    {
        private KDNode<T> _current;
        private Queue<KDNode<T>> _queue;

        public KDTreeLevelOrderIterator(KDNode<T> start)
        {
            _current = start;
            _queue = new Queue<KDNode<T>>();
            if (_current != null)
            {
                _queue.Enqueue(_current);
            }
        }

        public void ProcessNode()
        {
            _current = _queue.Dequeue();

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
        public bool HasNext()
        {
            return _queue.Count > 0;
        }
    }
}
