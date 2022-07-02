using System;
using System.Linq;

namespace Task3
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

            if (int.TryParse(line, out int n) && n >= 1 && n <= Math.Pow(10, 5))
            {
                line = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(line))
                {
                    throw new Exception();
                }

                var splits = line.Split(' ');
                if (splits.Length != n)
                {
                    throw new Exception();
                }
                
                var array = new long[splits.Length];                
                for (int i = 0; i < splits.Length; i++)
                {
                    if (long.TryParse(splits[i], out long ai))
                    {
                        if (1 > ai || ai > Math.Pow(10, 18))
                        {
                            throw new Exception();
                        }

                        array[i] = ai;                        
                    }
                    else
                    {
                        throw new Exception();
                    }
                }

                var x = 0;
                foreach (var item in array.OrderBy(o => o).Reverse())
                {
                    x = Convert.ToInt32(Math.Ceiling( Math.Pow((item + x), 0.5)));
                }
                
                Console.WriteLine(x);
                Console.ReadLine();
            }
            else
            {
                throw new Exception();
            }            
        }
    }
}