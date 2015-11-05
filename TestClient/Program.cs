using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using TestClient.TestServiceReference;
using XmlSerializerTest;

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

            var x =  c.GetData(100);
        }

        private static void ApplyDataContractSurrogate(OperationDescription description)
        {
            DataContractSerializerOperationBehavior dcsOperationBehavior = description.Behaviors.Find<DataContractSerializerOperationBehavior>();
            if (dcsOperationBehavior != null)
            {
                if (dcsOperationBehavior.DataContractSurrogate == null)
                {
                    dcsOperationBehavior.DataContractSurrogate = new BinarySurrogate();
                }
            }
        }
    }
}
