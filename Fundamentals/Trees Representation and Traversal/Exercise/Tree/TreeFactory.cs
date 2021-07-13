namespace Tree
{
    using System;
    using System.Collections.Generic;

    public class TreeFactory
    {
        private Dictionary<int, Tree<int>> nodesBykeys;

        public TreeFactory()
        {
            this.nodesBykeys = new Dictionary<int, Tree<int>>();
        }

        public Tree<int> CreateTreeFromStrings(string[] input)
        {
            var tree = new Tree<int>();

            for (int i = 0; i < input.Length; i++)
            {
                var splittedInput = input[i].Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
                var parent = int.Parse(splittedInput[0]);
                var child = int.Parse(splittedInput[1]);

                if (i == 0)
                {
                    tree = new Tree<int>(parent, new Tree<int>(child));
                }
                else
                {
                    tree.AddChild(parent, new Tree<int>(child));
                }
            }

            return tree;
        }

        public Tree<int> CreateNodeByKey(int key)
        {
            throw new NotImplementedException();
        }

        public void AddEdge(int parent, int child)
        {
            throw new NotImplementedException();
        }

        private Tree<int> GetRoot()
        {
            throw new NotImplementedException();
        }
    }
}
