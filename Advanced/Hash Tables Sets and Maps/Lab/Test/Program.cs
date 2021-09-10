namespace Test
{
    using System;
    using System.Collections.Generic;

    using HashTable;

    public class Program
    {
        public static void Main()
        {
            var hashTable = new HashTable<string, int>(2);
            hashTable.Add("Georgi", 2);
            hashTable["Peter"] = 5;
            Console.WriteLine(hashTable["Ivan"]);

        }
    }
}
