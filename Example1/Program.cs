﻿/*
1 задание
Условие
Ограничение времени - 1 секунда
Ограничение памяти - 256 МБ

Костя подключен к мобильному оператору «Мобайл». Абонентская плата Кости составляет ﻿AA﻿ рублей в месяц. 
За эту стоимость Костя получает ﻿BB﻿ мегабайт интернет-трафика. 
Если Костя выйдет за лимит трафика, то каждый следующий мегабайт будет стоить ему ﻿CC﻿ рублей.
Костя планирует потратить ﻿DD﻿ мегабайт интернет-трафика в следующий месяц. 
Помогите ему сосчитать, во сколько рублей ему обойдется интернет.

Формат входных данных
Вводится ﻿44﻿ целых положительных числа ﻿A, B, C, D (1\leq A, B, C, D \leq100)A,B,C,D(1≤A,B,C,D≤100)﻿ — стоимость тарифа Кости, 
размер тарифа Кости, стоимость каждого лишнего мегабайта, размер интернет-трафика Кости в следующем месяце. 
Числа во входном файле разделены пробелами.

Формат выходных данных
Выведите одно натуральное число — суммарные расходы Кости на интернет.

Замечание
В первом примере Костя сначала оплатит пакет интернета, после чего потратит на ﻿55﻿ мегабайт больше, чем разрешено по тарифу.
Следовательно, за ﻿55﻿ мегабайт он дополняет отдельно, получившаяся стоимость ﻿100+12×5=160﻿ рублей.
Во втором примере Костя укладывается в тарифный план, поэтому платит только за него. 

Ввод                Вывод
100  10  12  15     160
100  10  12  1      100
 */

using System;

namespace Example1
{
    class Program
    {
        static void Main(string[] args)
        {
            var str = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new Exception();
            }

            var split = str.Split(' ');
            var length = split.Length;

            if (length != 4)
            {
                throw new Exception();
            }

            for (int i = 0; i < length; i++)
            {
                if (!int.TryParse(split[i], out int intResult))
                {
                    throw new Exception();
                }
                else if (intResult < 1)
                {
                    throw new Exception();
                }
                else if (intResult > 100)
                {
                    throw new Exception();
                }
            }

            var result = 0;

            var a = Convert.ToInt32(split[0]);
            var b = Convert.ToInt32(split[1]);
            var c = Convert.ToInt32(split[2]);
            var d = Convert.ToInt32(split[3]);

            if (d > b)
            {
                var difference = d - b;
                result += difference * c;
            }

            result += a;

            Console.WriteLine(result);
        }
    }
}