using UnityEngine;

namespace UGF.Serialize.Runtime.Container
{
    public abstract class SerializerContainerAsset<TData> : SerializerAsset<TData>
    {
        [SerializeField] private SerializerAsset m_container;

        public SerializerAsset Container { get { return m_container; } set { m_container = value; } }
    }
}
