using System;

namespace UGF.Serialize.Runtime
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class SerializeTypeAttribute : Attribute
    {
        public object Id { get { return m_id ?? throw new ArgumentException("Value not specified."); } }
        public bool HasId { get { return m_id != null; } }

        private readonly object m_id;

        public SerializeTypeAttribute(object id = null)
        {
            m_id = id;
        }
    }
}
