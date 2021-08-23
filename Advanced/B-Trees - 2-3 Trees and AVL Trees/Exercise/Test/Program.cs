using _01.Hierarchy;
using _02.Two_Three;
using _03.AVL;
using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            AVL<int> avl = new AVL<int>();
            for (int i = 1; i < 10; i++)
            {
                avl.Insert(i);
            }

            avl.Delete(4);
            Console.WriteLine();

            //// Assert
            //Assert.AreEqual(5, root.Value);
            //Assert.AreEqual(4, root.Height);

            //// Nodes of height 1
            //Assert.AreEqual(1, root.Left.Left.Height); // 1
            //Assert.AreEqual(1, root.Left.Right.Height); // 3
            //Assert.AreEqual(1, root.Right.Left.Right.Height); // 7
            //Assert.AreEqual(1, root.Right.Right.Height); // 9

            //// Nodes of height 2
            //Assert.AreEqual(2, root.Left.Height); // 2
            //Assert.AreEqual(2, root.Right.Left.Height); //6

            //// Nodes of height 3
            //Assert.AreEqual(3, root.Right.Height); // 8

            //// Nodes of height 4
            //Assert.AreEqual(4, root.Height); // 5

        }
    }
}
