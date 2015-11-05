using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfBinarySurrogate
{
    [DataContract]
    [Serializable]
    public class ParentClass
    {
        [DataMember]
        public NormalChild NormalChild { get; set; }
        [DataMember]
        public CustomChild CustomChild { get; set; }
        [DataMember]
        public IEnumerable<CustomChild> CustomChildren { get; set; }
    }
}
