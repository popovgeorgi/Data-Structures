using System;
using Tree;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = new string[]
               {
                "7 19",
                "7 21",
                "7 14",
                "19 1",
                "19 12",
                "19 31",
                "14 23",    
                "14 6"
               };

            var factory = new TreeFactory();

            var tree = factory.CreateTreeFromStrings(input);
            var result = tree.GetAsString();
            Console.WriteLine(result);
        }
    }
}
