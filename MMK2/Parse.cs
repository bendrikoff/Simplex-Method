using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMK2
{
    class Parse
    {
        //static string equation = "2*x1+0,5*x2-76*x3+x4-x5=10";
        //public static double[] array = parseEqual(equation);
       
        static CultureInfo en = new CultureInfo("en-US");


        public static double[] parseEqual(string equation)
        {
            var parts = equation.Split('='); // уравнение, разделенное на 2 части
            var list = new List<double>();
            ParseLeft(parts[0], ref list); // добавляем выражение слева
            list.Add(Convert.ToDouble(parts[1])); // добавляем правую часть
            return list.ToArray();
        }
        static void ParseLeft(string equation, ref List<double> list)
        {
            if (equation.Length == 0) return;
            int i = 0;
            while (++i < equation.Length && !(equation[i] == '-' || equation[i] == '+')) ; // определяем границу первого слагаемого
            double elem;
            list.Add(
                 double.TryParse(equation.Substring(0, i).Split('*')[0], NumberStyles.Float, en, out elem) ? elem
                    : (equation[0] == '-' ? -1 : 1) // если нет числа, значит 1 с учетом знака
                );
            ParseLeft(equation.Substring(i, equation.Length - i), ref list); // рекуррентное заполнение
        }
    }
}
