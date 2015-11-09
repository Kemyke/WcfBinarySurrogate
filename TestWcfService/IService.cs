using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfBinarySurrogate;

namespace TestWcfService
{
    [ServiceContract]
    [UseBinarySurrogateBehavior(typeof(MyBinarySerializedTypeProvider), typeof(BinarySurrogateConverter))]
    public interface IService
    {
        [OperationContract]
        ParentClass GetData(int value);
    }
}
