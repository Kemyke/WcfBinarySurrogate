using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfBinarySurrogate;

namespace TestWcfService
{
    public class Service1 : IService
    {
        public ParentClass GetData(int value)
        {
            ParentClass ret = new ParentClass();
            ret.NormalChild = new NormalChild() { Name = "normalchild", Value = value };
            ret.CustomChild = new CustomChild() { Name = "customchild", Value = value };
            ret.CustomChildren = new List<CustomChild>() { new CustomChild() { Name = "customchild", Value = value * 2 } };

            return ret;
        }
    }
}
