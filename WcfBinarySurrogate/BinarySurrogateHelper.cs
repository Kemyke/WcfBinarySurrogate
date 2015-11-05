using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace XmlSerializerTest
{
    public static class BinarySurrogateHelper
    {
        public static BinaryStringContainer CreateSurrogate(object obj)
        {
            BinaryStringContainer ret = new BinaryStringContainer();
            using (MemoryStream ms = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                var ba = ms.ToArray();
                ret.BinaryString = Convert.ToBase64String(ba);
            }
            return ret;
        }

        public static object CreateObject(BinaryStringContainer bs)
        {
            object ret;
            byte[] b = Convert.FromBase64String(bs.BinaryString);

            using (MemoryStream ms = new MemoryStream(b))
            {
                IFormatter formatter = new BinaryFormatter();
                ret = formatter.Deserialize(ms);
            }

            return ret;
        }
    }
}
