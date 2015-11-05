using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace WcfBinarySurrogate
{
    public static class BinarySurrogateHelper
    {
        public static BinaryStringContainer CreateSurrogate(object obj)
        {
            BinaryStringContainer ret = new BinaryStringContainer();
            using (MemoryStream ms = new MemoryStream())
            using (GZipStream gs = new GZipStream(ms, CompressionMode.Compress))
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(gs, obj);
                gs.Close();

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
            using (var zipStream = new GZipStream(ms, CompressionMode.Decompress))
            using (var resultStream = new MemoryStream())
            {
                zipStream.CopyTo(resultStream);
                resultStream.Position = 0;
                IFormatter formatter = new BinaryFormatter();
                ret = formatter.Deserialize(resultStream);
            }

            return ret;
        }
    }
}
