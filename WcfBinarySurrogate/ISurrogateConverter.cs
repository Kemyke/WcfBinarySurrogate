using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfBinarySurrogate
{
    public interface ISurrogateConverter
    {
        BinaryStringContainer ConvertToSurrogate(object obj);
        object ConvertFromSurrogate(BinaryStringContainer bs);
    }
}
