namespace Problem04.BalancedParentheses
{
    using System;
    using System.Collections.Generic;

    public class BalancedParenthesesSolve : ISolvable
    {
        public bool AreBalanced(string parentheses)
        {
            var splitted = parentheses.ToCharArray();

            var stack = new Stack<char>();
            int previous = -1;

            for (int i = 0; i < splitted.Length; i++)
            {
                var current = (int)splitted[i];

                if (i > 0 && ((previous + 1 == current) || (previous + 2 == current)))
                {
                    stack.Pop();

                    if (stack.Count == 0)
                    {
                        previous = -1;
                    }
                    else
                    {
                        previous = stack.Peek();
                    }
                }
                else
                {
                    stack.Push(splitted[i]);
                    previous = current;
                }
            }

            if (stack.Count == 0)
            {
                return true;
            }

            return false;
        }
    }
}
