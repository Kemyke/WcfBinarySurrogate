﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfBinarySurrogate;

namespace TestWcfService
{
    public class MyBinarySerializedTypeProvider : IBinarySerializedTypeProvider
    {
        private static readonly IEnumerable<Type> surrogatedTypes = new List<Type>()
            {
                typeof(CustomChild), 
                typeof(IEnumerable<CustomChild>),
            };

        public IEnumerable<Type> GetBinarySerializableTypes()
        {
            return surrogatedTypes;
        }
    }
}
