using _03.MinHeap;
using System;

namespace _04.CookiesProblem
{
    public class CookiesProblem
    {
        public int Solve(int k, int[] cookies)
        {
            var minHeap = new MinHeap<int>();

            for (int i = 0; i < cookies.Length; i++)
            {
                minHeap.Add(cookies[i]);
            }

            if (minHeap.Size == 0)
            {
                return -1;
            }

            var operations = 0;

            while (true)
            {
                if (minHeap.Size == 0)
                {
                    return -1;
                }
                var removed = minHeap.Dequeue();
                if (removed >= k)
                {
                    return operations;
                }

                if (minHeap.Size <= 1)
                {
                    return -1;
                }
                var secondRemoved = minHeap.Dequeue();
                minHeap.Dequeue();

                var newCookie = removed + 2 * secondRemoved;
                minHeap.Add(newCookie);
                operations++;
            }
        }
    }
}
