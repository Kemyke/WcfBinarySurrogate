using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XmlSerializerTest
{
    [DataContract]
    [Serializable]
    public class NormalChild
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Value { get; set; }
    }
}
