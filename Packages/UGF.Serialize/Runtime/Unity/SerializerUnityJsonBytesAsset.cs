using System.Text;

namespace UGF.Serialize.Runtime.Unity
{
    public class SerializerUnityJsonBytesAsset : SerializerAsset<byte[]>
    {
        protected override ISerializer<byte[]> OnBuildTyped()
        {
            return new SerializerUnityJsonBytes(Encoding.Default);
        }
    }
}
