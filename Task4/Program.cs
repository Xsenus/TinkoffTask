using System;

namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            var line = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
            {
                throw new Exception();
            }

            var splits = line.Split(' ');
            if (splits.Length != 2)
            {
                throw new Exception();
            }

            for (int i = 0; i < splits.Length; i++)
            {
                if (int.TryParse(splits[i], out int result))
                {
                    if (1 > result || result > 50)
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    throw new Exception();
                }
            }

            var n = int.Parse(splits[0]);
            var m = int.Parse(splits[1]);

            var range = new int[n + 3, m + 3];
            range[2, 2] = 1;

            var x = 2;
            var y = 2;
            
            while ((x < n + 1) || (y < m + 1))
            {
                if (y == m + 1)
                {
                    x++;
                }
                else
                {
                    y++;
                }

                var i = x;
                var j = y;
                
                while ((i <= n + 1) && j >= 2)
                {
                    range[i, j] = range[i + 1, j - 2] + range[i - 1, j - 2] + range[i - 2, j - 1] + range[i - 2, j + 1];
                    i++;
                    j--;
                }
            }
            
            Console.WriteLine(range[n + 1, m + 1]);
        }
    }
}