using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMK2
{
    class SimplexMethod
    {
        public static int vars = 5;
        public static int equats = 3;

        public static bool searchMin = true;

        public static double[,] table = { {12, 3, -2, 1, 0, 0},
                                          {8, -1, 2, 0, 1, 0},
                                          {6, 2, 3, 0, 0, 1},
                                          {0, 2, -1, 0, 0, 0}
        };

        //public static double[,] table = { {5, 1, -5, 1, 0, 0},
        //                                  {4, -1, 1, 0, 1, 0},
        //                                  {8, 1, 1, 0, 0, 1},
        //                                  {0, -2, -3, 0, 0, 0}
        //};



        public static void Resolve()
        {
            int leadColumn = getLeadingColumn();
            int leadLine = getLeadingLine(leadColumn);

            Console.WriteLine();
            while (!isResult(leadLine))
            {
                leadColumn = getLeadingColumn();
                leadLine = getLeadingLine(leadColumn);

                newBasis(leadColumn, leadLine);
                for (int i = 0; i < equats + 1; i++)
                {

                    for (int j = 0; j < vars + 1; j++)
                    {
                        Console.Write($"{table[i, j]} ");
                    }
                    Console.WriteLine();

                }
            }
            

           


        }

        //Ищем максимальное положительное число в последней строке для минимума и возвращаем номер столбца(ведущий столбец)
        //Для максимума ищем минимальное отрицательное 
        static int getLeadingColumn()
        {
            int extrIndex = 0;

            for (int i = 0; i < vars + 1; i++)
            {
                if (searchMin)
                {
                    if (table[equats, i] > 0 && table[equats, i] > table[equats, extrIndex])
                    {
                        extrIndex = i;
                    }
                }
                else
                {
                    if (table[equats, i] < table[equats, extrIndex])
                    {
                        extrIndex = i;
                    }
                }
            }

            return extrIndex;
        }
        //находим минимальное положительное значение в ведущем столбце(ведущая строка)

        //Доделать деление
        static int getLeadingLine(int leadingColumn)
        {
            int minIndex = 0; //Присваиваем первый попавшийся положительный элемент
            for(int i = 0; i < equats + 1; i++)
            {
                if(table[i, 0] / table[i, leadingColumn] > 0)
                {
                    minIndex = i;
                }
            }

            for (int i = 0; i < equats + 1; i++)
            {

                //Выбираем минимальный положительный  результат деления свободного столбца на ведущий и записываем его индекс
                if (table[i,0] / table[i,leadingColumn] > 0 && (table[i,0] / table[i,leadingColumn]) < (table[minIndex,0] / table[minIndex,leadingColumn]))
                {
                    Console.WriteLine($"i:{i}, {table[i, 0] / table[i, leadingColumn]}");
                    minIndex = i;
                }

            }

            return minIndex;
        }
        //Вычисляем новое базиcное решение(для этого выбранную переменную вводим в базис)
        static void newBasis(int leadingColumn,int leadingLine)
        {
           
            //Вычисляем новую базисную строку, для этого делим строку почленно на ведущий элемент
            double leadingElement = table[leadingLine, leadingColumn];

            double[] leadingVector = new double[equats+1]; //Вектор содержит элементы из ведущего столбца

            for (int i = 0; i < equats + 1; i++)
            {               
                for (int j = 0; j < vars + 1; j++)
                {
                    if (j == leadingColumn)
                    {
                        leadingVector[i] = table[i, j];
                    }
                }
            }

            for (int i = 0; i < vars + 1; i++)
            {
                table[leadingLine, i] /= leadingElement;
            }
            //Вычисляем остальные строки, для этого вычитаем из старой строки произведение элемента из базисного столбца на элемент из базисной строки
            for (int i = 0; i < equats+1; i++)
            {
                for (int j = 0; j < vars + 1; j++)
                {
                    if (i!=leadingLine)
                    {
                        table[i, j] -= leadingVector[i] * table[leadingLine, j];
                    }
                }
            }
        }
        static bool isResult(int leadingLine)
        {
            for(int i = 0; i < vars;i++)
            {
                if (searchMin)
                {
                    if (table[equats, i] > 0) return false;
                }
                else
                {
                    if (table[equats, i] < 0) return false;
                }

            }
            return true;

        }
    }
}
