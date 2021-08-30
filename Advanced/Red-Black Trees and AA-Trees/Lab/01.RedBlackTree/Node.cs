namespace _01.RedBlackTree
{
    using System;

    public partial class RedBlackTree<T> where T : IComparable
    {
        public class Node
        {
            public Node(T value)
            {
                this.Value = value;
                this.Color = Color.Red;
            }

            public Color Color { get; set; }

            public T Value { get; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node Parent { get; set; }

            public int Count { get; set; }

            public override string ToString()
            {
                if (Color == Color.Black)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }

                if (Color == Color.Red)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                if (Parent == null)
                {
                    return $"{Value}";
                }
                return $"{Value}P:{Parent.Value}H:{Count}";
            }

            public void Recolor()
            {
                if (this.Color == Color.Black)
                {
                    this.Color = Color.Red;
                }
                else
                {
                    this.Color = Color.Black;
                }
            }
        }

    }
}