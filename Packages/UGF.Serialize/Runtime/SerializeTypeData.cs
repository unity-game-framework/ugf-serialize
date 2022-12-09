using System;
using UGF.EditorTools.Runtime.IMGUI.Types;
using UnityEngine;

namespace UGF.Serialize.Runtime
{
    [Serializable]
    public struct SerializeTypeData
    {
        [SerializeField] private string m_idValue;
        [SerializeField] private TypeReference m_idType;
        [SerializeField] private TypeReference m_type;

        public string IdValue { get { return m_idValue; } set { m_idValue = value; } }
        public TypeReference IdType { get { return m_idType; } set { m_idType = value; } }
        public TypeReference Type { get { return m_type; } set { m_type = value; } }

        public T GetId<T>()
        {
            return (T)Convert.ChangeType(m_idValue, typeof(T));
        }

        public object GetId()
        {
            return Convert.ChangeType(m_idValue, m_idType.Get());
        }

        public void SetId(object value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            m_idValue = value.ToString();
            m_idType.Set(value.GetType());
        }

        public void SetType(Type type)
        {
            m_type.Set(type);
        }
    }
}
