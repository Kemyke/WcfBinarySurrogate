using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using XmlSerializerTest;

namespace TestWcfService
{
    [ServiceContract]
    [UseBinarySurrogateBehavior]
    public interface IService
    {
        [OperationContract]
        ParentClass GetData(int value);
    }
}
