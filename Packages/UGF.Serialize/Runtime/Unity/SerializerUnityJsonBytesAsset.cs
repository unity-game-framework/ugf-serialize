using System.Text;
using UnityEngine;

namespace UGF.Serialize.Runtime.Unity
{
    [CreateAssetMenu(menuName = "UGF/Serialize/SerializerUnityJsonBytes", order = 2000)]
    public class SerializerUnityJsonBytesAsset : SerializerAsset<byte[]>
    {
        protected override ISerializer<byte[]> OnBuildTyped()
        {
            return new SerializerUnityJsonBytes(Encoding.Default);
        }

        private void Reset()
        {
            Name = "unity-json-bytes";
        }
    }
}
