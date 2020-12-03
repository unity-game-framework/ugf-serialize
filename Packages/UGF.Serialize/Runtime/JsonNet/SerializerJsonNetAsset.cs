#if UGF_SERIALIZE_JSONNET
using UnityEngine;

namespace UGF.Serialize.Runtime.JsonNet
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Serialize/Serializer JsonNet", order = 2000)]
    public class SerializerJsonNetAsset : SerializerAsset<string>
    {
        [SerializeField] private bool m_readable;

        public bool Readable { get { return m_readable; } set { m_readable = value; } }

        protected override ISerializer<string> OnBuildTyped()
        {
            return new SerializerJsonNet(m_readable);
        }

        private void Reset()
        {
            Name = "jsonnet-text";
        }
    }
}
#endif
