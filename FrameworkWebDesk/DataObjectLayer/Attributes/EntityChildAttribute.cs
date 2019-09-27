using System;
using System.Collections.Generic;
using System.Text;
using DataObjectLayer;

namespace DataObjectLayer
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EntityChildAttribute : Attribute
    {
    }
}
