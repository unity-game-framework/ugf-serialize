using System.Text;
using UnityEngine;

namespace UGF.Serialize.Runtime.Unity
{
    [CreateAssetMenu(menuName = "UGF/Serialize/Serializer Unity Json Bytes", order = 2000)]
    public class SerializerUnityJsonBytesAsset : SerializerAsset<byte[]>
    {
        protected override ISerializer<byte[]> OnBuildTyped()
        {
            Encoding encoding = OnCreateEncoding();

            return new SerializerUnityJsonBytes(encoding);
        }

        protected virtual Encoding OnCreateEncoding()
        {
            return Encoding.Default;
        }

        private void Reset()
        {
            Name = "unity-json-bytes";
        }
    }
}
