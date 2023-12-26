using System;

namespace Blackboard.Actions
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CategoryAttribute : Attribute
    {
        public string Category => category;
        
        private string category;

        public CategoryAttribute(string category)
        {
            this.category = category;
        }
    }
}