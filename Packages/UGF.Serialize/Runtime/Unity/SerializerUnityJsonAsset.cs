using UnityEngine;

namespace UGF.Serialize.Runtime.Unity
{
    [CreateAssetMenu(menuName = "UGF/Serialize/Serializer Unity Json", order = 2000)]
    public class SerializerUnityJsonAsset : SerializerAsset<string>
    {
        [SerializeField] private bool m_readable;

        public bool Readable { get { return m_readable; } set { m_readable = value; } }

        protected override ISerializer<string> OnBuildTyped()
        {
            return new SerializerUnityJson(m_readable);
        }

        private void Reset()
        {
            Name = "unity-json-text";
        }
    }
}
