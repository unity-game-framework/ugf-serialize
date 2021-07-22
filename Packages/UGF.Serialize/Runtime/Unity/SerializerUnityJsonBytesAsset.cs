using System.Text;
using UGF.RuntimeTools.Runtime.Encodings;
using UnityEngine;

namespace UGF.Serialize.Runtime.Unity
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Serialize/Serializer Unity Json Bytes", order = 2000)]
    public class SerializerUnityJsonBytesAsset : SerializerAsset<byte[]>
    {
        [SerializeField] private EncodingType m_encoding;

        public EncodingType Encoding { get { return m_encoding; } set { m_encoding = value; } }

        protected override ISerializer<byte[]> OnBuildTyped()
        {
            Encoding encoding = EncodingUtility.GetEncoding(m_encoding);

            return new SerializerUnityJsonBytes(encoding);
        }
    }
}
