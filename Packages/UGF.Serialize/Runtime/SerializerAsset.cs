using System;
using UnityEngine;

namespace UGF.Serialize.Runtime
{
    public abstract class SerializerAsset : ScriptableObject, ISerializerBuilder
    {
        [SerializeField] private string m_name;

        public string Name { get { return m_name; } set { m_name = value; } }

        public abstract Type DataType { get; }

        public T Build<T>() where T : ISerializer
        {
            return (T)OnBuild();
        }

        public ISerializer Build()
        {
            return OnBuild();
        }

        protected abstract ISerializer OnBuild();
    }
}
