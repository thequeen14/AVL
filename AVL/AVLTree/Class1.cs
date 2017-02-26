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

    public class AVLTree
    {
        public int CheckBalanceFactor()
        {
            return 1;
        }
    }
}
