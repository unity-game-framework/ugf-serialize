﻿using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace UGF.Serialize.Runtime.Formatter
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Serialize/Serializer Formatter Binary", order = 2000)]
    public class SerializerFormatterBinaryAsset : SerializerAsset<byte[]>
    {
        protected override ISerializer<byte[]> OnBuildTyped()
        {
            IFormatter formatter = OnCreateFormatter();

            return new SerializerFormatter(formatter);
        }

        protected virtual IFormatter OnCreateFormatter()
        {
            return new BinaryFormatter();
        }
    }
}
