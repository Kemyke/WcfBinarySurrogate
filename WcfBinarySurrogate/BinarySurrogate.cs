﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XmlSerializerTest
{
    public class BinarySurrogate : IDataContractSurrogate
    {
        private Type[] surrogatedTypes = new Type[] { typeof(CustomChild) };
        private Type[] surrogatedInterfaces = new Type[] { typeof(IEnumerable<CustomChild>) };

        public BinarySurrogate()
        {
            var notSeriazableTypes = surrogatedTypes.Where(t => !t.GetCustomAttributes(typeof(SerializableAttribute), false).Any()).Select(t=>t.Name);
            if (notSeriazableTypes.Any())
            {
                throw new ArgumentException(string.Format("Type must be attibuted with Serializable attribute! Non-attibutes classes: {0}.", string.Join(",", notSeriazableTypes)));
            }
        }

        public Type GetDataContractType(Type type)
        {
            if (surrogatedTypes.Contains(type) || surrogatedInterfaces.Contains(type))
            {
                return typeof(BinaryStringContainer);
            }
            
            return type;
        }

        public object GetObjectToSerialize(object obj, Type targetType)
        {
            if (targetType == typeof(BinaryStringContainer))
            {
                BinaryStringContainer bs = BinarySurrogateHelper.CreateSurrogate(obj);
                return bs;
            }
            return obj;
        }

        public object GetDeserializedObject(object obj, Type targetType)
        {
            if (obj is BinaryStringContainer)
            {
                BinaryStringContainer bs = obj as BinaryStringContainer;
                object ret = BinarySurrogateHelper.CreateObject(bs);
                return ret;
            }
            return obj;
        }

        public Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
        {
            throw new NotImplementedException("GetReferencedTypeOnImport");
        }

        public System.CodeDom.CodeTypeDeclaration ProcessImportedType(System.CodeDom.CodeTypeDeclaration typeDeclaration, System.CodeDom.CodeCompileUnit compileUnit)
        {
            throw new NotImplementedException("ProcessImportedType");
        }

        public object GetCustomDataToExport(Type clrType, Type dataContractType)
        {
            throw new NotImplementedException("GetCustomDataToExport");
        }

        public object GetCustomDataToExport(System.Reflection.MemberInfo memberInfo, Type dataContractType)
        {
            throw new NotImplementedException("GetCustomDataToExport");
        }

        public void GetKnownCustomDataTypes(Collection<Type> customDataTypes)
        {
            throw new NotImplementedException("GetKnownCustomDataTypes");
        }
    }

}
