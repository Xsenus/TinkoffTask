using System;
using System.Collections.Generic;
using System.Linq;

namespace Task5
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

            if (int.TryParse(line, out int height) && height >= 1 && height <= 300000)
            {
                line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                {
                    throw new Exception();
                }

                var splits = line.Split(' ');
                if (splits.Length < 1)
                {
                    throw new Exception();
                }

                var arrayUp = new int[splits.Length];
                for (int i = 0; i < splits.Length; i++)
                {
                    if (int.TryParse(splits[i], out int x))
                    {
                        if (x < 0)
                        {
                            throw new Exception();
                        }

                        arrayUp[i] = x;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }

                line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                {
                    throw new Exception();
                }

                splits = line.Split(' ');
                if (splits.Length < 1)
                {
                    throw new Exception();
                }

                var arrayDown = new int[splits.Length];
                if (arrayUp.Length != arrayDown.Length)
                {
                    throw new Exception();
                }

                for (int i = 0; i < splits.Length; i++)
                {
                    if (int.TryParse(splits[i], out int x))
                    {
                        if (x < 0)
                        {
                            throw new Exception();
                        }

                        arrayDown[i] = x;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }

                var len = Enumerable.Repeat(int.MaxValue, height + 1).ToArray();
                len[height] = 0;

                var previous = new int[height + 1];
                var leap = new int[height + 1];

                for (var i = 0; i < leap.Length; ++i)
                {
                    leap[i] = i;
                }

                var queue = new Queue<Gap>();
                queue.Enqueue(new Gap(height, 0));
                var visited = height;

                while (queue.Count > 0)
                {
                    var cur = queue.Dequeue();
                    if (len[cur.Position] != cur.Value)
                    {
                        continue;
                    }
                    
                    if (cur.Position == 0)
                    {
                        break;
                    }
                    
                    for (var jumpTo = cur.Position - arrayUp[cur.Position - 1]; jumpTo < visited; ++jumpTo)
                    {
                        var next = jumpTo;
                        if (next > 0) next += arrayDown[next - 1];
                        if (len[next] <= cur.Value + 1) continue;
                        len[next] = cur.Value + 1;
                        previous[next] = cur.Position;
                        leap[next] = jumpTo;
                        queue.Enqueue(new Gap(next, cur.Value + 1));
                    }
                    visited = Math.Min(visited, cur.Position - arrayUp[cur.Position - 1]);
                }
                
                if (len[0] == int.MaxValue)
                {
                    Console.WriteLine(-1);
                    return;
                }
                
                var list = new List<int>();
                var pos = 0;
                
                while (pos != height)
                {
                    list.Add(leap[pos]);
                    pos = previous[pos];
                }
                Console.WriteLine(list.Count);
            }
            else
            {
                throw new Exception();
            }
        }

        public class Gap : IComparable<Gap>
        {
            public int Position { get; private set; }
            public int Value { get; private set; }
            
            public Gap(int position, int value)
            {
                Position = position; 
                Value = value;
            }
            
            public int CompareTo(Gap gap)
            {
                return Value.CompareTo(gap.Value);
            }
        }
    }
}