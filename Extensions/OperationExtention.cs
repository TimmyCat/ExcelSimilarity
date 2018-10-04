using System;

namespace ExcelSimilarity.Extensions
{
    public static class OperationExtention
    {
        public static float Calculate(this string operation, float x, float y)
        {
            switch (operation)
            {
                case "+":
                    return x + y;
                case "-":
                    return x - y;
                case "*":
                    return x*y;
                case "/":
                    return x/y;
                default: throw new Exception("invalid logic");
            }
        }
    }
}
