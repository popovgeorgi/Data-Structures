using _01.RedBlackTree;
using System;
using System.Security.Cryptography;

namespace Test
{
    class Program
    {
        static void Main()
        {

            RedBlackTree<int> rbt = new RedBlackTree<int>();

            for (int i = 0; i < 1; i++)
            {
                rbt.Insert(i);
            }

            Console.WriteLine(rbt.Count());
        }
    }
}
