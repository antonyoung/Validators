namespace Validators.Tests.Attributes
{

    using System;


    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class PriorityAttribute 
        : Attribute
    {

        public int Priority { get; private set; }


        public PriorityAttribute(int priority) => Priority = priority;
    }
}
