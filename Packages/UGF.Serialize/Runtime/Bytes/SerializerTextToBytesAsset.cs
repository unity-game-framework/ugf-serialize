using UnityEngine;

namespace UGF.Serialize.Runtime.Bytes
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Serialize/Serializer Text to Bytes", order = 2000)]
    public class SerializerTextToBytesAsset : SerializerAsset<byte[]>
    {
        [SerializeField] private SerializerAsset m_text;

        public SerializerAsset Text { get { return m_text; } set { m_text = value; } }

        protected override ISerializer<byte[]> OnBuildTyped()
        {
            var serializer = m_text.Build<ISerializer<string>>();

            return new SerializerTextToBytes(serializer);
        }
    }
}
