using System;

namespace Task2
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
                if (long.TryParse(splits[i], out long result))
                {
                    if (1 > result || result > Math.Pow(10, 18))
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    throw new Exception();
                }
            }

            var n = long.Parse(splits[0]);
            var m = long.Parse(splits[1]);

            if (n == m)
            {
                Console.WriteLine("1");
            }
            else
            {
                var i = 1;

                do
                {
                    if (n <= m)
                    {
                        m -= n;
                    }
                    else
                    {
                        n -= m;
                    }
                    i++;
                } 
                while (Math.Abs(m-n) != 0);

                Console.WriteLine(i);
            }

            Console.ReadLine();
        }
    }
}