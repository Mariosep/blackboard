using System;

namespace Blackboard
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class DescriptionAttribute : Attribute
    {
        public string Description { get; }
        
        public DescriptionAttribute(string description)
        {
            Description = description;
        }
    }
}