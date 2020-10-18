using UnityEngine;

namespace UGF.Serialize.Runtime.Yaml
{
    [CreateAssetMenu(menuName = "UGF/Serialize/Serializer Yaml", order = 2000)]
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
