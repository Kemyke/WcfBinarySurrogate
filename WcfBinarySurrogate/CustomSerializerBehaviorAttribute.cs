using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WcfBinarySurrogate
{
    public class UseBinarySurrogateBehaviorAttribute : Attribute, IContractBehavior
    {
        private IBinarySerializedTypeProvider typeProvider;
        private ISurrogateConverter surrogateConverter;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="typeProvider">Must implement IBinarySerializedTypeProvider</param>
        public UseBinarySurrogateBehaviorAttribute(Type typeProvider, Type surrogateConverter)
        {
            if (typeProvider == null)
            {
                throw new ArgumentNullException("typeProvider");
            }

            if (surrogateConverter == null)
            {
                throw new ArgumentNullException("surrogateConverter");
            }

            this.typeProvider = Activator.CreateInstance(typeProvider) as IBinarySerializedTypeProvider;
            if (this.typeProvider == null)
            {
                throw new ArgumentException("typeProvider implement IBinarySerializedTypeProvider");
            }

            this.surrogateConverter = Activator.CreateInstance(typeProvider) as ISurrogateConverter;
            if (this.surrogateConverter == null)
            {
                throw new ArgumentException("typeProvider implement ISurrogateConverter");
            }
        }

        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
            this.ReplaceSerializerOperationBehavior(contractDescription);
        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.DispatchRuntime dispatchRuntime)
        {
            foreach (OperationDescription op in endpoint.Contract.Operations)
            {
                DataContractSerializerOperationBehavior dataContractBehavior = op.Behaviors.Find<DataContractSerializerOperationBehavior>() as DataContractSerializerOperationBehavior;
                if (dataContractBehavior != null)
                {
                    dataContractBehavior.DataContractSurrogate = new BinarySurrogate(typeProvider, surrogateConverter);
                }
                else
                {
                    dataContractBehavior = new DataContractSerializerOperationBehavior(op);
                    dataContractBehavior.DataContractSurrogate = new BinarySurrogate(typeProvider, surrogateConverter);
                    op.Behaviors.Add(dataContractBehavior);
                }
            }
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
        }

        private void ReplaceSerializerOperationBehavior(ContractDescription contract)
        {
            foreach (OperationDescription od in contract.Operations)
            {
                DataContractSerializerOperationBehavior dataContractBehavior = od.Behaviors.Find<DataContractSerializerOperationBehavior>() as DataContractSerializerOperationBehavior;
                if (dataContractBehavior != null)
                {
                    dataContractBehavior.DataContractSurrogate = new BinarySurrogate(typeProvider, surrogateConverter);
                }
                else
                {
                    dataContractBehavior = new DataContractSerializerOperationBehavior(od);
                    dataContractBehavior.DataContractSurrogate = new BinarySurrogate(typeProvider, surrogateConverter);
                    od.Behaviors.Add(dataContractBehavior);
                }
            }
        }
    }
}
