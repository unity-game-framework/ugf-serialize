using System;
using UGF.Builder.Runtime;
using UnityEngine;

namespace UGF.Serialize.Runtime
{
    public abstract class SerializerAsset : BuilderAsset<ISerializer>, ISerializerBuilder
    {
        [SerializeField] private string m_name;

        public string Name { get { return m_name; } set { m_name = value; } }

        public abstract Type DataType { get; }
    }
}
