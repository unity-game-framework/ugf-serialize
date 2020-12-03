#if UGF_SERIALIZE_YAML
using UnityEngine;

namespace UGF.Serialize.Runtime.Yaml
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Serialize/Serializer Yaml", order = 2000)]
    public class SerializerYamlAsset : SerializerAsset<string>
    {
        protected override ISerializer<string> OnBuildTyped()
        {
            return new SerializerYaml();
        }

        private void Reset()
        {
            Name = "yaml-text";
        }
    }
}
#endif
