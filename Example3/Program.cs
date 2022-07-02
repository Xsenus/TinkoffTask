/*
3 задание
Условие
Ограничение времени - 1 секунда
Ограничение памяти - 256 МБ
У Кати насыщенный день на работе. Ей надо передать n разных договоров коллегам. 
Все встречи происходят на разных этажах, а между этажами можно перемещаться только по лестничным пролетам — считается, 
что это улучшает физическую форму сотрудников. Прохождение каждого пролета занимает ровно ﻿1﻿ минуту.
Сейчас Катя на парковочном этаже, планирует свой маршрут. Коллег можно посетить в любом порядке, но один из них покинет офис через ﻿t﻿ минут. 
С парковочного этажа лестницы нет — только лифт, на котором можно подняться на любой этаж. В итоге план Кати следующий:
Подняться на лифте на произвольный этаж. Считается, что лифт поднимается на любой этаж за ﻿00﻿ минут.
Передать всем коллегам договоры, перемещаясь между этажами по лестнице. Считается, что договоры на этаже передаются мгновенно.
В первые ﻿t﻿ минут передать договор тому коллеге, который планирует уйти.
Пройти минимальное количество лестничных пролетов.Помогите Кате выполнить все пункты ее плана.

Формат входных данных
В первой строке вводятся целые положительные числа ﻿n﻿ и ﻿t﻿ ﻿(2≤n,t≤100)﻿ — количество сотрудников и время, 
когда один из сотрудников покинет офис (в минутах). В следующей строке n чисел — номера этажей, на которых находятся сотрудники. 
Все числа различны и по абсолютной величине не превосходят 100. Номера этажей даны в порядке возрастания. В следующей строке записан номер сотрудника, который уйдет через t минут.

Формат выходных данных
Выведите одно число — минимально возможное число лестничных пролетов, которое понадобится пройти Кате.

Замечание
В первом примере времени достаточно, чтобы Катя поднялась по этажам по порядку.
Во втором примере Кате понадобится подняться к уходящему сотруднику, а потом пройти всех остальных — например, в порядке ﻿{1,2,3,4,6}

Ввод                    Вывод
5 5 
1 4 9 16 25             24
2

6 4
1 2 3 6 8 25            31
5
*/

using System;

namespace Example3
{
    class Program
    {
        static void Main(string[] args)
        {
            var tmp = Console.ReadLine();
            var splits = tmp?.Trim().Split(' ');
            
            if (splits.Length == 2)
            {
                for (int i = 0; i < splits.Length; i++)
                {
                    if (int.TryParse(splits[i], out int result))
                    {
                        if (result < 2 || result > 100)
                        {
                            throw new Exception();
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                
                var n = Convert.ToInt32(splits[0]);
                var t = Convert.ToInt32(splits[1]);

                tmp = Console.ReadLine();
                splits = tmp?.Trim().Split(' ');

                if (splits.Length != n)
                {
                    throw new Exception();
                }
                
                var numbers = new int[splits.Length];                
                for (int i = 0; i < splits.Length; i++)
                {
                    if (int.TryParse(splits[i], out int result))
                    {
                        if (result < 0 || result > 100)
                        {
                            throw new Exception();
                        }
                                                
                        numbers[i] = result;

                        if (i - 1 >= 0 && result < numbers[i - 1])
                        {
                            throw new Exception();
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                }

                tmp = Console.ReadLine();                
                if (int.TryParse(tmp, out int nymberStaff) && nymberStaff >= 1 && nymberStaff <= n)
                {
                    var result = default(int);
                    for (int i = 0; i < numbers.Length; i++)
                    {
                        if (i - 1 >= 0)
                        {
                            if (i == nymberStaff && result > t)
                            {
                                result = 0;
                                break;
                            }
                            
                            result += numbers[i] - numbers[i - 1];                            
                        }
                    }

                    if (result == 0)
                    {
                        var left = 0;
                        for (int i = nymberStaff - 1; i > 0; i--)
                        {
                            if (i - 1 >= 0)
                            {
                                left += numbers[i] - numbers[i - 1];
                            }
                        }

                        var right = 0;
                        for (int i = nymberStaff; i < numbers.Length; i++)
                        {
                            if (i - 1 >= 0)
                            {
                                right += numbers[i] - numbers[i - 1];
                            }
                        }

                        if (right > left)
                        {
                            result = left + numbers[nymberStaff] - numbers[0];
                        }
                        else
                        {
                            result = right + numbers[numbers.Length - 1] - numbers[nymberStaff - 1] + left;
                        }
                    }

                    Console.WriteLine(result);
                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
                throw new Exception();
            }
        }
    }
}