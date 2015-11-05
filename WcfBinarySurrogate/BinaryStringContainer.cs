using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WcfBinarySurrogate
{
    [DataContract]
    public class BinaryStringContainer
    {
        private string binaryString = null;
        [DataMember]
        public string BinaryString
        {
            get
            { 
                return binaryString; 
            }
            set
            {
                if(value == null)
                {
                    throw new ArgumentNullException("BinaryString");
                }
                binaryString = value;
            }
        }
    }
}
