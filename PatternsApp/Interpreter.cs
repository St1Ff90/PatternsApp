using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternsApp
{
    public class ExpressionProcessor
    {
        public Dictionary<char, int> Variables = new Dictionary<char, int>();

        public int Calculate(string expression)
        {




            int result = 0;

            List<string> list = new List<string>();

            for (int i = 0; i < expression.Length; ++i)
            {
                if (expression[i] == ' ')
                {
                    i++;
                }

                if (char.IsDigit(expression[i]))
                {
                    StringBuilder integer = new StringBuilder();
                    integer.Append(expression[i]);
                    int j = i + 1;
                    while (j < expression.Length && char.IsDigit(expression[j]))
                    {
                        integer.Append(expression[j]);
                        j++;
                        i = j;
                    }
                    list.Add(integer.ToString());
                    
                }

                else if (char.IsLetter(expression[i]))
                {
                    if (i < expression.Length - 1 && char.IsLetter(expression[i + 1]))
                    {
                        return 0;
                    }

                    if (Variables.ContainsKey(expression[i]))
                    {
                        list.Add(Variables[expression[i]].ToString());
                    }
                }
                if (expression[i] == '-' || expression[i] == '+')
                {
                    list.Add(expression[i].ToString());
                }

            }

            result = int.Parse(list[0]);

            for (int i = 1; i < list.Count; i++)
            {
                if (list[i].Length == 1)
                {
                    if (list[i] == "-")
                    {
                        result -= int.Parse(list[i + 1]);
                    }
                    if (list[i] == "+")
                    {
                        result += int.Parse(list[i + 1]);
                    }
                }
            }

            return result;




        }
    }
}
