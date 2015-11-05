using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using TestClient.TestServiceReference;
using TestWcfService;
using WcfBinarySurrogate;

namespace TestClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            ServiceClient c = new ServiceClient();

            foreach (OperationDescription opDesc in c.Endpoint.Contract.Operations)
            {
                ApplyDataContractSurrogate(opDesc);
            }

            ParentClass pc =  c.GetData(100);
        }

        private static void ApplyDataContractSurrogate(OperationDescription description)
        {
            DataContractSerializerOperationBehavior dcsOperationBehavior = description.Behaviors.Find<DataContractSerializerOperationBehavior>();
            if (dcsOperationBehavior != null)
            {
                if (dcsOperationBehavior.DataContractSurrogate == null)
                {
                    dcsOperationBehavior.DataContractSurrogate = new BinarySurrogate(new MyBinarySerializedTypeProvider());
                }
                else
                {
                    throw new InvalidOperationException(string.Format("Try to apply surrogate on a behavior which already has one! Original surrogate: {0}. Applied surrogate: {1}.", dcsOperationBehavior.DataContractSurrogate.GetType().Name, typeof(BinarySurrogate).Name));
                }
            }
        }
    }
}
