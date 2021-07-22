using System.Text;
using UGF.RuntimeTools.Runtime.Encodings;
using UnityEngine;

namespace UGF.Serialize.Runtime.Bytes
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Serialize/Serializer Text to Bytes", order = 2000)]
    public class SerializerTextToBytesAsset : SerializerAsset<byte[]>
    {
        [SerializeField] private SerializerAsset m_text;
        [SerializeField] private EncodingType m_encoding;

        public SerializerAsset Text { get { return m_text; } set { m_text = value; } }
        public EncodingType Encoding { get { return m_encoding; } set { m_encoding = value; } }

        protected override ISerializer<byte[]> OnBuildTyped()
        {
            var serializer = m_text.Build<ISerializer<string>>();
            Encoding encoding = EncodingUtility.GetEncoding(m_encoding);

            return new SerializerTextToBytes(encoding, serializer);
        }
    }
}
