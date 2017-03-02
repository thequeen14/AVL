using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVLTree
{
    public class Node<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public Node<TKey, TValue> Left { get; set; }
        public Node<TKey, TValue> Right { get; set; }
        public Node<TKey, TValue> Parent { get; set; }

        public Node(TKey key, TValue value)
        {
            Key = key;
            Value = value;
            Left = null;
            Right = null;
            Parent = null;
        }
    }

    public class AVLTree<TKey, TValue> where TKey : IComparable<TKey>
    {
        public int Count { get; private set; }
        private Node<TKey, TValue> _root;

        public AVLTree()
        {
            Count = 0;
            _root = null;
        }

        public void Add(TKey key, TValue value)
        {
            var node = new Node<TKey, TValue>(key, value);
            if (_root == null)
            {
                _root = node;
                Count = 1;
            }
            else
            {
                var tempNode = _root;
                Node<TKey, TValue> parent = null;

                while (tempNode != null)
                {
                    if (tempNode.Key.CompareTo(key) == 0)
                    {
                        throw new ArgumentException("Key must be unique.");
                    }
                    parent = tempNode;
                    if (tempNode.Key.CompareTo(key) > 0)
                    {
                        tempNode = tempNode.Left;
                    }
                    else
                    {
                        tempNode = tempNode.Right;
                    }
                }

                if (parent.Key.CompareTo(key) > 0)
                {
                    parent.Left = node;
                }
                else
                {
                    parent.Right = node;
                }
                node.Parent = parent;
                Count++;

                while (NodeNeeedsBalancing(_root))
                {
                    BalanceTree(FindPivotToBalance(node));
                }
            }
        }

        private bool NodeNeeedsBalancing(Node<TKey, TValue> currentNode)
        {
            if (CheckBalanceFactor(currentNode) == 0 || CheckBalanceFactor(currentNode) == 1 || CheckBalanceFactor(currentNode) == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private Node<TKey, TValue> FindPivotToBalance(Node<TKey, TValue> currentNode)
        {
            Node<TKey, TValue> findPivot = _root;
            while (NodeNeeedsBalancing(findPivot))
            {
                if (currentNode.Key.CompareTo(_root.Key) > 0)
                {
                    findPivot = findPivot.Right;
                }
                else
                {
                    findPivot = findPivot.Left;
                }
            }
            return findPivot.Parent;
        }

        private void BalanceTree(Node<TKey, TValue> currentNode)
        {
            int balanceFactor = CheckBalanceFactor(currentNode);
            if (balanceFactor > 1)
            {
                
            }
        }

        private int CheckBalanceFactor(Node<TKey, TValue> currentNode)
        {
            return GetHeight(currentNode.Right) - GetHeight(currentNode.Left);
        }

        public int GetHeight(Node<TKey, TValue> currentNode)
        {
            int height = 0;

            if (currentNode != null)
            {
                height = Math.Max(GetHeight(currentNode.Left), GetHeight(currentNode.Right)) + 1;
            }

            return height;
        }

        public bool Contains(TKey key)
        {
            if (Find(key, _root) != null)
            {
                return true;
            }
                return false;
        }

        private Node<TKey, TValue> Find(TKey key, Node<TKey, TValue> currentNode)
        {
            if (currentNode.Key.CompareTo(key) == 0)
            {
                return currentNode;
            }
            if (currentNode.Key.CompareTo(key) > 0)
            {
                if (currentNode.Left != null)
                {
                    return Find(key, currentNode.Left);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if (currentNode.Right != null)
                {
                    return Find(key, currentNode.Right);
                }
                else
                {
                    return null;
                }
            }
        }

        private void SmallLeftRotation(Node<TKey, TValue> currentNode)
        {
            Node<TKey, TValue> tempNode = currentNode.Right;
            currentNode.Right = tempNode.Left;
            tempNode.Left = currentNode;
            tempNode.Parent = currentNode.Parent;
            currentNode.Parent = tempNode;
            currentNode.Right.Parent = currentNode;
        }

        private void BigLeftRotation(Node<TKey, TValue> currentNode)
        {
            SmallRightRotation(currentNode.Right);
            SmallLeftRotation(currentNode);
        }

        private void SmallRightRotation(Node<TKey, TValue> currentNode)
        {
            Node<TKey, TValue> tempNode = currentNode.Left;
            currentNode.Left = tempNode.Right;
            tempNode.Right = currentNode;
            tempNode.Parent = currentNode.Parent;
            currentNode.Parent = tempNode;
            currentNode.Left.Parent = currentNode;
        }

        private void BigRightRotation(Node<TKey, TValue> currentNode)
        {
            SmallLeftRotation(currentNode.Left);
            SmallRightRotation(currentNode);
        }
    }
}
