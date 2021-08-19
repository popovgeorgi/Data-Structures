using AVLTree;
using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new AVL<int>();
            tree.Insert(10);
            tree.Insert(9);
            tree.Insert(8);
            Console.WriteLine();
        }
    }
}
