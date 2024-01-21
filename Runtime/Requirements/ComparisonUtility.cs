using System;

namespace Blackboard.Requirement
{
    public enum OperatorType
    {
        Equal,
        NotEqual,
        Less,
        LessOrEqual,
        Greather,
        GreatherOrEqual
    }

    public static class ComparisonUtility
    {
        public static string OperatorTypeToString(OperatorType operatorType)
        {
            switch (operatorType)
            {
                case OperatorType.Equal:
                    return "==";
                case OperatorType.NotEqual:
                    return "!=";
                case OperatorType.Less:
                    return "<";
                case OperatorType.LessOrEqual:
                    return "<=";
                case OperatorType.Greather:
                    return ">";
                case OperatorType.GreatherOrEqual:
                    return ">=";
                default:
                    throw new ArgumentOutOfRangeException(nameof(operatorType), operatorType, null);
            }
        }

        public static bool Compare(bool a, bool b, OperatorType operatorType)
        {
            switch (operatorType)
            {
                case OperatorType.Equal:
                    return a == b;
                case OperatorType.NotEqual:
                    return a != b;
                default:
                    throw new ArgumentOutOfRangeException(nameof(operatorType), operatorType, null);
            }
        }

        public static bool Compare(int a, int b, OperatorType operatorType)
        {
            switch (operatorType)
            {
                case OperatorType.Equal:
                    return a == b;
                case OperatorType.NotEqual:
                    return a != b;
                case OperatorType.Less:
                    return a < b;
                case OperatorType.LessOrEqual:
                    return a <= b;
                case OperatorType.Greather:
                    return a > b;
                case OperatorType.GreatherOrEqual:
                    return a >= b;
                default:
                    throw new ArgumentOutOfRangeException(nameof(operatorType), operatorType, null);
            }
        }

        public static bool Compare(float a, float b, OperatorType operatorType)
        {
            switch (operatorType)
            {
                case OperatorType.Equal:
                    return a == b;
                case OperatorType.NotEqual:
                    return a != b;
                case OperatorType.Less:
                    return a < b;
                case OperatorType.LessOrEqual:
                    return a <= b;
                case OperatorType.Greather:
                    return a > b;
                case OperatorType.GreatherOrEqual:
                    return a >= b;
                default:
                    throw new ArgumentOutOfRangeException(nameof(operatorType), operatorType, null);
            }
        }

        public static bool Compare(string a, string b, OperatorType operatorType)
        {
            switch (operatorType)
            {
                case OperatorType.Equal:
                    return a == b;
                case OperatorType.NotEqual:
                    return a != b;
                default:
                    throw new ArgumentOutOfRangeException(nameof(operatorType), operatorType, null);
            }
        }
    }
}