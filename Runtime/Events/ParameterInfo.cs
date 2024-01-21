using System;

namespace Blackboard.Events
{
    [Serializable]
    public struct ParameterInfo
    {
        public Type type;
        public string parameterName;

        public ParameterInfo(Type type, string parameterName)
        {
            this.type = type;
            this.parameterName = parameterName;
        }
    }
}