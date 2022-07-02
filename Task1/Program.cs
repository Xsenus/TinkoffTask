using System;

namespace Task1
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
            if (splits.Length != 3)
            {
                throw new Exception();
            }

            for (int i = 0; i < splits.Length; i++)
            {
                if (int.TryParse(splits[i], out int result))
                {
                    if (0 > result || result > Math.Pow(10, 9))
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    throw new Exception();
                }
            }

            var a = int.Parse(splits[0]);
            var b = int.Parse(splits[1]);
            var n = int.Parse(splits[2]);

            if ((a - b) % 2 == 0 && (a - b) >= 2 * n) 
            {
                Console.WriteLine("YES");
            }
            else
            {
                Console.WriteLine("NO");
            }
        }
    }
}